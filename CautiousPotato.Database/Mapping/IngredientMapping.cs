using CautiousPotato.Database.Entities;

namespace CautiousPotato.Database.Mapping;

internal static class IngredientMapping
{
    public static Core.Models.Ingredient ToCoreModel(this Ingredient ingredient) =>
        new(ingredient.ExternalId, ingredient.Name);

    public static ICollection<Core.Models.Ingredient> ToCoreModel(this ICollection<Ingredient> ingredients) =>
        ingredients
            .Select(ingredient => ingredient.ToCoreModel())
            .ToArray();

    public static Ingredient ToEntity(this Core.Models.Ingredient ingredient) =>
        new() { ExternalId = ingredient.Id, Name = ingredient.Name };

    public static ICollection<Ingredient> ToEntity(this ICollection<Core.Models.Ingredient> ingredients) =>
        ingredients
            .Select(ingredient => ingredient.ToEntity())
            .ToArray();
}
