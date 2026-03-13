namespace PioneerSolution.Services.DTOs;

public class AddPropertyViewModel
{
    public string Name { get; set; } = string.Empty;
    
    // "Date", "String", "Integer", "Dropdown"
    public string Type { get; set; } = "String";
    
    public bool IsRequired { get; set; }
    
    // Comma-separated values for Dropdown type (e.g. "HR,IT,Finance")
    public string? DropdownExpectedValues { get; set; }
}
