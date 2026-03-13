using POS.UI.CONSOLE.Config;
using POS.UI.CONSOLE.Endpoints;
using POS.APPLICATION.AppServices;
using POS.APPLICATION.Interfaces.AppServices;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseWindowsService();
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

app.UseCors("all");

app.MapPrinterEndpoints();

var options = app.Services
    .GetRequiredService<Microsoft.Extensions.Options.IOptions<PrinterServiceOptions>>()
    .Value;

app.Run($"http://localhost:{options.HttpPort}");