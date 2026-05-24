using CautiousPotato.Core.Models;

namespace CautiousPotato.Database.Repositories;

public interface IRecipiesRepository
{
    Task<Recipe> CreateAsync(string name, Guid[] ingredientIds);
    Task<ICollection<Recipe>> GetAllAsync();
    Task<Recipe?> GetAsync(Guid id);
    Task<Recipe> GetRequiredAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task AddIngredientAsync(Guid id, Guid ingredientId);
    Task RemoveIngredientAsync(Guid id, Guid ingredientId);
}
