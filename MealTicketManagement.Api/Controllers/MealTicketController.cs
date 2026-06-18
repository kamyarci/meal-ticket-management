using MealTicketManagement.Application.DTOs.MealTicket.Request;
using MealTicketManagement.Application.DTOs.MealTicket.Response;
using MealTicketManagement.Application.DTOs.Report;
using MealTicketManagement.Application.Interfaces.MealTicket;
using Microsoft.AspNetCore.Mvc;

namespace MealTicketManagement.Api.Controllers;

[ApiController]
[Route("api/mealtickets")]
public class MealTicketController : ControllerBase
{
    private readonly ILogger<MealTicketController> _logger;
    private readonly ICreateMealTicketUseCase _createMealTicketUseCase;
    private readonly IUpdateMealTicketUseCase _updateMealTicketUseCase;
    private readonly IGetMealTicketByIdUseCase _getMealTicketByIdUseCase;
    private readonly IGetTicketReportUseCase _getTicketReportUseCase;

    public MealTicketController(ILogger<MealTicketController> logger, ICreateMealTicketUseCase createMealTicketUseCase,
        IUpdateMealTicketUseCase updateMealTicketUseCase, IGetMealTicketByIdUseCase getMealTicketByIdUseCase,
        IGetTicketReportUseCase getTicketReportUseCase)
    {
        _logger = logger;
        _createMealTicketUseCase = createMealTicketUseCase;
        _updateMealTicketUseCase = updateMealTicketUseCase;
        _getMealTicketByIdUseCase = getMealTicketByIdUseCase;
        _getTicketReportUseCase = getTicketReportUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMealTicket([FromBody] CreateMealTicketRequest request)
    {
        MealTicketResponse result = await _createMealTicketUseCase.Execute(request);
        return CreatedAtAction(nameof(GetMealTicketById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMealTicket(Guid id, [FromBody] UpdateMealTicketRequest request)
    {
        MealTicketResponse result = await _updateMealTicketUseCase.Execute(id, request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMealTicketById(Guid id)
    {
        MealTicketResponse result = await _getMealTicketByIdUseCase.Execute(id);
        return Ok(result);
    }

    [HttpGet("report")]
    public async Task<IActionResult> GetTicketReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        TicketReportResponse result = await _getTicketReportUseCase.Execute(startDate, endDate);
        return Ok(result);
    }
}