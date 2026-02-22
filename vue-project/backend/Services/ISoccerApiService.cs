using backend.Model;

namespace backend.Services;

public interface ISoccerApiService
{
    Task<List<Event>> GetBundesligaMatchesAsync(DateTime date);
    Task<Venue?> GetVenueAsync(string venueId);
}