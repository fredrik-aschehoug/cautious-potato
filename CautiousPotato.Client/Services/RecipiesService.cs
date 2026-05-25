using CauriousPotato.Clients;
using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;

namespace CautiousPotato.Client.Services;

public interface IRecipiesService
{
    event Func<Task>? OnChangeAsync;

    Task CreateRecipeAsync(CreateRecipeRequest request);
    Task DeleteRecipeAsync(DeleteRecipeRequest request);
    Task<Recipe[]> GetAllRecipiesAsync();
}

public class RecipiesService(IRecipesClient client) : IRecipiesService
{
    public event Func<Task>? OnChangeAsync;
    public Task<Recipe[]> GetAllRecipiesAsync() => client.GetAllAsync();

    public async Task CreateRecipeAsync(CreateRecipeRequest request)
    {
        await client.CreateAsync(request);
        await NotifyStateChangedAsync();
    }

    private async Task NotifyStateChangedAsync()
    {
        if (OnChangeAsync != null)
        {
            await OnChangeAsync();
        }
    }

    public async Task DeleteRecipeAsync(DeleteRecipeRequest request)
    {
        await client.DeleteAsync(request);
        await NotifyStateChangedAsync();
    }
}
