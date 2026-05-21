namespace CauriousPotato.Requests.Recipies;

public record RemoveIngredientFromRecipeRequest(Guid RecipeId, Guid IngredientId);
