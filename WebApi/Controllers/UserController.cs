using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic logic;

    public UserController(IUserLogic userLogic)
    {
        logic = userLogic;
    }


    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync([FromBody] UserRegistrationDto registrationDto)
    {
        try
        {
            User user = await logic.CreateAsync(registrationDto);
            return Created($"/user/{user.Id}", user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}