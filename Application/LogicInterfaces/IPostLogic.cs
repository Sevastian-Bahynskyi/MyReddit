using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto creationDto);
    Task<ICollection<Post>> GetAllAsync();
    Task<ICollection<Comment>> GetCommentsAsync();
}