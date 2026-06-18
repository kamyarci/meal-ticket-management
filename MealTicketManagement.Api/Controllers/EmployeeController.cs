using MealTicketManagement.Application.DTOs.Employee.Request;
using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.Interfaces.Employee;
using Microsoft.AspNetCore.Mvc;

namespace MealTicketManagement.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly ICreateEmployeeUseCase _createEmployeeUseCase;
    private readonly IUpdateEmployeeUseCase _updateEmployeeUseCase;
    private readonly IGetEmployeeByIdUseCase _getEmployeeByIdUseCase;
    private readonly IGetAllEmployeesUseCase _getAllEmployeesUseCase;

    public EmployeeController(ILogger<EmployeeController> logger, ICreateEmployeeUseCase createEmployeeUseCase,
        IUpdateEmployeeUseCase updateEmployeeUseCase, IGetEmployeeByIdUseCase getEmployeeByIdUseCase,
        IGetAllEmployeesUseCase getAllEmployeesUseCase)
    {
        _logger = logger;
        _createEmployeeUseCase = createEmployeeUseCase;
        _updateEmployeeUseCase = updateEmployeeUseCase;
        _getEmployeeByIdUseCase = getEmployeeByIdUseCase;
        _getAllEmployeesUseCase = getAllEmployeesUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        EmployeeResponse result = await _createEmployeeUseCase.Execute(request);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeRequest request)
    {
        EmployeeResponse result = await _updateEmployeeUseCase.Execute(id, request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        IEnumerable<EmployeeResponse> result = await _getAllEmployeesUseCase.Execute();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        EmployeeResponse result = await _getEmployeeByIdUseCase.Execute(id);
        return Ok(result);
    }
}