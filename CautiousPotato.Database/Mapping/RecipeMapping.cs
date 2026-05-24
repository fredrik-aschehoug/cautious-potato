using CautiousPotato.Database.Entities;

namespace CautiousPotato.Database.Mapping;

internal static class RecipeMapping
{
    public static Core.Models.Recipe ToCoreModel(this Recipe recipe) =>
        new(recipe.ExternalId, recipe.Name, recipe.Ingredients.ToCoreModel());

    public static ICollection<Core.Models.Recipe> ToCoreModel(this ICollection<Recipe> recipe) =>
        recipe
            .Select(ingredient => ingredient.ToCoreModel())
            .ToArray();
}
