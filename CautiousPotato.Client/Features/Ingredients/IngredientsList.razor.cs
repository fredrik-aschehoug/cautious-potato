using CautiousPotato.Client.Services;
using CautiousPotato.Core.Models;
using Microsoft.AspNetCore.Components;

namespace CautiousPotato.Client.Features.Ingredients;

public sealed partial class IngredientsList : ComponentBase, IDisposable
{
    private Ingredient[]? _ingredients { get; set; }

    [Inject] public required IIngredientsService IngredientsService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IngredientsService.OnChangeAsync += RefetchIngredientsAsync;
        await RefetchIngredientsAsync();
    }

    private async Task RefetchIngredientsAsync()
    {
        _ingredients = await IngredientsService.GetAllIngredientsAsync();
        StateHasChanged();
    }

    public void Dispose()
    {
        IngredientsService.OnChangeAsync -= RefetchIngredientsAsync;
    }
}