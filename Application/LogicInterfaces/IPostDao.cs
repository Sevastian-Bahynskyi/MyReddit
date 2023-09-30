using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<ICollection<Post>> GetByTitleAsync(string titleContains);
    Task<Post?> GetAsync(int ownerId);
}