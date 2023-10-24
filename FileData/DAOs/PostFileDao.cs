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
        context.LoadData();
        return Task.FromResult(context.Posts.FirstOrDefault(p => p.Id == id));
    }

    public Task UpdateAsync(Post post)
    {
        Post? existing = context.Posts.FirstOrDefault(p => p.Id == post.Id);
        if (existing is null)
            throw new Exception($"Post with id {post.Id} is not found");

        context.Posts.Remove(existing);
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Comment> CreateCommentAsync(Comment comment, int postId)
    {
        Post post = context.Posts.FirstOrDefault(p => p.Id == postId)!;
        int id = 1;
        if(context.Comments.Any())
            id = post.Comments.Max(c => c.Id) + 1;
        comment.Id = id;
        
        if(comment.ReplyToCommentId != null)
        {
            context.Comments
            .FirstOrDefault(c => c.Id == comment.ReplyToCommentId)!
            .Replies.Add(comment);
        }
        else
            post.Comments.Add(comment);
        
        context.SaveChanges();
        return Task.FromResult(comment);
    }
}