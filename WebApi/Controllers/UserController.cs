using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IUserLogic logic;

    public UserController(IConfiguration config, IUserLogic logic)
    {
        this.config = config;
        this.logic = logic;
    }

    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.IsNormalUser()? "user" : "admin"),
            new Claim("Email", user.Email),
            new Claim("Password", user.Password)
        };

        return claims.ToList();
    }

    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new JwtHeader(signIn);

        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60));

        JwtSecurityToken token = new JwtSecurityToken(header, payload);

        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        try
        {
            User user = await logic.RegisterAsync(registrationDto);
            string token = GenerateJwt(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        try
        {
            User user = await logic.LoginAsync(loginDto);
            string token = GenerateJwt(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}