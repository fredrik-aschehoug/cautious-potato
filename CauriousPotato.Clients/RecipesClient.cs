using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;
using Flurl;
using System.Net.Http.Json;

namespace CauriousPotato.Clients;

public class RecipesClient(HttpClient client) : IRecipesClient
{
    private readonly Uri Path = new("/api/recipies", UriKind.Relative);
    public async Task<Recipe> CreateAsync(CreateRecipeRequest request)
    {
        var response = await client.PostAsJsonAsync(Path, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Recipe>() ?? throw new("No response");
    }

    public async Task DeleteAsync(DeleteRecipeRequest request)
    {
        await client.DeleteAsync(Path.AppendPathSegment(request.Id));
    }

    public async Task<Recipe[]> GetAllAsync()
    {
        var response = await client.GetFromJsonAsync<Recipe[]>(Path);
        return response ?? throw new("No response");
    }

    public Task<Recipe?> GetAsync(GetRecipeRequest request)
    {
        return client.GetFromJsonAsync<Recipe?>(Path.AppendPathSegment(request.Id));
    }

    public async Task<Recipe> AddIngredientAsync(AddIngredientToRecipeRequest request)
    {
        var path = Path
            .AppendPathSegment(request.RecipeId)
            .AppendPathSegment("ingredients")
            .AppendPathSegment(request.IngredientId);

        var response = await client.PutAsync(path, null);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Recipe>() ?? throw new Exception("Unable to deserialize");

    }

    public async Task<Recipe> RemoveIngredientAsync(RemoveIngredientFromRecipeRequest request)
    {
        var path = Path
            .AppendPathSegment(request.RecipeId)
            .AppendPathSegment("ingredients")
            .AppendPathSegment(request.IngredientId);

        var response = await client.DeleteAsync(path);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Recipe>() ?? throw new Exception("Unable to deserialize");
    }
}
