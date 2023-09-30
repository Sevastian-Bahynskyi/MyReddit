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
        int newId = 1;
        if (context.Users.Any())
            newId = context.Users.Max(u => u.Id) + 1;

        user.Id = newId;
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return Task.FromResult(context.Users.FirstOrDefault(u => u.Username.Equals(username)));
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return Task.FromResult(context.Users.FirstOrDefault(u => u.Id == userId));
    }
}