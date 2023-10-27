namespace Domain.Models;

public interface IHasComments
{
    int CountAllComments();
    Comment? RemoveCommentById(int id);
    Comment? FindACommentById(int id);
}