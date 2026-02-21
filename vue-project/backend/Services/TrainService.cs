using backend.Clients;
using backend.Model;

namespace backend.Services;

public class TrainService : ITrainService
{
    private ITrainApiClient _client;
    
    public TrainService(ITrainApiClient client)
    {
        _client = client;
    }

    public Task<JourneyResponse> GetJourneysAsync()
    {
        return null;
    }

    public Task<string> GetStationIdAsync(string station) => _client.FetchStationId(station);
} 