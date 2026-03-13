using Microsoft.AspNetCore.Mvc;
using PioneerSolution.Core.Interfaces;
using PioneerSolution.Core.Models;
using PioneerSolution.Services.DTOs;

namespace PioneerSolution.Web.UI.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IPropertyDefinitionService _propertyService;

    public EmployeeController(IEmployeeService employeeService, IPropertyDefinitionService propertyService)
    {
        _employeeService = employeeService;
        _propertyService = propertyService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllWithPropertiesAsync();
        var definitions = await _propertyService.GetAllAsync();

        var viewModel = new EmployeeListViewModel
        {
            PropertyDefinitions = definitions.ToList(),
            Employees = employees.Select(e => new EmployeeDisplayRow
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                DynamicValues = e.PropertyValues.ToDictionary(
                    pv => pv.PropertyDefinitionId,
                    pv => pv.Value)
            }).ToList()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Create()
    {
        var definitions = await _propertyService.GetAllAsync();

        var viewModel = new AddEmployeeViewModel
        {
            PropertyDefinitions = definitions.ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddEmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var definitions = await _propertyService.GetAllAsync();
            model.PropertyDefinitions = definitions.ToList();
            return View(model);
        }

        // Check if an employee with the same code already exists
        var existingEmployee = await _employeeService.GetByCodeAsync(model.Code);
        if (existingEmployee != null)
        {
            ModelState.AddModelError("Code", "An employee with this code already exists.");
            var definitions = await _propertyService.GetAllAsync();
            model.PropertyDefinitions = definitions.ToList();
            return View(model);
        }

        var employee = new Employee
        {
            Code = model.Code,
            Name = model.Name
        };

        await _employeeService.CreateAsync(employee, model.PropertyValues ?? new Dictionary<int, string>());

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetByIdWithPropertiesAsync(id);
        if (employee == null) return NotFound();

        var definitions = await _propertyService.GetAllAsync();

        var viewModel = new EditEmployeeViewModel
        {
            Id = employee.Id,
            Code = employee.Code,
            Name = employee.Name,
            PropertyDefinitions = definitions.ToList(),
            PropertyValues = employee.PropertyValues.ToDictionary(
                pv => pv.PropertyDefinitionId,
                pv => pv.Value)
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditEmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var definitions = await _propertyService.GetAllAsync();
            model.PropertyDefinitions = definitions.ToList();
            return View(model);
        }

        // Check if another employee with the same code exists (excluding the current employee)
        var existingEmployee = await _employeeService.GetByCodeAsync(model.Code);
        if (existingEmployee != null && existingEmployee.Id != model.Id)
        {
            ModelState.AddModelError("Code", "An employee with this code already exists.");
            var definitions = await _propertyService.GetAllAsync();
            model.PropertyDefinitions = definitions.ToList();
            return View(model);
        }

        await _employeeService.UpdateAsync(model.Id, model.Code, model.Name, model.PropertyValues ?? new Dictionary<int, string>());

        return RedirectToAction(nameof(Index));
    }
}
