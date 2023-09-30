using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetAsync(string username);
}