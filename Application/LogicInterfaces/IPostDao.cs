using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<ICollection<Post>> GetAllAsync();
    Task<Post?> GetAsync(string postTitle, int ownerId);
}