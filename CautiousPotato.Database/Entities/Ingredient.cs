namespace CautiousPotato.Database.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public required string Name { get; set; }
}
