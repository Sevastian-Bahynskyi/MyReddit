using Domain.Models;
using Domain.Models.Votes;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class MyRedditContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = ../EfcDataAccess/Reddit.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<PostVotes>().HasKey(postVotes => postVotes.Id);
        modelBuilder.Entity<ContentVote>().HasKey(contentVote => contentVote.Id);
        modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
    }
}