namespace Domain.DTOs;

public class UserRegistrationDto
{
    public string Username { get; }
    public string Password { get; }

    public UserRegistrationDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}