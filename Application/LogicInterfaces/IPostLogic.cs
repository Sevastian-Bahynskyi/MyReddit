using System.Collections;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto creationDto);
    Task<IEnumerable<Post>> GetAllAsync(SearchPostParametersDto searchPostDto);
    Task<Post> GetByIdAsync(int id);
    Task<Post> UpdateAsync(PostUpdateDto updateDto);
}