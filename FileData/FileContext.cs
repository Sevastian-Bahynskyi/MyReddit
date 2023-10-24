using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string FILE_PATH = "data.json";
    private DataContainer? dataContainer;

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }
    
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.Posts;
        }
    }
    
    public ICollection<Comment> Comments
    {
        get
        {
            LoadData();
            return dataContainer!.Comments;
        }
    }
    

    public void LoadData()
    {
        if (dataContainer != null) return;
        if (!File.Exists(FILE_PATH))
        {
            dataContainer = new DataContainer()
            {
                Posts = new List<Post>(),
                Users = new List<User>(),
                Comments = new List<Comment>()
            };
            return;
        }

        string content = File.ReadAllText(FILE_PATH);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        dataContainer!.Users = dataContainer.Users.OrderBy(u => u.Id).ToList();
        dataContainer!.Posts = dataContainer.Posts.OrderBy(p => p.Id).ToList();
        dataContainer!.Comments = dataContainer.Comments.OrderBy(c => c.Id).ToList();

        string jsonData = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        File.WriteAllText(FILE_PATH, jsonData);
    }
}