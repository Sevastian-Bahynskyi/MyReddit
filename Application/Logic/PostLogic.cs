using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    
    const int TITLE_MIN = 5;
    const int TITLE_MAX = 150;
    const int DESCRIPTION_MIN = 10;
    const int DESCRIPTION_MAX = 20_000;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto creationDto)
    {
        User? existingUser = await userDao.GetByIdAsync(creationDto.OwnerId);
        if(existingUser is null)
            throw new Exception($"User with id {creationDto.OwnerId} doesn't exist.");
        
        
        Post? existing = await postDao.GetAsync(creationDto.Title, creationDto.OwnerId);
        if (existing is not null)
            throw new Exception($"{existing.Owner.Username} already published post with title: {existing.Title}");
        
        ValidateData(creationDto.Title, creationDto.Description);

        Post post = new Post()
        {
            Title = creationDto.Title,
            Owner = existingUser,
            Description = creationDto.Description,
            CreatedAt = DateTime.Now
        };

        return await postDao.CreateAsync(post);
    }

    public async Task<ICollection<Post>> GetAllAsync()
    {
        return await postDao.GetAllAsync();
    }

    public Task<ICollection<Comment>> GetCommentsAsync()
    {
        throw new NotImplementedException();
    }

    private void ValidateData(string title, string description)
    {
        if (title.Length is < TITLE_MIN or > TITLE_MAX)
            throw new Exception($"Post title length must be between {TITLE_MIN} and {TITLE_MAX}");
        
        if (description.Length is < DESCRIPTION_MIN or > DESCRIPTION_MAX)
            throw new Exception($"Post description length must be between {DESCRIPTION_MIN} and {DESCRIPTION_MAX}");
    }
}