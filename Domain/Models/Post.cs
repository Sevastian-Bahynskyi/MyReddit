using Domain.Models.Votes;

namespace Domain.Models;

public class Post : IHasComments
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Comment> Comments { get; set; }
    public PostVotes Votes { get; set; }

    public Comment? FindACommentById(int id)
    {
        if (Comments.Any(r => r.Id == id))
            return Comments.First(r => r.Id == id);
        
        foreach (var currentComment in Comments)
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
        return Comments.Sum(comment => CountRepliesOfComment(comment) + 1);
    }

    private int CountRepliesOfComment(Comment comment)
    {
        return comment.Replies.Sum(CountRepliesOfComment) + comment.Replies.Count;
    }

    
    
    public Comment? RemoveCommentById(int id)
    {
        if(Comments.RemoveAll(comment => comment.Id == id) > 0)
            return null;
        Comment? toReturn = null;
        Comments.ForEach(reply => toReturn = RemoveReplyById(id, reply));
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