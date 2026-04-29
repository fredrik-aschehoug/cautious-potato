using CautiousPotato.Core.Models;

namespace CautiousPotato.Database.Repositories;

public interface IRecipiesRepository
{
    Task CreateAsync(Recipe recipe);
    Task<Recipe[]> GetAllAsync();
    Task<Recipe?> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
}
