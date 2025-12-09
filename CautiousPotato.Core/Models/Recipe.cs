namespace CautiousPotato.Core.Models;

public record Recipe(
    Guid Id,
    string Name,
    Ingredient[] Ingredients
);
