using AgroOrganizer.Models.Dtos.LoginDto;
using AgroOrganizer.Models.Dtos.UserDto;

namespace AgroOrganizer.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAll(int limit, int offset);
    Task<UserDto?> GetById(int userId);
    Task<UserDto?> CreateUser(CreateUserRequestDto model);
    Task<UserDto?> UpdateUser(UpdateUserRequestDto model, int userId);
    Task<bool> DeleteUser(int userId);
    Task<LoginResponseDto> Login(LoginRequestDto loginDto);
}