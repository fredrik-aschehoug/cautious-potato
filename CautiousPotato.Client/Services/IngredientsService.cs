using CauriousPotato.Clients;
using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Core.Models;

namespace CautiousPotato.Client.Services;

public interface IIngredientsService
{
    event Func<Task>? OnChangeAsync;

    Task CreateIngredientAsync(CreateIngredientRequest request);
    Task<Ingredient[]> GetAllIngredientsAsync();
}

public class IngredientsService(IIngredientsClient client) : IIngredientsService
{
    public event Func<Task>? OnChangeAsync;
    public Task<Ingredient[]> GetAllIngredientsAsync() => client.GetAllAsync();

    public async Task CreateIngredientAsync(CreateIngredientRequest request)
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
}