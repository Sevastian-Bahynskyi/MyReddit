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
    
    [HttpPatch]
    public async Task<ActionResult<Comment>> UpdateCommentAsync([FromBody] CommentUpdateDto updateDto)
    {
        try
        {
            Comment comment = await logic.UpdateAsync(updateDto);
            return Ok(comment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Comment>> GetById([FromRoute] int id)
    {
        try
        {
            Comment comment = await logic.GetByIdAsync(id);
            return Ok(comment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}