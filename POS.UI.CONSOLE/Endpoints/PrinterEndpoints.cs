using POS.APPLICATION.Interfaces.AppServices;
using POS.APPLICATION.Models;

namespace POS.UI.CONSOLE.Endpoints;

public static class PrinterEndpoints
{
    public static void MapPrinterEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Printer service running");

        app.MapGet("/whoami", () =>
        {
            return Environment.UserName;
        });

        app.MapGet("/printers", (IPrintAppService printerService) =>
        {
            return printerService.GetPrinters();
        });

        app.MapPost("/print", (PrintRequest request, IPrintAppService printerService) =>
        {
            printerService.Print(request.PrinterName!, request.Text!);
            return Results.Ok("Printed");
        });

        app.MapGet("/status", () =>
        {
            return Results.Ok("OK");
        });

        app.MapPost("/print-ticket", (TicketRequest request, IPrintAppService printerService) =>
        {
            printerService.PrintTicket(request.PrinterName!, request.Lines!);
            return Results.Ok("Printed");
        });

        app.MapPost("/print-sample", (string printerName, IPrintAppService printerService) =>
        {
            printerService.PrintSampleTicket(printerName);
            return Results.Ok();
        });
    }
}