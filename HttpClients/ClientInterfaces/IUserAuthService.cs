using System.Security.Claims;
using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserAuthService
{
    Task LoginAsync(UserLoginDto loginDto);
    Task LogoutAsync();
    Task RegisterAsync(UserRegistrationDto registrationDto);
    Task<ClaimsPrincipal> GetAuthAsync();
    Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}