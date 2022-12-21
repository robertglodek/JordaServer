using Hangfire;
using Jorda.Application;
using Jorda.Infrastructure;
using Jorda.Infrastructure.Persistance.Initializer;
using Jorda.Server.Application.Common.Jobs;
using Jorda.Server.WebUi.Middleware;
using Jorda.WebUi;
using Serilog;

//Application startup logging config.
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Application starting!");

try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add serilog based on configuration.
    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
    // Add services to the container.
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddWebUIServices(builder.Configuration);

    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider.GetRequiredService<IDbInitialiser>();
        await service.InitialiseAsync();
        await service.SeedAsync();
    }

    // Configure the HTTP request pipeline.

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseCors("FrontEndClients");
    app.UseMiddleware<RequestCultureMiddleware>();

    app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jorda.Api"));
    }
    
    app.UseHangfireDashboard();

    RecurringJob.AddOrUpdate<DetermineUsersTodosCompletionJob>(x => x.Execute(), Cron.Daily(0, 0));
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapHangfireDashboard();
    app.Run();



}
catch (Exception ex)
{
    //Comment logging error when adding new migrations or updating database
    Log.Fatal(ex, "Something went wrong!");

}
finally
{
    Log.CloseAndFlush();
}