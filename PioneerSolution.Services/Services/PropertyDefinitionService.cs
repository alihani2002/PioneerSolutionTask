using PioneerSolution.Core.Interfaces;
using PioneerSolution.Core.Models;

namespace PioneerSolution.Services.Services;

public class PropertyDefinitionService : IPropertyDefinitionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PropertyDefinitionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PropertyDefinition>> GetAllAsync()
    {
        return await _unitOfWork.PropertyDefinitions.GetAllAsync();
    }

    public async Task<PropertyDefinition?> GetByIdAsync(int id)
    {
        return await _unitOfWork.PropertyDefinitions.GetByIdAsync(id);
    }

    public async Task CreateAsync(PropertyDefinition definition)
    {
        await _unitOfWork.PropertyDefinitions.AddAsync(definition);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var definition = await _unitOfWork.PropertyDefinitions.GetByIdAsync(id);
        if (definition != null)
        {
            _unitOfWork.PropertyDefinitions.Remove(definition);
            await _unitOfWork.CompleteAsync();
        }
    }
}
