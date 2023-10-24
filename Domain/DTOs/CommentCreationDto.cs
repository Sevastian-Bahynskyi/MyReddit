namespace Domain.DTOs;

public class CommentCreationDto
{
    public string CommentBody { get; init; }
    public int PostId { get; init; }
    public string OwnerEmail { get; init; }
    public int? CommentId { get; set; }
}