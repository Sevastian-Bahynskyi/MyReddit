using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Votes;

public class ContentVote
{
    [Key]
    public int Id { get; set; }
    public string OwnerEmail { get; init;}
    public VoteAction Vote { get; init; }

}