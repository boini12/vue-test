using backend.Model;

namespace backend.Clients;

public interface ITrainApiClient
{
    Task<List<Journey>> FetchJourneysAsync(
        string from,
        string to,
        JourneyTimeSelection journeyTimeSelection,
        DateTime travelDate);
    Task<string> FetchStationId(string stationName);
}