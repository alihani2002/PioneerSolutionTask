namespace PioneerSolution.Core.Models;

public class PropertyDefinition
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // e.g., "Date", "String", "Integer", "Dropdown"
    public string Type { get; set; } = string.Empty; 
    
    // Validation flag
    public bool IsRequired { get; set; }

    // Comma separated expected values for Dropdown type
    // e.g., "HR,IT,Finance"
    public string? DropdownExpectedValues { get; set; }
    
    // Navigation property
    public ICollection<EmployeePropertyValue> PropertyValues { get; set; } = new List<EmployeePropertyValue>();
}
