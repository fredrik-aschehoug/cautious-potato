using CautiousPotato.Database.Entities;

namespace CautiousPotato.Database.Mapping;

internal static class IngredientMapping
{
    public static Core.Models.Ingredient ToCoreModel(this Ingredient ingredient) =>
        new(ingredient.ExternalId, ingredient.Name);

    public static Core.Models.Ingredient[] ToCoreModel(this Ingredient[] ingredients) =>
        ingredients
            .Select(ingredient => ingredient.ToCoreModel())
            .ToArray();

    public static Ingredient ToEntity(this Core.Models.Ingredient ingredient) =>
        new() { ExternalId = ingredient.Id, Name = ingredient.Name };
}
