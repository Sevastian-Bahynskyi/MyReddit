using Application.LogicInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            post.Comments.Add(comment);
        }

        context.Posts.Update(post);
        await context.SaveChangesAsync();
        return comment;
    }

    public Task<bool> DeleteAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Comment?> GetByIdAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }
}