namespace CauriousPotato.Requests.Recipies;

public record AddIngredientToRecipeRequest(Guid RecipeId, Guid IngredientId);
