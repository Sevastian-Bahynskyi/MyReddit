using Application.LogicInterfaces;
using Domain.Models;
using Domain.Models.Votes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly MyRedditContext context;
    private IPostDao postDao;

    public CommentEfcDao(MyRedditContext context, IPostDao postDao)
    {
        this.context = context;
        this.postDao = postDao;
    }
    public async Task<Comment> CreateAsync(Comment comment, int postId)
    {
        Post post = (await postDao.GetByIdAsync(postId))!;
        comment.Votes = new PostVotes();
        
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
        EntityEntry<Comment> created = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return created.Entity;
    }

    public async Task<bool> DeleteAsync(int commentId)
    {
        var comment = await GetByIdAsync(commentId);
        var resultEntity = context.Comments.Remove(comment!);
        await context.SaveChangesAsync();
        return resultEntity.State == EntityState.Deleted;
    }

    public async Task<Comment?> GetByIdAsync(int commentId)
    {
        return await context.Comments
            .Include(c => c.Owner)
            .Include(c => c.Replies)
                .ThenInclude(reply => reply.Replies)
                .ThenInclude(reply => reply.Owner)
            .Include(c => c.Votes)
                .ThenInclude(votes => votes.Votes)
            .FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task UpdateAsync(Comment comment)
    {
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }
}