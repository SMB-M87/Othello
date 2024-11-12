using API.Controllers;
using API.Data;
using API.Models;
using API.Service;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Database>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("API")));

builder.Services
    .AddTransient<IRepository, Repository>()
    .AddTransient<IGameRepository, GameAccessLayer>()
    .AddTransient<IResultRepository, ResultAccessLayer>()
    .AddTransient<IPlayerRepository, PlayerAccessLayer>()
    .AddTransient<IHomeRepository, HomeAccessLayer>()
    .AddTransient<GameController>()
    .AddTransient<ResultController>()
    .AddTransient<PlayerController>()
    .AddTransient<HomeController>()
    .AddTransient<MediatorController>()
    .AddTransient<AdminController>()
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new ColorArrayConverter());
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.Cookie.Name = ".AspNet.SharedAuthCookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.Domain = "localhost";
        options.Cookie.Path = "/";
    });

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\SharedKeys"))
    .SetApplicationName("Othello");

builder.Services.AddScoped<BotService>();
builder.Services.AddHostedService<BotGameService>();
builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddHostedService<CleanUpService>();
builder.Services.AddHostedService<GameService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();

    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        await next();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
