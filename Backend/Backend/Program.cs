using Backend.Controllers;
using Backend.Models;
using Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository, Repository>()
    .AddTransient<IGameRepository, GameRepository>()
    .AddTransient<IResultRepository, ResultRepository>()
    .AddTransient<IPlayerRepository, PlayerRepository>()
    .AddTransient<GameController>()
    .AddTransient<ResultController>()
    .AddTransient<PlayerController>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new ColorArrayConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
