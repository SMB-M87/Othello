using API.Controllers;
using API.Data;
using API.Models;
using API.Service;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Database>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("API")));

builder.Services
    .AddTransient<IRepository, Repository>()
    .AddTransient<IGameRepository, GameAccessLayer>()
    .AddTransient<IResultRepository, ResultAccessLayer>()
    .AddTransient<IPlayerRepository, PlayerAccessLayer>()
    .AddTransient<IUserRepository, UserAccessLayer>()
    .AddTransient<ILogRepository, LogAccessLayer>()
    .AddTransient<RegisterController>()
    .AddTransient<MiddlewareController>()
    .AddTransient<LogController>()
    .AddTransient<PlayerController>()
    .AddTransient<GameController>()
    .AddTransient<ResultController>()
    .AddTransient<UserController>()
    .AddTransient<ModController>()
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
        options.Cookie.Name = "__Host-SharedAuthCookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.Path = "/";
    });

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\SharedKeys"/*@"/var/othello"*/))
    .SetApplicationName("Othello")
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "__Host-Antiforgery";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddScoped<BotService>();
builder.Services.AddHostedService<BotGameService>();
builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddHostedService<CleanUpService>();
builder.Services.AddHostedService<GameService>();

builder.WebHost.UseUrls("http://127.0.0.1:7023");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7269"/*"https://othello.hbo-ict.org"*/)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

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

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigin");

app.Run();
