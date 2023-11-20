using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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
        return Task.FromResult(context.Posts
            .Include(post => post.Owner)
            .Include(post => post.Comments)
            .Include(post => post.Votes)
            .AsQueryable().AsEnumerable());
    }

    public async Task<Post?> GetAsync(string postTitle, string ownerEmail)
    {
        return await context.Posts
            .Include(post => post.Owner)
            .Include(post => post.Comments)
            .Include(post => post.Votes)
            .FirstOrDefaultAsync(p => p.Title.Equals(postTitle) &&
               p.Owner.Email.Equals(ownerEmail));
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await context.Posts
            .Include(post => post.Owner)
            .Include(post => post.Comments)
                .ThenInclude(comment => comment.Replies)
                .ThenInclude(comment => comment.Votes)
                .Include(comment => comment.Owner)
            .Include(post => post.Votes)
                .ThenInclude(vote => vote.Votes)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Post post)
    {
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }
}