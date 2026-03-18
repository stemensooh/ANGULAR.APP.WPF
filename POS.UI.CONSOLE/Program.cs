using Microsoft.AspNetCore.Diagnostics;
using POS.APPLICATION.AppServices;
using POS.APPLICATION.Interfaces.AppServices;
using POS.UI.CONSOLE.Config;
using POS.UI.CONSOLE.Endpoints;
using Serilog;

Directory.SetCurrentDirectory(AppContext.BaseDirectory);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(
        Path.Combine(AppContext.BaseDirectory, "logs", "service-.log"),
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();
builder.Host.UseSerilog();

builder.Services.Configure<PrinterServiceOptions>(
    builder.Configuration.GetSection("PrinterService")
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

builder.Services.AddSingleton<IPrintAppService, PrintAppService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        Log.Error(error, "Unhandled exception");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal server error");
    });
});

app.UseCors("all");

app.MapPrinterEndpoints();

var options = app.Services
    .GetRequiredService<Microsoft.Extensions.Options.IOptions<PrinterServiceOptions>>()
    .Value;

try
{
    app.Run($"http://localhost:{options.HttpPort}");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Service crashed");
}
finally
{
    Log.CloseAndFlush();
}

