using backend.Clients;
using backend.Model;
using backend.Services;
using backend.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TrainApiSettings>(builder.Configuration.GetSection("TrainApi"));

builder.Services.AddControllers();

// Typed HttpClients
builder.Services.AddHttpClient<ITrainApiClient, TrainApiClient>(client => client.BaseAddress = new Uri(builder.Configuration["TrainApi:BaseUrl"]));

// Domain Services
builder.Services.AddScoped<ITrainService, TrainService>();

var app = builder.Build();

// Run a test request using DI
using (var scope = app.Services.CreateScope())
{
    var client = scope.ServiceProvider.GetRequiredService<ITrainApiClient>();
    var result = await client.FetchJourneysAsync("Berlin Hbf", "Hamburg Hbf", JourneyTimeSelection.Departure, DateTime.Now); 
    Console.WriteLine($"Test result: {result}");
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
