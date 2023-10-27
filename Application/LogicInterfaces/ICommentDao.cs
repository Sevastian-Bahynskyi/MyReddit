using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment, int postId);
    Task<bool> DeleteAsync(int commentId);
    Task<Comment?> GetByIdAsync(int commentId);
    Task UpdateAsync(Comment comment);
}