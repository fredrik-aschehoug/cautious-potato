using CautiousPotato.Core.Models;

namespace CautiousPotato.Database.Repositories;

public interface IIngredientRepository
{
    Task CreateAsync(Ingredient ingredient);
    Task<Ingredient[]> GetAllAsync();
    Task<Ingredient?> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
}