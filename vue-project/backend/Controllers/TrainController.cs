using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController(ITrainService service) : ControllerBase
{
    [HttpGet("id/{journeys")]
    public async Task<IActionResult> GetJourneysAsync()
    {
        string from = "Berlin Hbf";
        string to = "Hamburg Hbf";
        JourneyTimeSelection journeyTimeSelection = JourneyTimeSelection.Arrival;
        DateTime travelDate = DateTime.Today;
        
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