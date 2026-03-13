namespace PioneerSolution.Core.Models;

public class EmployeePropertyValue
{
    public int Id { get; set; }
    
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public int PropertyDefinitionId { get; set; }
    public PropertyDefinition PropertyDefinition { get; set; } = null!;

    // Stored as string, parsed to appropriate type when rendering based on PropertyDefinition.Type
    public string Value { get; set; } = string.Empty;
}
