using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.Auth;
using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Models.Dtos.MailModel;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Models.PasswordHasher.Interface;
using AgroOrganizer.Services.Auth.Interfaces;
using AgroOrganizer.Services.Mail;
using AgroOrganizer.Services.Mail.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace AgroOrganizer.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMailService _mailService;
    private readonly IJwtUtils _jwtUtils;
    public readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(IMailService mailService, IJwtUtils jwtUtils, 
        ApplicationDbContext context, IPasswordHasher passwordHasher, IConfiguration configuration)
    {
        _jwtUtils = jwtUtils;
        _mailService = mailService;
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }
    
    public async Task<LoginResponseDto?> Authenticate(HttpContext context, LoginRequestDto model)
    {
        try
        {
            var user = await _context.Users
                .Where(user => user.Email == model.Email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }
            
            //Compare passwords
            var clientSalt = user.PasswordSalt;
            var clientHash = user.PasswordHash;
            
            var passwordCorrect = _passwordHasher.Verify(model.Password, clientHash, clientSalt);

            if (!passwordCorrect)
            {
                return null;
            }
            
            //Check if user is required to change his/her password
            if (user.ShouldChangePassword)
            {
                return new LoginResponseDto(user);
            }
            
            await _context.SaveChangesAsync();
            return await GenerateTokens(context, user.Id);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while authenticating user");
            return null;
        }
    }

    public async Task<LoginResponseDto?> GenerateTokens(HttpContext context, int userId)
    {
        //authentication successful so generate jwt token
        try
        {
            
            var userEntity = await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
            if (userEntity == null) return null;
            
            var accessToken = _jwtUtils.GenerateJwtToken(userEntity);
            var refreshToken = _jwtUtils.GenerateJwtRefreshToken(userEntity);

            var accesTokenExpiryMinutes = int.Parse(_configuration["AuthJWT:ExpiryMinutes"] ?? "10");
            var refreshTokenExpiryMinutes = int.Parse(_configuration["AuthJWT:RefreshExpiryMinutes"] ?? "20");
            
            var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"; //Check if we are in dev environment http
            
            context.Response.Cookies.Append("Access-Token", accessToken, new CookieOptions()
            {
                SameSite = isDev ? SameSiteMode.Unspecified : SameSiteMode.None,
                Secure = !isDev,
                HttpOnly = true,
                IsEssential = true,
                Expires = DateTimeOffset.Now.AddMinutes(accesTokenExpiryMinutes)
            });

            context.Response.Cookies.Append("Refresh-Token", refreshToken, new CookieOptions()
            {
                SameSite = isDev ? SameSiteMode.Unspecified : SameSiteMode.None,
                Secure = !isDev,
                HttpOnly = true,
                IsEssential = true,
                Expires = DateTimeOffset.Now.AddMinutes(refreshTokenExpiryMinutes)
            });
            
            return new LoginResponseDto(userEntity);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while generating tokens");
            return null;
        }
    }

    public async Task<string> ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
    {
        try
        {
            var userEntity = await _context.Users.Where(user => user.Email == changePasswordRequestDto.Email).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                throw new NotFoundException("Could not find user with this email address.");
            }
        
            //Compare passwords
            var clientSalt = userEntity.PasswordSalt;
            var clientHash = userEntity.PasswordHash;
            
            
            var oldPasswordCorrect = _passwordHasher.Verify(changePasswordRequestDto.OldPassword, clientHash, clientSalt);
            if (!oldPasswordCorrect)
            {
                throw new BadRequestException("Old password is incorrect.");
            }
        
            //Change password
            (string newHashString, string newSaltString ) = _passwordHasher.Hash(changePasswordRequestDto.NewPassword);
            userEntity.ChangePassword(newHashString, newSaltString, false);
        
            await _context.SaveChangesAsync();
        
            return "Changed password successfully!";
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while changing password");
            return null;
        }
      
    }

    public async Task<bool> ResetPassword(ForgottenPasswordRequestDto forgottenPasswordRequestDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(); //Start writing down everything in a 'temporary memory'.
        try
        {
            var userEntity = await _context.Users.Where(user => user.Email == forgottenPasswordRequestDto.Email).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                return true;
            }

            var password = _passwordHasher.CreatePassword(8);
            
            (string newHashString, string newSaltString ) = _passwordHasher.Hash(password);
        
            userEntity.ChangePassword(newHashString, newSaltString, true);
            
            await _context.SaveChangesAsync();
            
            //Send password through mail
            var mailTemplate =
                MailTemplate.ResetPasswordTemplate(userEntity.FirstName, userEntity.LastName, password, _configuration.GetSection("General:AllowOrigin").Value ?? "http://localhost:5173");
            var mailModel = new MailModel()
            {
                MailTo = userEntity.Email,
                Subject = mailTemplate.Subject,
                Body = mailTemplate.Body
            };
            
            var mailSent = await _mailService.SendMail(mailModel);
            if (mailSent)
            {
                await transaction.CommitAsync();
                return true;
            }
            else
            {
                await transaction.RollbackAsync();
                return false;
            }

        }
        catch (Exception e)
        {
            Log.Error(e, "Error while resetting password");
            return false;
        }
    }
}