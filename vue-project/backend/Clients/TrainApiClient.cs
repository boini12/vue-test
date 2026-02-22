using System.Text.Json;
using backend.Model;
using Microsoft.AspNetCore.WebUtilities;

namespace backend.Clients;

public class TrainApiClient(HttpClient client, ILogger<TrainApiClient> logger) : ITrainApiClient
{
    public async Task<List<JourneyResponse>> FetchJourneysAsync(
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
       
       var url = QueryHelpers.AddQueryString("journeys", parameters!);

       HttpResponseMessage response;
       try
       {
           response = await client.GetAsync(url);
       }
       catch (Exception ex)
       {
           logger.LogError(ex, "Error fetching journeys");
           return new List<JourneyResponse>();
       }

       if (!response.IsSuccessStatusCode)
       {
           logger.LogError("Error fetching journeys");
           return new List<JourneyResponse>();
       }
       
       var json = await response.Content.ReadAsStringAsync();
       try
       {
           var result = JsonSerializer.Deserialize<JourneyResponse>(json, new JsonSerializerOptions
           {
               PropertyNameCaseInsensitive = true
           });
           
           return result?.Journeys ?? new List<JourneyResponse>();;
       }
       catch (Exception ex)
       {
           logger.LogError(ex, "Error fetching journeys");
           return new List<JourneyResponse>();
       }
    }

    public async Task<string> FetchStationId(string stationName)
    {
        var query = new Dictionary<string, string>
        {
            ["query"] = stationName,
            ["results"] = "1"
        };
        
        var url = QueryHelpers.AddQueryString("locations", query);

        HttpResponseMessage response;

        try
        {
            response = await client.GetAsync(url);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching Journeys");
            return string.Empty;
        }
        
        logger.LogInformation("Journeys fetched");

        if (!response.IsSuccessStatusCode)
            return string.Empty;
        
        var data = await response.Content.ReadAsStringAsync();

        try
        {
            // API returns an array of stations, even though it will only hold one item
            // because the Id is unqiue.
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