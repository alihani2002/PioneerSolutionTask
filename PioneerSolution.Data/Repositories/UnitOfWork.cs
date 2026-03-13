using PioneerSolution.Core.Interfaces;
using PioneerSolution.Core.Models;
using PioneerSolution.Data.Context;

namespace PioneerSolution.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IGenericRepository<Employee>? _employees;
    private IGenericRepository<PropertyDefinition>? _propertyDefinitions;
    private IGenericRepository<EmployeePropertyValue>? _employeePropertyValues;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Employee> Employees =>
        _employees ??= new GenericRepository<Employee>(_context);

    public IGenericRepository<PropertyDefinition> PropertyDefinitions =>
        _propertyDefinitions ??= new GenericRepository<PropertyDefinition>(_context);

    public IGenericRepository<EmployeePropertyValue> EmployeePropertyValues =>
        _employeePropertyValues ??= new GenericRepository<EmployeePropertyValue>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}


