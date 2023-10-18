namespace Domain.DTOs;

public class UserRegistrationDto
{
    public string Username { get; }
    public string Email { get; }
    public string Password { get; }

    public UserRegistrationDto(string email, string username, string password)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}