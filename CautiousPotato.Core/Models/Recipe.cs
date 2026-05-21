namespace CautiousPotato.Core.Models;

public record Recipe(
    Guid Id,
    string Name,
    ICollection<Ingredient> Ingredients
);
