namespace Domain.Models.Votes;

public class PostVote
{
    public string OwnerEmail { get; init;}
    public VoteAction Vote { get; init; }

}