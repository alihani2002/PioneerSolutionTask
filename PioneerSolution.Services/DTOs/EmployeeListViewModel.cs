using PioneerSolution.Core.Models;

namespace PioneerSolution.Services.DTOs;

public class EmployeeListViewModel
{
    public List<PropertyDefinition> PropertyDefinitions { get; set; } = new();
    public List<EmployeeDisplayRow> Employees { get; set; } = new();
}

public class EmployeeDisplayRow
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // PropertyDefinitionId -> Value
    public Dictionary<int, string> DynamicValues { get; set; } = new();
}
