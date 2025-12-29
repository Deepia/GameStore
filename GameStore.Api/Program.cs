using System.Reflection.Metadata.Ecma335;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
var app = builder.Build();
app.MapGamesEndPoints();
app.Run();
