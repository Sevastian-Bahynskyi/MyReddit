namespace Domain.Models;

public class PostVote
{
    public enum VoteAction
    {
        VoteUp, VoteDown
    }
    public string OwnerEmail { get; init;}
    public VoteAction Vote { get; init; }

}