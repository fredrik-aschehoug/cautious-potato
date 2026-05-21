using AutoFixture;
using CauriousPotato.Requests.Ingredients;
using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;
using Microsoft.AspNetCore.Http.Features;

namespace CautiousPotato.IntegrationTests;

[Collection(nameof(TestCollection))]
public class RecepiesTests(TestFixture fixture)
{
    [Fact]
    public async Task Can_Create_Recipe()
    {
        var ingredient = await CreateIngredientAsync();
        var request = new CreateRecipeRequest(fixture.AutoFixture.Create<string>(), [ingredient.Id]);

        var created = await fixture.RecipesClient.CreateAsync(request);

        Assert.NotNull(created);
        Assert.Equal(request.Name, created.Name);
        Assert.Equal([ingredient], created.Ingredients);

        var result = await fixture.RecipesClient.GetAsync(new(created.Id));

        Assert.Equivalent(created, result);
    }

    [Fact]
    public async Task Can_Create_Recipe_With_No_Ingredients()
    {
        var request = new CreateRecipeRequest(fixture.AutoFixture.Create<string>(), []);

        var created = await fixture.RecipesClient.CreateAsync(request);

        Assert.NotNull(created);
        Assert.Equal(request.Name, created.Name);
        Assert.Empty(created.Ingredients);
    }

    [Fact]
    public async Task Can_Get_All_Recipies()
    {
        var created2 = await fixture.RecipesClient.CreateAsync(new(fixture.AutoFixture.Create<string>(), []));
        var created1 = await fixture.RecipesClient.CreateAsync(new(fixture.AutoFixture.Create<string>(), []));

        var result = await fixture.RecipesClient.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.Contains(result, it => it.Id == created1.Id);
        Assert.Contains(result, it => it.Id == created2.Id);
    }

    private async Task<Ingredient> CreateIngredientAsync()
    {
        var request = new CreateIngredientRequest(fixture.AutoFixture.Create<string>());

        var created = await fixture.IngredientsClient.CreateAsync(request);
        return created;
    }
}
