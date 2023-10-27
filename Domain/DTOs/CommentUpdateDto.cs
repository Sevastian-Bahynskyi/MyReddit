using Domain.Models.Votes;

namespace Domain.DTOs;

public class CommentUpdateDto
{
    public int Id { get; private set; }
    public ContentVote? VoteAction { get; set; }
    public string? CommentBody { get; set; }

    public CommentUpdateDto(int id)
    {
        Id = id;
    }
}