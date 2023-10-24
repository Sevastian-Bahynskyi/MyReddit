using System.Net.Http.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.HttpClients;

public class HttpCommentClient : ICommentService
{
    private readonly HttpClient client;
    private readonly string START_URI = "comment";

    public HttpCommentClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task<Comment> CreateAsync(CommentCreationDto commentCreationDto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(START_URI, commentCreationDto);
        string content = await HttpClientHelper.HandleResponse(responseMessage);
        return await HttpClientHelper.GenerateObjectFromJson<Comment>(content);
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"{START_URI}/{id}");
        string content = await HttpClientHelper.HandleResponse(responseMessage);
        return await HttpClientHelper.GenerateObjectFromJson<Comment>(content);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        HttpResponseMessage responseMessage = await client.DeleteAsync($"{START_URI}/{id}");
        await HttpClientHelper.HandleResponse(responseMessage);
        return true;
    }
}