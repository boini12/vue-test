using backend.Model;

namespace backend.Services;

public interface ITrainService
{
    Task<JourneyResponse> GetJourneysAsync();
    Task<string> GetStationIdAsync(string station);
}