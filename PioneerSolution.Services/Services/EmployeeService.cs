using Microsoft.EntityFrameworkCore;
using PioneerSolution.Core.Interfaces;
using PioneerSolution.Core.Models;
using PioneerSolution.Data.Context;

namespace PioneerSolution.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;

    public EmployeeService(IUnitOfWork unitOfWork, AppDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllWithPropertiesAsync()
    {
        return await _context.Employees
            .Include(e => e.PropertyValues)
                .ThenInclude(pv => pv.PropertyDefinition)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdWithPropertiesAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.PropertyValues)
                .ThenInclude(pv => pv.PropertyDefinition)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task CreateAsync(Employee employee, Dictionary<int, string> propertyValues)
    {
        // Add the employee first
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.CompleteAsync();

        // Now add all property values
        foreach (var kvp in propertyValues)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                var propValue = new EmployeePropertyValue
                {
                    EmployeeId = employee.Id,
                    PropertyDefinitionId = kvp.Key,
                    Value = kvp.Value
                };
                await _unitOfWork.EmployeePropertyValues.AddAsync(propValue);
            }
        }

        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(int employeeId, string code, string name, Dictionary<int, string> propertyValues)
    {
        var employee = await _context.Employees
            .Include(e => e.PropertyValues)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null) return;

        // Update basic fields
        employee.Code = code;
        employee.Name = name;

        // Remove existing property values
        _context.EmployeePropertyValues.RemoveRange(employee.PropertyValues);

        // Add updated property values
        foreach (var kvp in propertyValues)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                employee.PropertyValues.Add(new EmployeePropertyValue
                {
                    EmployeeId = employee.Id,
                    PropertyDefinitionId = kvp.Key,
                    Value = kvp.Value
                });
            }
        }

        await _context.SaveChangesAsync();
    }
}
