using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController: ControllerBase
{
    private readonly ICommentLogic logic;

    public CommentController(ICommentLogic logic)
    {
        this.logic = logic;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateCommentAsync([FromBody] CommentCreationDto creationDto)
    {
        try
        {
            Comment comment = await logic.CreateAsync(creationDto);
            return Created($"/comments/{comment.Id}/{creationDto.CommentBody}", comment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}