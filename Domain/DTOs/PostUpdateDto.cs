using Domain.Models.Votes;

namespace Domain.DTOs;

public class PostUpdateDto
{
    public int Id { get; private set; }
    public PostVote? VoteAction { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}