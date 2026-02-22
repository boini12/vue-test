using backend.Model;

namespace backend.Clients;

public interface ISoccerApiClient
{
    Task<List<Event>> FetchBundesligaMatchesAsync(DateTime date);
    Task<Venue?> FetchVenueAsync(string id);
}