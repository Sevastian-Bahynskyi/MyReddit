using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly MyRedditContext context;

    public PostEfcDao(MyRedditContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> created = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return created.Entity;
    }

    public Task<IEnumerable<Post>> GetAllAsync(SearchPostParametersDto searchPostDto)
    {
        throw new NotImplementedException();
    }

    public Task<Post?> GetAsync(string postTitle, string ownerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }
}