using CautiousPotato.Client.Services;
using CautiousPotato.Core.Models;
using Microsoft.AspNetCore.Components;

namespace CautiousPotato.Client.Features.Recipies;

public partial class RecipiesList
{
    private Recipe[]? _recipies { get; set; }

    [Inject] public required IRecipiesService RecipiesService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        RecipiesService.OnChangeAsync += RefetchRecipiesAsync;
        await RefetchRecipiesAsync();
    }

    private async Task RefetchRecipiesAsync()
    {
        _recipies = await RecipiesService.GetAllRecipiesAsync();
        StateHasChanged();
    }

    private Task HandleDeleteAsync(Recipe recipe)
    {
        return RecipiesService.DeleteRecipeAsync(new(recipe.Id));
    }

    public void Dispose()
    {
        RecipiesService.OnChangeAsync -= RefetchRecipiesAsync;
    }
}