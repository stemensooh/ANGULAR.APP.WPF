using Newtonsoft.Json;
using POS.APPLICATION.Constanst;
using POS.APPLICATION.Dto;
using POS.APPLICATION.Dto.Ticket;
using POS.APPLICATION.Helpers;
using POS.APPLICATION.Interfaces.AppServices;
using POS.APPLICATION.Utilities;
using Serilog;
using System.Drawing;
using System.Drawing.Printing;

namespace POS.APPLICATION.AppServices
{
    public class PrintAppService : IPrintAppService
    {
        public IEnumerable<string> GetPrinters()
        {
            return PrinterSettings.InstalledPrinters.Cast<string>();
        }

        public void Print(string printerName, string text)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printerName;

            pd.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(text, new Font("Arial", 12), Brushes.Black, new PointF(10, 10));
            };

            pd.Print();
        }

        public void PrintRaw(string printerName, byte[] bytes)
        {
            RawPrinterHelper.SendBytesToPrinter(printerName, bytes);
        }

        public void PrintTicket(string printerName, List<string> lines)
        {
            var builder = new EscPosBuilder()
                .Initialize()
                .Center()
                .Bold()
                .DoubleSize()
                .Text("MI NEGOCIO")
                .NormalSize()
                .Bold(false)
                .Text("----------------------")
                .Left();

            foreach (var line in lines)
                builder.Text(line);

            var bytes = builder
                .Feed(3)
                .Cut()
                .Build();

            PrintRaw(printerName, bytes);
        }

        public void PrintTicket(string printerName, TicketDto ticketDto)
        {
            try
            {
                Log.Information("Printing to {Printer}", printerName);

                var config = JsonConvert.DeserializeObject<ConfigDto>(File.ReadAllText(Path.Combine(PrintConstants.ProgramData, "config.json")));
                if (config == null)
                {
                    throw new Exception("No se pudo cargar la configuración de impresión.");
                }

                var ticket = new TicketBuilder();

                ticket.OpenDrawer();
                ticket.Emisor(ticketDto.Emisor);
                
                ticket
                    .Separator()
                    .HeaderItems()
                    .Separator();

                ticketDto.Items?.ForEach(item =>
                {
                    ticket.Item(item.Cantidad ?? 0, item.Descripcion!, item.PrecioUnitario ?? 0, item.Subtotal ?? 0);
                    item.Adicional?.ForEach(mod => ticket.Modifier($"{mod.Nombre}: {mod.Valor}"));
                });


                ticket
                    .Separator()
                    .Subtotal(ticketDto.Subtotal ?? 0)
                    .Impuestos(ticketDto.Impuestos)
                    .Total(ticketDto.Total ?? 0)
                    .Separator();

                ticket.Receptor(ticketDto.Receptor);

                ticket
                    .Feed(3)
                    .Cut();

                var bytes = ticket.Build();
                var preview = ticket.Preview();
                if (printerName.Contains("PDF"))
                {
                    DebugPrint(preview);
                }
                else
                {
                    PrintRaw(printerName, bytes);
                }

                Log.Information("Print success");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Print failed");
                throw;
            }
        }

        public void PrintSampleTicket(string printerName)
        {
            try
            {
                Log.Information("Printing to {Printer}", printerName);

                var config = JsonConvert.DeserializeObject<ConfigDto>(File.ReadAllText(Path.Combine(PrintConstants.ProgramData, "config.json")));
                if (config == null)
                {
                    throw new Exception("No se pudo cargar la configuración de impresión.");
                }

                var ticket = new TicketBuilder()
                .OpenDrawer()
                .Logo()
                .Center(config.Empresa ?? "")
                .Text("RUC: 0999999999")
                .Text("Guayaquil - Ecuador")

                .Separator()
                .HeaderItems()
                .Separator()

                .Item(1, "Producto A", 0, 10)
                .Item(2, "Producto B", 1, 9)
                .Item(1, "Hamburguesa doble con queso extra y papas grandes", 0, 12.50M)
                .Item(5, "Producto B", 1, 9)
                .Item(1, "Hamburguesa doble", 0, 12.50M)
                .Modifier("Queso extra")
                .Modifier("Tocino")
                .Modifier("Salsa BBQ")
                .Modifier("Sin sal", '*')
                .Separator()
                .Total(15)
                .Separator()

                .Qr("https://miempresa.com/factura/123")
                .Barcode("123456789")
                .Feed(3)
                .Cut();

                var bytes = ticket.Build();
                var preview = ticket.Preview();
                if (printerName.Contains("PDF"))
                {
                    DebugPrint(preview);
                }
                else
                {
                    PrintRaw(printerName, bytes);
                }

                Log.Information("Print success");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Print failed");
                throw;
            }

            
        }

        public void DebugPrint(string text)
        {
            PrintDocument pd = new PrintDocument();

            pd.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(text,
                    new Font("Consolas", 10),
                    Brushes.Black,
                    new PointF(10, 10));
            };

            pd.Print();
        }
    }
}
