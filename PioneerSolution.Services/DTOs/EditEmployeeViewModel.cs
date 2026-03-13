using PioneerSolution.Core.Models;

namespace PioneerSolution.Services.DTOs;

public class EditEmployeeViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // PropertyDefinitionId -> Value entered by the user
    public Dictionary<int, string> PropertyValues { get; set; } = new();

    // Metadata for rendering the dynamic form fields
    public List<PropertyDefinition> PropertyDefinitions { get; set; } = new();
}
