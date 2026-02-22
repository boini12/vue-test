using backend.Model;

namespace backend.Services;

public interface ITrainService
{
    Task<List<JourneyResponse>> GetJourneysAsync(
        string from,
        string to,
        JourneyTimeSelection journeyTimeSelection,
        DateTime travelDate
    );
    Task<string> GetStationIdAsync(string station);
}