namespace Domain.DTOs;

public class PostUpdateDto
{
    public enum VoteUpdateAction
    {
        UpVote, DownVote
    }
    public int Id { get; set; }
    public VoteUpdateAction VoteAction { get; set; }
}