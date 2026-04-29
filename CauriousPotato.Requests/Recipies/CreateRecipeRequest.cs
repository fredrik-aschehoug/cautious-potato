namespace CauriousPotato.Requests.Recipies;

public record CreateRecipeRequest(string Name, Guid[] Ingredients);