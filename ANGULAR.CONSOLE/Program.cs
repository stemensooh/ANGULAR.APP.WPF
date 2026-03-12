using ANGULAR.CONSOLE.Services;
using ANGULAR.CONSOLE.Endpoints;
using ANGULAR.CONSOLE.Config;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddSingleton<PrinterService>();

var app = builder.Build();

app.UseCors("all");

app.MapPrinterEndpoints();

var options = app.Services
    .GetRequiredService<Microsoft.Extensions.Options.IOptions<PrinterServiceOptions>>()
    .Value;

app.Run($"http://localhost:{options.HttpPort}");