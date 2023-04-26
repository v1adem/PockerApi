using PockerApi.Models;
using PockerApi.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PockerDBSettings>(
    builder.Configuration.GetSection("PockerDB"));


builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<DeckService>();

builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
