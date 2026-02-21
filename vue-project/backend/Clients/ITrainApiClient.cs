using backend.Model;

namespace backend.Clients;

public interface ITrainApiClient
{
    Task<List<JourneyResponse>> FetchJourneysAsync(
        string from,
        string to,
        JourneyTimeSelection journeyTimeSelection,
        DateTime travelDate);
    Task<string> FetchStationId(string stationName);
}