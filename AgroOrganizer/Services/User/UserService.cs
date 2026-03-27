using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Models.PasswordHasher;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IConfiguration configuration, PasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }
    public async Task<List<UserDto>> GetAll(int limit, int offset)
    {

        var users = await _userRepository.GetAllAsync(offset, limit);
        if (users == null)
        {
            throw new NotFoundException("No users found");
        }
        
        return users.Select(u => new UserDto(u)).ToList();
    }

    public async Task<UserDto?> GetById(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }
        
        return new UserDto(user);
    }

    public async Task<UserDto?> CreateUser(CreateUserRequestDto model)
    {
        var existingEmail = await _userRepository.GetByEmailAsync(model.Email);
        if (existingEmail != null)
        {
            throw new ConflictException("User with this email already exists");
        }
        
        var (hash, salt) = _passwordHasher.Hash(model.Password);

        var userEntity = new UserEntity(model, salt, hash);
        
        var createdUser = await _userRepository.CreateAsync(userEntity);
        
        return new UserDto(createdUser);
    }

    public async Task<UserDto?> UpdateUser(UpdateUserRequestDto model, int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }

        var existingUser = await _userRepository.GetByEmailAsync(model.Email);
        if (existingUser != null && existingUser.Id != userId)
        {
            throw new ConflictException("Email already in use");
        }

        user.Update(model);

        await _userRepository.SaveChangesAsync();

        return new UserDto(user);
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _userRepository.DeleteAsync(userId);

        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }

        return true;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);

        if (user == null)
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        var isValid = _passwordHasher.Verify(
            loginDto.Password,
            user.PasswordHash,
            user.PasswordSalt);
        
        if (!isValid)
        {
            throw new UnauthorizedException("Invalid email or password");
        }
        
        return new LoginResponseDto(user);
    }
}