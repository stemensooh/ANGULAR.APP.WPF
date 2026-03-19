namespace POS.APPLICATION.Dto.Ticket
{
    public class TicketDto
    {
        public string? ClaveAcceso { get; set; }
        public string? TipoComprobante { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Sucursal { get; set; }
        public string? PuntoVenta { get; set; }
        public DateTime? FechaEmision { get; set; }
        public EmisorDto? Emisor { get; set; }
        public ReceptorDto? Receptor { get; set; }
        public List<ItemDto>? Items { get; set; }
        public List<ImpuestoDto>? Impuestos { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
    }

    

   
}
