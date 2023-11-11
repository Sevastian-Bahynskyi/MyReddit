namespace Domain.Models.Votes;

public class PostVotes
{
    private List<ContentVote> votes;
    public IEnumerable<ContentVote> Votes
    {
        get => votes;
        set => votes = value.ToList();
    }

    public int VotesNumber => positiveVotesNumber >= negativeVotesNumber? positiveVotesNumber : -negativeVotesNumber;
    private int positiveVotesNumber => votes.Count(v => v.Vote == VoteAction.VoteUp);
    private int negativeVotesNumber => votes.Count(v => v.Vote == VoteAction.VoteDown);

    public PostVotes()
    {
        votes = new List<ContentVote>();
    }
    
    public PostVotes(IEnumerable<ContentVote> votes)
    {
        this.votes = votes.ToList();
    }

    public bool AddVote(ContentVote vote)
    {
        if (!votes.Any(v =>
                v.OwnerEmail.Equals(vote.OwnerEmail) && v.Vote == vote.Vote))
        {
            votes.RemoveAll(v => v.OwnerEmail.Equals(vote.OwnerEmail));
            votes.Add(vote);
            return true;
        }

        return false;
    }

    public bool AddVote(string ownerEmail, VoteAction voteAction)
    {
        ContentVote vote = new ContentVote() { OwnerEmail = ownerEmail, Vote = voteAction };
        return AddVote(vote);
    }

    public bool VoteUp(string ownerEmail)
    {
        return AddVote(ownerEmail, VoteAction.VoteUp);
    }
    
    public bool VoteDown(string ownerEmail)
    {
        return AddVote(ownerEmail, VoteAction.VoteDown);
    }

    public VoteAction? UserCurrentVote(string userEmail)
    {
        return votes.FirstOrDefault(v => v.OwnerEmail.Equals(userEmail))?.Vote;
    }

}