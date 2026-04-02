using AgroOrganizer.Context;
using AgroOrganizer.Models.Dtos.UserDto;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<UserEntity>> GetAllAsync(int offset, int limit)
    {
        return await _context.Users
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<UserEntity?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<UserEntity> CreateAsync(UserEntity userEntityModel)
    {
         await _context.Users.AddAsync(userEntityModel);
         await _context.SaveChangesAsync();
         return userEntityModel;
    }

    public async Task<UserEntity?> UpdateAsync(int id, UpdateUserRequestDto userModel)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return null;   
        }
        
        user.Update(userModel);
        
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity?> DeleteAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return null;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}