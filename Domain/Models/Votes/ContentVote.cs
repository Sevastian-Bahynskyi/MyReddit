namespace Domain.Models.Votes;

public class ContentVote
{
    public string OwnerEmail { get; init;}
    public VoteAction Vote { get; init; }

}