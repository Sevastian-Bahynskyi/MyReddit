using System.Collections.ObjectModel;
using Application.LogicInterfaces;
using Domain.DTOs;
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

    public Task<IEnumerable<Post>> GetAllAsync(SearchPostParametersDto searchPostDto)
    {
        ICollection<Post> postList = context.Posts.OrderByDescending(p => p.CreatedAt).ToList();
        return Task.FromResult(postList.AsEnumerable());
    }

    public Task<Post?> GetAsync(string postTitle, string ownerEmail)
    {
        return Task.FromResult(context.Posts.FirstOrDefault(p => p.Title.Equals(postTitle) && p.Owner.Email == ownerEmail));
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Posts.FirstOrDefault(p => p.Id == id));
    }

    public async Task UpdateAsync(Post post)
    {
        Post? existing = await GetByIdAsync(post.Id);
        if (existing is null)
            throw new Exception($"Post with id {post.Id} is not found");

        context.Posts.Remove(existing);
        context.Posts.Add(post);
    }
}