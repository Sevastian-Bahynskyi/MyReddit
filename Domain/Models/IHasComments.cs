namespace Domain.Models;

public interface IHasComments
{
    int CountAllComments();
    IHasComments? RemoveCommentById(int id);
    Comment? FindACommentById(int id);
}