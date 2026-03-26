using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Models.Entities.User;

namespace AgroOrganizer.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<UserEntity>> GetAllAsync(int offset, int limit);
    public Task<UserEntity?> GetByIdAsync(int id);
    public Task<UserEntity> CreateAsync(UserEntity userEntityModel);
    public Task<UserEntity?> UpdateAsync(int id, UpdateUserRequestDto userModel);
    public Task<UserEntity?> DeleteAsync(int id);
    public Task<UserEntity?> GetByEmailAsync(string email);
    public Task SaveChangesAsync();
}