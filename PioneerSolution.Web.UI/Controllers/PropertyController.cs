using Microsoft.AspNetCore.Mvc;
using PioneerSolution.Core.Interfaces;
using PioneerSolution.Core.Models;
using PioneerSolution.Services.DTOs;

namespace PioneerSolution.Web.UI.Controllers;

public class PropertyController : Controller
{
    private readonly IPropertyDefinitionService _propertyService;

    public PropertyController(IPropertyDefinitionService propertyService)
    {
        _propertyService = propertyService;
    }

    // GET: Property
    public async Task<IActionResult> Index()
    {
        var properties = await _propertyService.GetAllAsync();
        return View(properties);
    }

    // GET: Property/Create
    public IActionResult Create()
    {
        return View(new AddPropertyViewModel());
    }

    // POST: Property/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddPropertyViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var definition = new PropertyDefinition
        {
            Name = model.Name,
            Type = model.Type,
            IsRequired = model.IsRequired,
            DropdownExpectedValues = model.Type == "Dropdown" ? model.DropdownExpectedValues : null
        };

        await _propertyService.CreateAsync(definition);

        return RedirectToAction(nameof(Index));
    }

    // POST: Property/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _propertyService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
