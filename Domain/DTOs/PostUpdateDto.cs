namespace Domain.DTOs;

public class PostUpdateDto
{
    public enum VoteUpdateAction
    {
        UpVote, DownVote
    }
    public int Id { get; private set; }
    public VoteUpdateAction? VoteAction { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}