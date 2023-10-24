using Application.LogicInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao: IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<User> CreateAsync(User user)
    {
        user.Id = context.Data!.LastUserId;
        context.Data.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return Task.FromResult(context.Data!.Users.FirstOrDefault(u => u.Email.Equals(email)));
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return Task.FromResult(context.Data!.Users.FirstOrDefault(u => u.Id == userId));
    }
}