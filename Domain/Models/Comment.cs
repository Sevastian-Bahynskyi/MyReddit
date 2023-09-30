namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public Post Post { get; }
    public ICollection<Comment> Replies { get; set; }
    public string CommentBody { get; set; }
    public int Likes { get; private set; }

    public Comment(string commentBody, Post post)
    {
        CommentBody = commentBody;
        Post = post;
        Replies = new List<Comment>();
    }

    public void Like()
    {
        Likes++;
    }
}