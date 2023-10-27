using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<Comment> CreateAsync(CommentCreationDto commentCreationDto);
    Task<Comment> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Comment> UpdateAsync(CommentUpdateDto updateDto);
}