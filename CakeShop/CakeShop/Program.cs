using CakeShop.BL.MediatRCommandHandlers.ClientHandlers;
using CakeShop.Extensions;
using CakeShop.HealthChecks;
using CakeShop.Middleware;
using CakeShop.Models.Models.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);



builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.RegisterRepositories()
                .RegisterServices();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(nameof(MongoDbConfiguration)));
builder.Services.Configure<KafkaProducerSettings>(builder.Configuration.GetSection(nameof(KafkaProducerSettings)));
builder.Services.Configure<KafkaConsumerSettings>(builder.Configuration.GetSection(nameof(KafkaConsumerSettings)));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateClientHandler).Assembly);

builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<MongoHealthCheck>("Mongo Server");

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15000); //time in seconds between check
    opt.MaximumHistoryEntriesPerEndpoint(1); //maximum history of checks
    opt.SetApiMaxActiveRequests(1); //api requests concurrency

    opt.AddHealthCheckEndpoint("healthz", "/healthz"); //map health check api
})
        .AddInMemoryStorage();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggHandlerMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    endpoints.MapHealthChecksUI();

    endpoints.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
});

WebHost.CreateDefaultBuilder(args)
        .UseStartup<Program>()
        .ConfigureLogging((ctx, logging) =>
        {
            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
        });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
