using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<Comment> GetByIdAsync(int id);
    Task<Comment> CreateAsync(CommentCreationDto creationDto);
    Task<bool> DeleteAsync(int id);
    Task<Comment> UpdateAsync(CommentUpdateDto updateDto);
}