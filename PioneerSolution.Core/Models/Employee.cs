namespace PioneerSolution.Core.Models;

public class Employee
{
    public int Id { get; set; }
    
    // Mandatory fields as per requirements
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Navigation property for dynamic properties
    public ICollection<EmployeePropertyValue> PropertyValues { get; set; } = new List<EmployeePropertyValue>();
}
