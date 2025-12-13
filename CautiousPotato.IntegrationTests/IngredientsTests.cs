using AutoFixture;
using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Core.Models;

namespace CautiousPotato.IntegrationTests;

[Collection(nameof(TestCollection))]
public class IngredientsTests(TestFixture fixture)
{
    [Fact]
    public async Task Can_Create_Ingredient()
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
}
