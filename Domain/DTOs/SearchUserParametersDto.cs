namespace Domain.DTOs;

public class SearchUserParametersDto : SearchDto
{
    public string UsernameContains { get; }

    public SearchUserParametersDto(string usernameContains)
    {
        UsernameContains = usernameContains;
    }
}