namespace POS.APPLICATION.Dto.Ticket
{
    public class ItemDto
    {
        public string? Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public List<ImpuestoDto>? Impuestos { get; set; }
        public decimal? TotalImpuestos
        {
            get
            {
                if (Impuestos == null) return null;
                decimal sum = 0;
                foreach (var imp in Impuestos)
                {
                    if (imp.Valor != null) sum += imp.Valor.Value;
                }
                return sum;
            }
        }

        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public List<AdicionalDto>? Adicional { get; set; }
    }



}
