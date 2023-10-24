using Domain.Models.Votes;

namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public int? ReplyToCommentId { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    public string CommentBody { get; init; }
    public User Owner { get; init; }
    public PostVotes Votes { get; set; } = new ();
}