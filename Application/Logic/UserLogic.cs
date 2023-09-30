using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUserDao userDao;
    const int USERNAME_MIN = 4;
    const int USERNAME_MAX = 20;
    const int PASSWORD_MIN = 8;
    const int PASSWORD_MAX = 20;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserRegistrationDto registrationDto)
    {
        User? existing = await userDao.GetAsync(registrationDto.Username);
        if (existing is not null)
            throw new Exception($"User with username {existing.Username} already exists");
            
        ValidateUserInput(registrationDto.Username, registrationDto.Password);
        User user = new User()
        {
            Password = registrationDto.Password,
            Username = registrationDto.Username
        };
        
        return await userDao.CreateAsync(user);
    }


    private void ValidateUserInput(string username, string password)
    {
        if (username.Length is < USERNAME_MIN or > USERNAME_MAX)
            throw new Exception($"Username length must be between {USERNAME_MIN} and {USERNAME_MAX}");
        
        if(password.Length is < PASSWORD_MIN or > PASSWORD_MAX)
            throw new Exception($"Password length must be between {PASSWORD_MIN} and {PASSWORD_MAX}");
    }
}