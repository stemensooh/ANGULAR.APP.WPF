using System;
using System.Text;

namespace POS.APPLICATION.Utilities
{
    public class TicketBuilder
    {
        private readonly EscPosBuilder _esc = new();
        private readonly StringBuilder _preview = new();
        private readonly int LineWidth; // ancho típico papel 80mm

        private const int cantW = 7;
        private const int descW = 21;
        private const int precUniValW = 7;
        private const int totalW = 7;

        public TicketBuilder(int lineWidth = 42)
        {
            LineWidth = lineWidth;
            _esc.Initialize();
        }

        public TicketBuilder Center(string text)
        {
            _esc.Center().Bold().DoubleSize().Text(text).NormalSize().Bold(false).Left();
            int padding = (LineWidth - text.Length) / 2;
            if (padding < 0) padding = 0;

            _preview.AppendLine(new string(' ', padding) + text);
            return this;
        }

        public TicketBuilder Text(string text)
        {
            _esc.Text(text);
            _preview.AppendLine(text);
            return this;
        }

        public TicketBuilder Separator()
        {
            string sep = new string('-', LineWidth);
            _esc.Text(sep);
            _preview.AppendLine(sep);
            return this;
        }

        public TicketBuilder HeaderItems()
        {
            if (cantW + descW + precUniValW + totalW != LineWidth)
                throw new Exception("Los anchos no cuadran");

            string line =
                "CANT.".PadRight(cantW) + 
                "DESCRIPCION".PadRight(descW) +
                "P.UNIT.".PadLeft(precUniValW) +
                "VALOR".PadLeft(totalW);

            _esc.Bold().Text(line).Bold(false);
            _preview.AppendLine(line);

            return this;
        }

        public TicketBuilder Item(int cant, string descripcion, decimal precio, decimal total)
        {
            var lines = WrapText(descripcion, descW);

            for (int i = 0; i < lines.Count; i++)
            {
                string line;

                if (i == 0)
                {
                    line =
                        cant.ToString().PadRight(cantW) + 
                        lines[i].PadRight(descW) +
                        precio.ToString("0.00").PadLeft(precUniValW) +
                        total.ToString("0.00").PadLeft(totalW);
                }
                else
                {
                    line =
                        "".PadRight(cantW) + 
                        lines[i].PadRight(descW) +
                        "".PadLeft(precUniValW) +
                        "".PadLeft(totalW);
                }

                _esc.Text(line);
                _preview.AppendLine(line);
            }

            return this;
        }

        public TicketBuilder Modifier(string text, char add = '+')
        {
            var lines = WrapText("  " + add + " " + text, descW);

            foreach (var l in lines)
            {
                string line =
                    "".PadRight(cantW) +
                    l.PadRight(descW) +
                    "".PadLeft(precUniValW) +
                    "".PadLeft(totalW);

                _esc.Text(line);
                _preview.AppendLine(line);
            }

            return this;
        }

        public TicketBuilder Total(decimal total)
        {
            string label = "TOTAL";
            string priceStr = total.ToString("0.00");

            int spaces = LineWidth - label.Length - priceStr.Length;

            string line = label + new string(' ', spaces) + priceStr;

            _esc.Bold().Text(line).Bold(false);
            _preview.AppendLine(line);

            return this;
        }

        public TicketBuilder OpenDrawer()
        {
            _esc.OpenDrawer();
            return this;
        }

        public TicketBuilder Qr(string data)
        {
            _esc.Qr(data);
            _preview.AppendLine("[QR] " + data);
            return this;
        }

        public TicketBuilder Barcode(string data)
        {
            _esc.Barcode(data);
            _preview.AppendLine("[BARCODE] " + data);
            return this;
        }

        public TicketBuilder Logo()
        {
            _preview.AppendLine("[LOGO]");
            return this;
        }

        public TicketBuilder Feed(int lines = 3)
        {
            _esc.Feed(lines);
            for (int i = 0; i < lines; i++)
                _preview.AppendLine();
            return this;
        }

        public TicketBuilder Cut()
        {
            _esc.Cut();
            _preview.AppendLine("[CUT]");
            return this;
        }

        public byte[] Build() => _esc.Build();

        public string Preview() => _preview.ToString();

        private List<string> WrapText(string text, int maxWidth)
        {
            var lines = new List<string>();

            while (text.Length > maxWidth)
            {
                int wrapAt = text.LastIndexOf(' ', maxWidth);

                if (wrapAt <= 0)
                    wrapAt = maxWidth;

                lines.Add(text.Substring(0, wrapAt).Trim());
                text = text.Substring(wrapAt).Trim();
            }

            if (text.Length > 0)
                lines.Add(text);

            return lines;
        }
    }
}
