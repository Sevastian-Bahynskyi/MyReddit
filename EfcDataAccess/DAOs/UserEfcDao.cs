using Application.LogicInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly MyRedditContext context;

    public UserEfcDao(MyRedditContext context)
    {
        this.context = context;
    }


    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> created = await context.User.AddAsync(user);
        await context.SaveChangesAsync();
        return created.Entity;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Id == userId);
    }
}