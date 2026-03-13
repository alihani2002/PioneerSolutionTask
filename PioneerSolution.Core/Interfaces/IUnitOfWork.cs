namespace PioneerSolution.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Models.Employee> Employees { get; }
    IGenericRepository<Models.PropertyDefinition> PropertyDefinitions { get; }
    IGenericRepository<Models.EmployeePropertyValue> EmployeePropertyValues { get; }

    Task<int> CompleteAsync();
}

