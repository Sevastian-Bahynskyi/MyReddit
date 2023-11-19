namespace Domain.Models.Votes;

public class ContentVote
{
    public int Id { get; set; }
    public string OwnerEmail { get; init;}
    public VoteAction Vote { get; init; }

}