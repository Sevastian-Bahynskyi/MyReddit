using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic logic;

    public PostController(IPostLogic postLogic)
    {
        logic = postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDto creationDto)
    {
        try
        {
            Post post = await logic.CreateAsync(creationDto);
            return Created($"/post/{post.Owner}/{post.Title}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] SearchPostParametersDto searchPostDto)
    {
        try
        {
            var postList = await logic.GetAllAsync(searchPostDto);
            return Ok(postList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetAsync([FromRoute] int id)
    {
        try
        {
            Post postList = await logic.GetByIdAsync(id);
            return Ok(postList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto updateDto)
    {
        try
        {
            Post post = await logic.UpdateAsync(updateDto);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost, Route("comments")]
    public async Task<ActionResult<Comment>> CreateCommentAsync([FromBody] CommentCreationDto creationDto)
    {
        try
        {
            Comment comment = await logic.CreateCommentAsync(creationDto);
            return Created($"/comments/{comment.Id}/{creationDto.CommentBody}", comment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}