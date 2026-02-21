using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly ITrainService _service;

    public TrainController(ITrainService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetJournery()
    {
        var result = await _service.GetJourneysAsync();
        return Ok(result);
    }

    [HttpGet("id/{station}")]
    public async Task<IActionResult> GetStationId(string station)
    {
        var id = await _service.GetStationIdAsync(station);
        return Ok(id);
    }
}