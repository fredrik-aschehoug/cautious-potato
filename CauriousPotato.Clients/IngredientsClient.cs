using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Core.Models;
using Flurl;
using System.Net.Http.Json;

namespace CauriousPotato.Clients;

public class IngredientsClient(HttpClient client) : IIngredientsClient
{
    private readonly Uri Path = new("/api/ingredients", UriKind.Relative);
    public async Task<Ingredient> CreateAsync(CreateIngredientRequest request)
    {
        var response = await client.PostAsJsonAsync(Path, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Ingredient>() ?? throw new("No response");
    }

    public async Task<Ingredient[]> GetAllAsync()
    {
        var response = await client.GetFromJsonAsync<Ingredient[]>(Path);
        return response ?? throw new("No response");
    }

    public Task<Ingredient?> GetAsync(GetIngredientRequest request)
    {
        return client.GetFromJsonAsync<Ingredient?>(Path.AppendPathSegment(request.Id));
    }
}
