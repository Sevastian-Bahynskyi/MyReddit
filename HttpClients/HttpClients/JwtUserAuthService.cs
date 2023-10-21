using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using HttpClients.HttpClients;

namespace HttpClients.Implementations;

public class JwtUserAuthService : IUserAuthService
{
    private readonly HttpClient client;
    public static string? Jwt { get; private set; }
    private readonly string START_URI = "/user";
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public JwtUserAuthService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task LoginAsync(UserLoginDto loginDto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync($"{START_URI}/login", loginDto);
        string responseContent = await HttpClientHelper.HandleResponse(responseMessage);
        
        Jwt = responseContent;

        ClaimsPrincipal principal = CreateClaimsPrincipal();
        
        OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        OnAuthStateChanged.Invoke(new ClaimsPrincipal());
        return Task.CompletedTask;
    }

    public async Task RegisterAsync(UserRegistrationDto registrationDto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync($"{START_URI}/register", registrationDto);

        string responseContent = await HttpClientHelper.HandleResponse(responseMessage);
        
        Jwt = responseContent;

        ClaimsPrincipal principal = CreateClaimsPrincipal();
        
        OnAuthStateChanged.Invoke(principal);
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        return Task.FromResult(CreateClaimsPrincipal());
    }
    
    private ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }
        
        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
        
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}