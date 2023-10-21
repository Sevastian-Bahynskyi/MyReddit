namespace Domain.DTOs;

public class PostCreationDto
{
    public string Title { get; }
    public string OwnerEmail { get; }
    public string Description { get; }

    public PostCreationDto(string ownerEmail, string title, string description)
    {
        OwnerEmail = ownerEmail;
        Title = title;
        Description = description;
    }
}