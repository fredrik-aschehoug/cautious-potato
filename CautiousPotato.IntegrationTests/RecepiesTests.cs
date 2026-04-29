using AutoFixture;
using CauriousPotato.Requests.Ingredients;
using CauriousPotato.Requests.Recipies;
using CautiousPotato.Core.Models;

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

        var result = await fixture.IngredientsClient.GetAsync(new(created.Id));

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Can_Create_Recipe_With_No_Ingredients()
    {
        var request = new CreateIngredientRequest(fixture.AutoFixture.Create<string>());

        var created = await fixture.IngredientsClient.CreateAsync(request);

        Assert.NotNull(created);
        Assert.Equal(request.Name, created.Name);

        var result = await fixture.IngredientsClient.GetAllAsync();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Can_Get_Ingredient()
    {
        var created = await fixture.IngredientsClient.CreateAsync(new(fixture.AutoFixture.Create<string>()));

        var result = await fixture.IngredientsClient.GetAsync(new(created.Id));

        Assert.NotNull(result);
        Assert.Equal(created, result);
    }

    [Fact]
    public async Task Can_Get_All_Ingredients()
    {
        var created1 = await fixture.IngredientsClient.CreateAsync(new(fixture.AutoFixture.Create<string>()));
        var created2 = await fixture.IngredientsClient.CreateAsync(new(fixture.AutoFixture.Create<string>()));

        var result = await fixture.IngredientsClient.GetAllAsync();

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.Contains(created1, result);
        Assert.Contains(created2, result);
    }

    private async Task<Ingredient> CreateIngredientAsync()
    {
        var request = new CreateIngredientRequest(fixture.AutoFixture.Create<string>());

        var created = await fixture.IngredientsClient.CreateAsync(request);
        return created;
    }
}
