using backend.Clients;
using backend.Model;

namespace backend.Services;

public class TrainService(ITrainApiClient client) : ITrainService
{
    public Task<JourneyResponse> GetJourneysAsync()
    {
        return null;
    }

    public Task<string> GetStationIdAsync(string station) => client.FetchStationId(station);
} 