using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Ticketer.Business;
using Ticketer.Business.Services;
using Ticketer.Business.Validators;
using Ticketer.WebApp.Extensions;

namespace Ticketer.WebApp;

public class Program
{
    static void ConfigureServices(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var dbConfig = new DbConfig();
        configuration.GetSection(DbConfig.SectionName).Bind(dbConfig);

        services.AddValidatorsFromAssemblyContaining<EventValidator>();
        services.AddFluentValidationAutoValidation();

        services.AddDbContext<TicketerDbContext>(options => options.UseNpgsql(dbConfig.ToString()));
        services.AddScoped<EventService>();
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName: environment.ApplicationName))
            .WithMetrics(metrics =>
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddPrometheusExporter()
            );
    }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

        var app = builder.Build();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            await app.EnsureDbMigrationsAsync<TicketerDbContext>();
        }

        app.MapPrometheusScrapingEndpoint();
        await app.RunAsync();
    }
}