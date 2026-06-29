using backend.Clients;
using backend.Model;

namespace backend.Services;

public class TrainService(ITrainApiClient client) : ITrainService
{
    public async Task<List<Journey>> GetJourneysAsync(
        string from,
        string to,
        JourneyTimeSelection journeyTimeSelection,
        DateTime travelDate
        )
    {
        var fromId = await client.FetchStationId(from);
        var toId = await client.FetchStationId(to);

        if (string.IsNullOrEmpty(fromId) || string.IsNullOrEmpty(toId))
            return new List<Journey>();

        return await client.FetchJourneysAsync(fromId, toId, journeyTimeSelection, travelDate);
    }

    public Task<string> GetStationIdAsync(string station) => client.FetchStationId(station);
} 