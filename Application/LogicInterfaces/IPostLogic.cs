using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto creationDto);
    Task<ICollection<Post>> GetAsync(SearchPostParametersDto searchDto);
    Task<ICollection<Comment>> GetCommentsAsync();
}