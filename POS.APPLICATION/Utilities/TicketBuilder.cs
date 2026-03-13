using System;
using System.Text;

namespace POS.APPLICATION.Utilities
{
    public class TicketBuilder
    {
        private readonly EscPosBuilder _esc = new();
        private readonly StringBuilder _preview = new();
        private const int LineWidth = 42; // ancho típico papel 80mm

        public TicketBuilder()
        {
            _esc.Initialize();
        }

        public TicketBuilder Center(string text)
        {
            _esc.Center().Bold().DoubleSize().Text(text).NormalSize().Bold(false).Left();
            _preview.AppendLine(text.PadLeft((LineWidth + text.Length) / 2));
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
            string line =
                "CANT".PadRight(4) +
                "DESCRIPCION".PadRight(20) +
                "DESC".PadLeft(7) +
                "TOTAL".PadLeft(11);

            _esc.Bold().Text(line).Bold(false);
            _preview.AppendLine(line);

            return this;
        }

        public TicketBuilder Item(int cant, string descripcion, decimal desc, decimal total)
        {
            int descWidth = 20;

            var lines = WrapText(descripcion, descWidth);

            for (int i = 0; i < lines.Count; i++)
            {
                string line;

                if (i == 0)
                {
                    line =
                        cant.ToString().PadRight(4) +
                        lines[i].PadRight(descWidth) +
                        desc.ToString("0.00").PadLeft(7) +
                        total.ToString("0.00").PadLeft(11);
                }
                else
                {
                    line =
                        "".PadRight(4) +
                        lines[i].PadRight(descWidth) +
                        "".PadLeft(7) +
                        "".PadLeft(11);
                }

                _esc.Text(line);
                _preview.AppendLine(line);
            }

            return this;
        }

        public TicketBuilder Modifier(string text, char add = '+')
        {
            int descWidth = 20;

            var lines = WrapText(add + " " + text, descWidth);

            foreach (var l in lines)
            {
                string line =
                    "".PadRight(4) +
                    l.PadRight(descWidth) +
                    "".PadLeft(7) +
                    "".PadLeft(11);

                _esc.Text(line);
                _preview.AppendLine(line);
            }

            return this;
        }

        public TicketBuilder Item(string name, decimal price)
        {
            string priceStr = price.ToString("0.00");

            int spaces = LineWidth - name.Length - priceStr.Length;
            if (spaces < 1) spaces = 1;

            string line = name + new string(' ', spaces) + priceStr;

            _esc.Text(line);
            _preview.AppendLine(line);

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
            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = "";

            foreach (var word in words)
            {
                if ((currentLine + word).Length > maxWidth)
                {
                    lines.Add(currentLine.TrimEnd());
                    currentLine = "";
                }

                currentLine += word + " ";
            }

            if (!string.IsNullOrWhiteSpace(currentLine))
                lines.Add(currentLine.TrimEnd());

            return lines;
        }
    }
}
