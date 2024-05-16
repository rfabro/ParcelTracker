using System.Text.Json.Serialization;
using ParcelTracker.API;
using ParcelTracker.Application;
using ParcelTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// register services
builder.Services.AddLogging();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.IgnoreNullValues = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(configuration);
builder.Services.AddFeatures(configuration);
builder.Services.AddInfrastructureServices(configuration);

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