using Domain.Models.Votes;

namespace Domain.Models;

public class Post
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

    public void RemoveCommentById(int id)
    {
        if (Comments.Any(r => r.Id == id))
            Comments.RemoveAll(r => r.Id == id);
        
        foreach (var currentComment in Comments)
        {
            var foundComment = FindAReplyOfComment(id, currentComment);
            if (foundComment != null)
                currentComment.Replies.Remove(foundComment);
                
        }
    }
}