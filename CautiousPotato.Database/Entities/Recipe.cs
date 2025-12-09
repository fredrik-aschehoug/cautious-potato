namespace CautiousPotato.Database.Entities;

public class Recipe
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public required string Name { get; set; }
    public required Ingredient[] Ingredients { get; set; }
}
