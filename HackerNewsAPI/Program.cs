using HackerNewsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<HttpClient>();
builder.Services.AddTransient<HackerNewsService>();
builder.Services.AddTransient<CachedHackerNewsService>();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapControllers();

app.Run();

public partial class Program { }

