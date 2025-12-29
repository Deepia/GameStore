using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";
  private static readonly List<GameDto> games = [
    new (1, "Street Fighter 1", "Fightings 1", 19.99M, new DateOnly(1999,11,23)),
    new (2, "Street Fighter 2", "Fighting 2", 29.99M, new DateOnly(1999,12,23)),
    new (3, "Street Fighter 3", "Fighting 3", 49.99M, new DateOnly(1999,10,23))
];

public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        group.MapGet("/", () => games);



group.MapGet("/{id}", (int id) =>
{
    var game = games.Find((item)=>item.Id == id);
    return game is null ?  Results.NotFound() : Results.Ok(game);
}).WithName(GetGameEndpointName);

group.MapPost("/",(CreateGameDto obj) =>
{
    GameDto gameDto = new (games.Count +1, obj.Name, obj.Genre, obj.Price, obj.ReleaseDate);
    games.Add(gameDto);
    return Results.CreatedAtRoute(GetGameEndpointName, new {id = gameDto.Id}, gameDto);
});
group.MapPut("/{id}",(int id, UpdateGameDto updateGameDto) =>
{
    var index = games.FindIndex(item=> item.Id == id);
    if(index == -1)
    {
        return Results.NotFound();
    }
    games[index] = new (id, updateGameDto.Name, updateGameDto.Genre, updateGameDto.Price, updateGameDto.ReleaseDate);
    return Results.NoContent();
    
});
group.MapDelete("/{id}", (int id) =>
{
    games.RemoveAll(item=>item.Id == id);
    return Results.NoContent();
});
    }

}
