using Backend.Data;
using Backend.Models;
using Backend.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Database>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Othello")));

builder.Services
    .AddTransient<IRepository, Repository>()
    .AddTransient<IGameRepository, GameAccessLayer>()
    .AddTransient<IResultRepository, ResultAccessLayer>()
    .AddTransient<IPlayerRepository, PlayerAccessLayer>()
    .AddTransient<GameController>()
    .AddTransient<ResultController>()
    .AddTransient<PlayerController>()
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new ColorArrayConverter());
    });


builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

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
