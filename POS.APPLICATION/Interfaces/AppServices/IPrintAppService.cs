using POS.APPLICATION.Dto.Ticket;

namespace POS.APPLICATION.Interfaces.AppServices
{
    public interface IPrintAppService
    {
        IEnumerable<string> GetPrinters();
        void Print(string printerName, string text);
        void PrintRaw(string printerName, byte[] bytes);
        void PrintTicket(string printerName, List<string> lines);
        void PrintTicket(string printerName, TicketDto ticketDto);
        void PrintSampleTicket(string printerName);
        void DebugPrint(string text);
    }
}
