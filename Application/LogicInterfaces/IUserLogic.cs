using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> RegisterAsync(UserRegistrationDto registrationDto);
    Task<User> LoginAsync(UserLoginDto loginDto);
}