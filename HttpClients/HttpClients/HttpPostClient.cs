using System.Net.Http.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.HttpClients;

public class HttpPostClient : IPostService
{
    private readonly string START_URI = "/post";
    private readonly HttpClient client;

    public HttpPostClient(HttpClient client)
    {
        this.client = client;
    }
    
    
    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostDto)
    {
        string query = await HttpClientHelper.ConstructQuery(searchPostDto);
        HttpResponseMessage responseMessage = await client.GetAsync($"{START_URI}{query}");

        string content = await HttpClientHelper.HandleResponse(responseMessage);
        return await HttpClientHelper.GenerateObjectFromJson<IEnumerable<Post>>(content);
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"{START_URI}/{id}");
        string content = await HttpClientHelper.HandleResponse(responseMessage);
        return await HttpClientHelper.GenerateObjectFromJson<Post>(content);
    }

    public async Task<Post> CreateAsync(PostCreationDto postCreationDto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync($"{START_URI}", postCreationDto);

        string content = await HttpClientHelper.HandleResponse(responseMessage);
        return await HttpClientHelper.GenerateObjectFromJson<Post>(content);
    }

    public async Task<Post> UpdateAsync(PostUpdateDto updateDto)
    {
        HttpResponseMessage responseMessage = await client.PatchAsJsonAsync($"{START_URI}", updateDto);
        string content = await HttpClientHelper.HandleResponse(responseMessage);
        return await HttpClientHelper.GenerateObjectFromJson<Post>(content);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}