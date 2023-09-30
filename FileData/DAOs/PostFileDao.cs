using Application.LogicInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<Post> CreateAsync(Post post)
    {
        int newId = 1;

        if (context.Posts.Any())
        {
            newId = context.Posts.Max(p => p.Id) + 1;
        }

        post.Id = newId;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<ICollection<Post>> GetByTitleAsync(string titleContains)
    {
        throw new NotImplementedException();
    }

    public Task<Post?> GetAsync(int ownerId)
    {
        throw new NotImplementedException();
    }
}