namespace POS.APPLICATION.Models
{
    public class TicketRequest
    {
        public string? PrinterName { get; set; }
        public List<string>? Lines { get; set; }
    }
}
