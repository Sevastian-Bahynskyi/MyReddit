namespace Domain.DTOs;

public class PostCreationDto
{
    public string Title { get; }
    public int OwnerId { get; }
    public string Description { get; }

    public PostCreationDto(int ownerId, string title, string description)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
    }
}