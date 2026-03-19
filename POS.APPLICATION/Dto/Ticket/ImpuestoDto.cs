namespace POS.APPLICATION.Dto.Ticket
{
    public class ImpuestoDto
    {
        public string? Codigo { get; set; }
        public string? CodigoPorcentaje { get; set; }
        public decimal? Tarifa { get; set; }
        public decimal? BaseImponible { get; set; }
        public decimal? Valor { get; set; }
    }
}
