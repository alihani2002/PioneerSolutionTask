using PioneerSolution.Core.Models;

namespace PioneerSolution.Core.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllWithPropertiesAsync();
    Task<Employee?> GetByIdWithPropertiesAsync(int id);
    Task CreateAsync(Employee employee, Dictionary<int, string> propertyValues);
}
