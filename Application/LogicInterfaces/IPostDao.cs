using System.Collections;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAllAsync(SearchPostParametersDto searchPostDto);
    Task<Post?> GetAsync(string postTitle, string ownerEmail);
    Task<Post?> GetByIdAsync(int id);
}