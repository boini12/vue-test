using System.Text.Json;
using backend.Model;
using Microsoft.AspNetCore.WebUtilities;

namespace backend.Clients;

public class TrainApiClient(HttpClient client, ILogger<TrainApiClient> logger) : ITrainApiClient
{
    public async Task<List<Journey>> FetchJourneysAsync(
        string from,
        string to,
        JourneyTimeSelection journeyTimeSelection,
        DateTime travelDate)
    {
       var timeKey = journeyTimeSelection == JourneyTimeSelection.Departure 
           ? "departure"
           : "arrival";

       var parameters = new Dictionary<string, string>
       {
            ["from"] = from,
            ["to"] = to,
            [timeKey] = travelDate.ToString("o"),
            ["nationExpress"] = "true",
            ["regionalExpress"] = "true",
            ["regional"] = "true",
            ["suburban"] = "false",
            ["bus"] = "false",
            ["ferry"] = "false",
            ["subway"] = "false",
            ["tram"] = "false",
            ["taxi"] = "false",
            ["remarks"] = "false",
            ["entrances"] = "false",
            ["subStops"] = "false",
            ["startWalkingWith"] = "false",
            ["results"] = "5",
            ["stopovers"] = "true",
       };
       
       var path = QueryHelpers.AddQueryString("journeys", parameters!);
       var fullUrl = new Uri(client.BaseAddress!, path);

       HttpResponseMessage response;
       try
       {
           response = await client.GetAsync(fullUrl);
       }
       catch (Exception ex)
       {
           logger.LogError(ex, "Error fetching journeys");
           return new List<Journey>();
       }

       if (!response.IsSuccessStatusCode)
       {
           logger.LogError("Error fetching journeys");
           return new List<Journey>();
       }
       
       logger.LogInformation("Journeys fetched");
       
       var json = await response.Content.ReadAsStringAsync();
       try
       {
           var result = JsonSerializer.Deserialize<JourneyResponse>(json, new JsonSerializerOptions
           {
               PropertyNameCaseInsensitive = true
           });
           
           return result?.Journeys ?? new List<Journey>();;
       }
       catch (Exception ex)
       {
           logger.LogError(ex, "Error fetching journeys");
           return new List<Journey>();
       }
    }

    public async Task<string> FetchStationId(string stationName)
    {
        var query = new Dictionary<string, string>
        {
            ["query"] = stationName,
            ["results"] = "1"
        };
        
        var path = QueryHelpers.AddQueryString("locations", query!);
        var fullUrl = new Uri(client.BaseAddress!, path);

        HttpResponseMessage response;

        try
        {
            response = await client.GetAsync(fullUrl);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching Journeys");
            return string.Empty;
        }

        if (!response.IsSuccessStatusCode)
            return string.Empty;
        
        logger.LogInformation("Journeys fetched");
        
        var data = await response.Content.ReadAsStringAsync();

        try
        {
            // API returns an array of stations, even though it will only hold one item
            // because the id is unique.
            var stationObjs = JsonSerializer.Deserialize<List<Station>>(data, new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true }
            );
            return stationObjs?.FirstOrDefault()?.Id ?? string.Empty;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching Station");
            return string.Empty;
        }
        return data;
    }
}