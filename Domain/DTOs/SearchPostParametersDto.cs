namespace Domain.DTOs;

public class SearchPostParametersDto : SearchDto
{
    public string? OwnerUsername { get; init; }
}