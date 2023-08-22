using Polly;
using StoneApi.Clients;
using StoneManager.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IStoneManagerClient, StoneManagerClient>()
    .AddScoped<IStonehengeClient, StonehengeClient>();

builder.Services
    .AddGrpcClient<StoneService.StoneServiceClient>(o =>
    {
        string? connectionString = builder.Configuration.GetConnectionString("StoneManagerClient");
        o.Address = new Uri(connectionString!);
    });

builder.Services
    .AddHttpClient(nameof(StonehengeClient), client =>
    {
        string? stonehengeConnectionString = builder.Configuration.GetConnectionString("StonehengeClient");
        client.BaseAddress = new Uri(stonehengeConnectionString!);
    })
    .AddTransientHttpErrorPolicy(x =>
        x.CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 2,
            durationOfBreak: TimeSpan.FromSeconds(10),
            onBreak: (result, span) => { Console.WriteLine("Polly break for {0}.", span); },
            onReset: () => { Console.WriteLine("Polly reset."); }
    ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
