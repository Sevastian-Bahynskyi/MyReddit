using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private IUserDao userDao;
    private IPostDao postDao;
    private ICommentDao commentDao;

    public CommentLogic(IUserDao userDao, IPostDao postDao, ICommentDao commentDao)
    {
        this.userDao = userDao;
        this.postDao = postDao;
        this.commentDao = commentDao;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto creationDto)
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
        
        return await commentDao.CreateAsync(comment, creationDto.PostId);
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await commentDao.GetByIdAsync(id);
    }
}