using Application.Extensions;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddServices();

builder.Services.AddDbContext<SqlServerDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MicrosoftSqlServer"));
});

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.InstanceName = "RedisCacheInstance";
	options.ConfigurationOptions = new ConfigurationOptions()
	{
		EndPoints = { "127.0.0.1", "6379" },
	};
	// options.Configuration = builder.Configuration.GetConnectionString("RedisCacheServer");
});

builder.Services.AddRepositories();

builder.Services.AddHealthChecks();

builder.Services.AddRouting(options =>
{
	options.LowercaseUrls = true;
	options.LowercaseQueryStrings = true;
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapHealthChecks("/api/health");

app.MapControllers();

app.MapOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "v1");
	});

	app.UseReDoc();
}

app.UseHttpsRedirection();

app.Run();

