namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public ICollection<Comment> Replies { get; set; }
    public string CommentBody { get; set; }
    public int Likes { get; private set; }

    public Comment(string commentBody)
    {
        CommentBody = commentBody;
        Replies = new List<Comment>();
    }

    public void Like()
    {
        Likes++;
    }
}