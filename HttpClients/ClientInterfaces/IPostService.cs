using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostDto);
    Task<Post> CreateAsync(PostCreationDto postCreationDto);
}