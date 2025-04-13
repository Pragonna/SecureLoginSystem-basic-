using Core.Application;
using Core.Infrastructure;
using EventBus.RabbitMQ;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddApplication(RabbitMQOptions.GetConfigRabbitMQ);
builder.Services.AddInfrastructure(builder.Configuration);


// Add loggers
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
/*    .WriteTo.Elasticsearch()
    .WriteTo.Seq()
    .WriteTo.Sentry()
    .WriteTo.ApplicationInsights()*/
    .CreateLogger();

// Add Serilog to the logging pipeline
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
