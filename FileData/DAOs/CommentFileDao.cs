using Application.LogicInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class CommentFileDao: ICommentDao
{
    private readonly FileContext context;
    
    public CommentFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<Comment> CreateAsync(Comment comment, int postId)
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
    
    public Task<bool> DeleteAsync(int commentId)
    {
        Comment? foundComment = null;
        foreach (var post in context.Data!.Posts)
        {
            foundComment = post.FindACommentById(commentId);
            if (foundComment != null)
            {
                post.RemoveCommentById(commentId);
                context.SaveChanges();
                return Task.FromResult(true);
            }
        }
        
        return Task.FromResult(false);
    }

    public Task<Comment?> GetByIdAsync(int commentId)
    { 
        Comment? foundComment = null;
        foreach (var post in context.Data!.Posts)
        {
            foundComment = post.FindACommentById(commentId);
            if (foundComment != null)
            {
                break;
            }
        }
        
        return Task.FromResult(foundComment);
    }

    public Task UpdateAsync(Comment comment)
    {
        foreach (var post in context.Data!.Posts)
        {
            var parentComment = post.RemoveCommentById(comment.Id);
            if (parentComment != null)
            {
                if(parentComment.GetType() == typeof(Post))
                    post.Comments.Add(comment);
                else
                {
                    ((Comment)parentComment).Replies.Add(comment);
                }
                context.SaveChanges();
                return Task.CompletedTask;
            }
        }
        return Task.CompletedTask;
    }
}