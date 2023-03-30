using CryptoManager.WebApi;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder.Services, builder.Environment);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();