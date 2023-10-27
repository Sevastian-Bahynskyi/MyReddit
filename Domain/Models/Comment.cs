using Domain.Models.Votes;

namespace Domain.Models;

public class Comment : IHasComments
{
    public int Id { get; set; }
    public int? ReplyToCommentId { get; set; }
    public List<Comment> Replies { get; set; } = new();
    public string CommentBody { get; set; }
    public User Owner { get; init; }
    public DateTime CreatedAt { get; set; }
    public PostVotes Votes { get; set; }
    
    public Comment? FindACommentById(int id)
    {
        if (Replies.Any(r => r.Id == id))
            return Replies.First(r => r.Id == id);
        
        foreach (var currentComment in Replies)
        {
            var foundComment = FindAReplyOfComment(id, currentComment);
            if (foundComment != null)
                return foundComment;
        }

        return null;
    }

    private Comment? FindAReplyOfComment(int replyId, Comment comment)
    {
        if (comment.Replies.Any(r => r.Id == replyId))
            return comment.Replies.First(r => r.Id == replyId);
        
        foreach (var reply in comment.Replies)
        {
            var foundComment = FindAReplyOfComment(replyId, reply);
            if (foundComment != null)
                return foundComment;
        }

        return null;
    }
    
    public int CountAllComments()
    {
        return Replies.Sum(comment => CountRepliesOfComment(comment) + 1);
    }

    private int CountRepliesOfComment(Comment comment)
    {
        return comment.Replies.Sum(CountRepliesOfComment) + comment.Replies.Count;
    }

    
    
    public IHasComments? RemoveCommentById(int id)
    {
        if(Replies.RemoveAll(comment => comment.Id == id) > 0)
            return this;
        Comment? toReturn = null;
        Replies.ForEach(reply => toReturn = RemoveReplyById(id, reply));
        return toReturn;

    }
    
    private Comment? RemoveReplyById(int id, Comment comment)
    {
        if(comment.Replies.RemoveAll(c => c.Id == id) > 0)
            return comment;
        
        Comment? toReturn = null;
        comment.Replies.ForEach(reply => toReturn = RemoveReplyById(id, reply));
        return toReturn;
    }
}