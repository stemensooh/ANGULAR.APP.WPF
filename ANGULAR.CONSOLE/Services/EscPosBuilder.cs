using System.Text;

namespace ANGULAR.CONSOLE.Services
{
    public class EscPosBuilder
    {
        private List<byte> _buffer = new();

        public EscPosBuilder Initialize()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x40 });
            return this;
        }

        public EscPosBuilder Center()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x61, 0x01 });
            return this;
        }

        public EscPosBuilder Left()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x61, 0x00 });
            return this;
        }

        public EscPosBuilder Bold(bool enable = true)
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x45, (byte)(enable ? 1 : 0) });
            return this;
        }

        public EscPosBuilder DoubleSize()
        {
            _buffer.AddRange(new byte[] { 0x1D, 0x21, 0x11 });
            return this;
        }

        public EscPosBuilder NormalSize()
        {
            _buffer.AddRange(new byte[] { 0x1D, 0x21, 0x00 });
            return this;
        }

        public EscPosBuilder Text(string text)
        {
            _buffer.AddRange(Encoding.ASCII.GetBytes(text + "\n"));
            return this;
        }

        public EscPosBuilder Feed(int lines = 3)
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x64, (byte)lines });
            return this;
        }

        public EscPosBuilder Cut()
        {
            _buffer.AddRange(new byte[] { 0x1D, 0x56, 0x00 });
            return this;
        }

        public byte[] Build()
        {
            return _buffer.ToArray();
        }

        public EscPosBuilder Raw(byte[] bytes)
        {
            _buffer.AddRange(bytes);
            return this;
        }


        public EscPosBuilder OpenDrawer()
        {
            _buffer.AddRange(new byte[] { 0x1B, 0x70, 0x00, 0x19, 0xFA });
            return this;
        }

        public EscPosBuilder Barcode(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(data);

            _buffer.AddRange(new byte[] { 0x1D, 0x6B, 0x49, (byte)bytes.Length });
            _buffer.AddRange(bytes);

            return this;
        }

        public EscPosBuilder Qr(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            int len = bytes.Length + 3;

            _buffer.AddRange(new byte[] { 0x1D, 0x28, 0x6B, (byte)len, 0x00, 0x31, 0x50, 0x30 });
            _buffer.AddRange(bytes);

            _buffer.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x51, 0x30 });

            return this;
        }

    }
}
