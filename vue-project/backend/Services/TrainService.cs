using backend.Clients;
using backend.Model;

namespace backend.Services;

public class TrainService(ITrainApiClient client) : ITrainService
{
    public Task<List<JourneyResponse>> GetJourneysAsync(
        string from, 
        string to, 
        JourneyTimeSelection journeyTimeSelection, 
        DateTime travelDate
        ) 
        => client.FetchJourneysAsync(from, to, journeyTimeSelection, travelDate);

    public Task<string> GetStationIdAsync(string station) => client.FetchStationId(station);
} 