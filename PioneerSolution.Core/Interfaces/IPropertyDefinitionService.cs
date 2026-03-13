using PioneerSolution.Core.Models;

namespace PioneerSolution.Core.Interfaces;

public interface IPropertyDefinitionService
{
    Task<IEnumerable<PropertyDefinition>> GetAllAsync();
    Task<PropertyDefinition?> GetByIdAsync(int id);
    Task CreateAsync(PropertyDefinition definition);
    Task DeleteAsync(int id);
}
