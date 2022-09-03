using Handlers.WeatherForecast;
using Shared.Requests;

//CONSTS
const string ClientPolicyName = "blazor-client";

// SERVICES
var builder = WebApplication.CreateBuilder(args);

// SWAGGER
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MEDIATR
builder.Services.AddMediatR(typeof(GetWeatherForecastsQueryHandler));

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:3000", "http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// APP
var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.InstallEndpoints(typeof(GetWeatherForecastsQuery), typeof(GetWeatherForecastsQueryHandler));

app.MapPost("WeatherForecasts", async (IMediator mediator, GetWeatherForecastsQuery input) => await mediator.Send(input));

app.Run();
