using PioneerSolution.Core.Models;

namespace PioneerSolution.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Employee> Employees { get; }
    IGenericRepository<PropertyDefinition> PropertyDefinitions { get; }
    IGenericRepository<EmployeePropertyValue> EmployeePropertyValues { get; }

    Task<int> CompleteAsync();
}

