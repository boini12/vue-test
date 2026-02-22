using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController(ITrainService service) : ControllerBase
{
    [HttpGet("id/{journeys")]
    public async Task<IActionResult> GetJourneysAsync()
    {
        var result = await service.GetJourneysAsync();
        return Ok(result);
    }

    [HttpGet("id/{station}")]
    public async Task<IActionResult> GetStationId(string station)
    {
        var id = await service.GetStationIdAsync(station);
        return Ok(id);
    }
}