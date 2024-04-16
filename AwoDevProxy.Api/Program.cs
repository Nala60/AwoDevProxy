using AwoDevProxy.Api.Middleware;
using AwoDevProxy.Api.Proxy;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
config.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(config.GetSection("ProxyConfig").Get<ProxyConfig>());
builder.Services.AddSingleton<IProxyManager, ProxyManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseWebSockets();
app.UseMiddleware<ProxyRootingMiddleware>();

app.MapControllers();

app.Run();
