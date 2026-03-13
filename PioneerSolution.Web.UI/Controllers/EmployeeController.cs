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

    // GET: Employee
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

    // GET: Employee/Create
    public async Task<IActionResult> Create()
    {
        var definitions = await _propertyService.GetAllAsync();

        var viewModel = new AddEmployeeViewModel
        {
            PropertyDefinitions = definitions.ToList()
        };

        return View(viewModel);
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddEmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Re-fetch definitions for the form re-render
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

    // GET: Employee/Edit/5
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

    // POST: Employee/Edit/5
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

        await _employeeService.UpdateAsync(model.Id, model.Code, model.Name, model.PropertyValues ?? new Dictionary<int, string>());

        return RedirectToAction(nameof(Index));
    }
}
