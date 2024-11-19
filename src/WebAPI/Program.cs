using Application;
using Infrastructure;
using NLog;
using NLog.Web;
using WebAPI.Extensions;
using WebAPI.Middlewares;

var logger = LogManager.GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services
        .AddApplicationServices()
        .AddInfrastructure();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddCustomSwagger();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsTesting())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.InitializeDatabase();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<NLogRequestPostedBodyMiddleware>
       (new NLogRequestPostedBodyMiddlewareOptions());

    app.UseMiddleware<ExceptionMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Programa detenido por excepción");
    throw;
}
finally
{
    LogManager.Shutdown();
}

public partial class Program { }