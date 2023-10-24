using Domain.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }
    public int LastUserId { get; set; } = 0;
    public int LastCommentId { get; set; } = 0;
    public int LastPostId { get; set; } = 0;
}