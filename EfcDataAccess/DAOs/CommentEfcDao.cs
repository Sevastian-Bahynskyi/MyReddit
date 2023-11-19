using Application.LogicInterfaces;
using Domain.Models;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    public Task<Comment> CreateAsync(Comment comment, int postId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Comment?> GetByIdAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }
}