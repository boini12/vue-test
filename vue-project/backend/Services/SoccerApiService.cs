using backend.Clients;
using backend.Model;

namespace backend.Services;

public class SoccerApiService(ISoccerApiClient client) : ISoccerApiService
{
    public Task<List<Event>> GetBundesligaMatchesAsync(DateTime date) => client.FetchBundesligaMatchesAsync(date);
    public Task<Venue?> GetVenueAsync(string venueId) => client.FetchVenueAsync(venueId);
}