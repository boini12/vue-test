using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController(ITrainService service) : ControllerBase
{
    [HttpGet("journeys")]
    public async Task<IActionResult> GetJourneysAsync(
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] JourneyTimeSelection journeyTimeSelection,
        [FromQuery] DateTime travelDate)

    {
        var result = await service.GetJourneysAsync(from, to, journeyTimeSelection, travelDate);
        return Ok(result);
    }

    [HttpGet("id/{station}")]
    public async Task<IActionResult> GetStationId(string station)
    {
        var id = await service.GetStationIdAsync(station);
        return Ok(id);
    }
}