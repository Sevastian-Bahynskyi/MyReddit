using Application.LogicInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly MyRedditContext context;

    public CommentEfcDao(MyRedditContext context)
    {
        this.context = context;
    }
    public async Task<Comment> CreateAsync(Comment comment, int postId)
    {
        Post post = (await context.Posts.FirstOrDefaultAsync(p => p.Id == postId))!;
        
        if (comment.ReplyToCommentId != null)
        {
            var foundComment = post.FindACommentById(comment.ReplyToCommentId.Value);
            foundComment?.Replies.Add(comment);
        }
        else
        {
            if (post.Comments == null)
                post.Comments = new List<Comment>();
            post.Comments.Add(comment);
        }

        context.Posts.Update(post);
        EntityEntry<Comment> created = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return created.Entity;
    }

    public Task<bool> DeleteAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public async Task<Comment?> GetByIdAsync(int commentId)
    {
        return await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public Task UpdateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }
}