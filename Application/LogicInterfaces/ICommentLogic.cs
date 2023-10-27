using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreationDto creationDto);
    Task<Comment> GetByIdAsync(int id);
    Task<Comment> UpdateAsync(CommentUpdateDto updateDto);
}