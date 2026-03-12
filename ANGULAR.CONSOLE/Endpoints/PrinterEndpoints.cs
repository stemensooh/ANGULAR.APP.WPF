using ANGULAR.CONSOLE.Models;
using ANGULAR.CONSOLE.Services;

namespace ANGULAR.CONSOLE.Endpoints;

public static class PrinterEndpoints
{
    public static void MapPrinterEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Printer service running");

        app.MapGet("/printers", (PrinterService printerService) =>
        {
            return printerService.GetPrinters();
        });

        app.MapPost("/print", (PrintRequest request, PrinterService printerService) =>
        {
            printerService.Print(request.PrinterName!, request.Text!);
            return Results.Ok("Printed");
        });

        app.MapGet("/status", () =>
        {
            return Results.Ok("OK");
        });

        app.MapPost("/print-ticket", (TicketRequest request, PrinterService printer) =>
        {
            printer.PrintTicket(request.PrinterName!, request.Lines!);
            return Results.Ok("Printed");
        });

        app.MapPost("/print-sample", (string printerName, PrinterService printer) =>
        {
            printer.PrintSampleTicket(printerName);
            return Results.Ok();
        });
    }
}