using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Models.ErrorHandling.CustomExceptions;
using AgroOrganizer.Models.PasswordHasher;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly  IConfiguration _configuration;
    private readonly PasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IConfiguration configuration, PasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }
    public async Task<List<UserDto>> GetAll(int? limit, int? offset)
    {
        int safeOffset = offset ?? 0;
        int safeLimit = limit ?? 10;

        if (safeLimit > 50)
            safeLimit = 50;

        if (safeOffset < 0)
            safeOffset = 0;
        
        var users = await _userRepository.GetAllAsync(safeOffset, safeLimit);
        if (users == null)
        {
            throw new NotFoundException("No users found");
        }
        
        return users.Select(u => new UserDto(u)).ToList();
    }

    public Task<UserDto?> GetById(uint userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> CreateUser(CreateUserRequestDto model)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> UpdateUser(UpdateUserRequestDto model, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(uint userId)
    {
        throw new NotImplementedException();
    }
}