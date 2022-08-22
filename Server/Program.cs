using APIV1.Infra;
using APIV2.Infra;
using ServerInfra.Installers;
using ServerInfra.Interfaces;

// CONST
var versions = new IAPIVersionMarker[] { new Version1Marker(), new Version2Marker() };

// SERVICES
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwagger();

// APP
var app = builder.Build();

// Configure the HTTP request pipeline


app.UseHttpsRedirection();

app.InstallControllersFromAssemblyWithMarkers(versions);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
