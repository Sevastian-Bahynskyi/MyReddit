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
        AddPost(post);
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAllAsync(SearchPostParametersDto searchPostDto)
    {
        ICollection<Post> postList = context.Data!.Posts.OrderByDescending(p => p.CreatedAt).ToList();
        return Task.FromResult(postList.AsEnumerable());
    }

    public Task<Post?> GetAsync(string postTitle, string ownerEmail)
    {
        return Task.FromResult(context.Data!.Posts.FirstOrDefault(p => p.Title.Equals(postTitle) && p.Owner.Email == ownerEmail));
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Data!.Posts.FirstOrDefault(p => p.Id == id));
    }

    public Task UpdateAsync(Post post)
    {
        Post? existing = context.Data!.Posts.FirstOrDefault(p => p.Id == post.Id);
        if (existing is null)
            throw new Exception($"Post with id {post.Id} is not found");

        context.Data.Posts.Remove(existing);
        context.Data.Posts.Add(post);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Comment> CreateCommentAsync(Comment comment, int postId)
    {
        Post post = context.Data!.Posts.FirstOrDefault(p => p.Id == postId)!;
        comment.Id = ++context.Data!.LastCommentId;
        
        if(comment.ReplyToCommentId != null)
        {
            var foundComment = post.FindACommentById(comment.ReplyToCommentId.Value);
            if(foundComment != null)
                foundComment.Replies.Add(comment);
        }
        else
        {
            post.Comments.Add(comment);
        }
        
        context.SaveChanges();
        return Task.FromResult(comment);
    }

    private void AddPost(Post post)
    {
        post.Id = ++context.Data!.LastPostId;
        context.Data!.Posts.Add(post);
        context.SaveChanges();
    }
}