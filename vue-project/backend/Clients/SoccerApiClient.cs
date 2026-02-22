using System.Text.Json;
using backend.Model;
using Microsoft.AspNetCore.WebUtilities;

namespace backend.Clients;

public class SoccerApiClient(HttpClient client, ILogger<SoccerApiClient> logger) : ISoccerApiClient
{
    private const string LeagueQuery = "league";
    
    public async Task<List<Event>> FetchBundesligaMatchesAsync(DateTime date)
    {
        var query = new Dictionary<string, string>
        {
            ["d"] = date.ToString("yyyy-MM-dd"),
            ["l"] = LeagueQuery,
        };
        
        var url = QueryHelpers.AddQueryString("eventsday.php?", query!);
        
        HttpResponseMessage response;
        try
        {
            response = await client.GetAsync(url);
        }
        catch (Exception ex)
        {
            logger.LogError($"SoccerApiClient.{url}: Error: {ex.Message}");
            return new List<Event>();
        }
        
        if (!response.IsSuccessStatusCode)
            return new List<Event>();
        
        logger.LogInformation($"SoccerApiClient.{url}: Success");
        
        var data = await response.Content.ReadAsStringAsync();

        try
        {
            var eventResponse = JsonSerializer.Deserialize<EventResponse>(data, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true }
            );
            return eventResponse?.Events ?? new List<Event>();;
        }
        catch (Exception ex)
        {
            logger.LogError($"SoccerApiClient.{url}: Error: {ex.Message}");
            return new List<Event>();
        }
    }

    public async Task<Venue?> FetchVenueAsync(string id)
    {
        var query = new Dictionary<string, string>
        {
            ["id"] = id,
        };
        
        var url = QueryHelpers.AddQueryString("lookupvenue.php?", query!);
        
        HttpResponseMessage response;
        try
        {
            response = await client.GetAsync(url);
        }
        catch (Exception ex)
        {
            logger.LogError($"SoccerApiClient.{url}: Error: {ex.Message}");
            return null;
        }
        
        if (!response.IsSuccessStatusCode)
            return null;
        
        logger.LogInformation($"SoccerApiClient.{url}: Success");
        
        var data = await response.Content.ReadAsStringAsync();

        try
        {
            var venueResponse = JsonSerializer.Deserialize<VenueResponse>(data, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true }
            );
            
            return venueResponse?.Venues.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}