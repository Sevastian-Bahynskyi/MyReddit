using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Domain.Models.Votes;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    
    const int TITLE_MIN = 5;
    const int TITLE_MAX = 150;
    const int DESCRIPTION_MIN = 5;
    const int DESCRIPTION_MAX = 20_000;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto creationDto)
    {
        User? existingUser = await userDao.GetByEmailAsync(creationDto.OwnerEmail);
        if(existingUser is null)
            throw new Exception($"User with id {creationDto.OwnerEmail} doesn't exist.");
        
        
        Post? existing = await postDao.GetAsync(creationDto.Title, creationDto.OwnerEmail);
        if (existing is not null)
            throw new Exception($"{existing.Owner.Username} already published post with title: {existing.Title}");
        
        ValidateData(creationDto.Title, creationDto.Description);

        Post post = new Post()
        {
            Title = creationDto.Title,
            Owner = existingUser,
            Description = creationDto.Description,
            CreatedAt = DateTime.Now,
            Comments = new List<Comment>(),
            Votes = new PostVotes()
        };

        return await postDao.CreateAsync(post);
    }

    public async Task<IEnumerable<Post>> GetAllAsync(SearchPostParametersDto searchPostDto)
    {
        var posts = await postDao.GetAllAsync(searchPostDto);
        return posts.OrderByDescending(p => p.CreatedAt);
    }

    public Task<IEnumerable<Comment>> GetCommentsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post  = await postDao.GetByIdAsync(id);
        if (post is null)
            throw new Exception($"Post with id {id} doesn't exist");
        return post;
    }

    public async Task<Post> UpdateAsync(PostUpdateDto updateDto)
    {
        
        Post post = await GetByIdAsync(updateDto.Id);

        post.Title = updateDto.Title ?? post.Title;
        post.Description = updateDto.Description ?? post.Description;


        if (updateDto.VoteAction != null)
        {
            if (await userDao.GetByEmailAsync(updateDto.VoteAction.OwnerEmail) == null)
                throw new Exception($"Vote action can't be done due to email invalidity.");
            post.Votes.AddVote(updateDto.VoteAction);
        }
        
        await postDao.UpdateAsync(post);
        return post;
    }

    public async Task<Comment> CreateCommentAsync(CommentCreationDto creationDto)
    {
        User? existing = await userDao.GetByEmailAsync(creationDto.OwnerEmail);
        if (existing == null)
            throw new Exception($"User with email {creationDto.OwnerEmail} doesn't exist");

        Post? post = await postDao.GetByIdAsync(creationDto.PostId);
        if (post == null)
            throw new Exception($"Post with id {post?.Id} doesn't exist");

        if (creationDto.CommentId != null && post.FindACommentById(creationDto.CommentId.Value) == null)
            throw new Exception($"Comment with id {creationDto.CommentId} doesn't exist");

        Comment comment = new Comment()
        {
            CommentBody = creationDto.CommentBody,
            Owner = existing
        };
        if (creationDto.CommentId != null)
            comment.ReplyToCommentId = creationDto.CommentId;
        
        return await postDao.CreateCommentAsync(comment, creationDto.PostId);
    }

    private void ValidateData(string title, string description)
    {
        if (title.Length is < TITLE_MIN or > TITLE_MAX)
            throw new Exception($"Post title length must be between {TITLE_MIN} and {TITLE_MAX}");
        
        if (description.Length is < DESCRIPTION_MIN or > DESCRIPTION_MAX)
            throw new Exception($"Post description length must be between {DESCRIPTION_MIN} and {DESCRIPTION_MAX}");
    }
}