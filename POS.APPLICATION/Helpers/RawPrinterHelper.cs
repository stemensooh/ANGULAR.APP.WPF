using System.Runtime.InteropServices;

namespace POS.APPLICATION.Helpers
{
    public static class RawPrinterHelper
    {
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA")]
        public static extern bool OpenPrinter(string printerName, out IntPtr printer, IntPtr defaults);

        [DllImport("winspool.Drv")]
        public static extern bool ClosePrinter(IntPtr printer);

        [DllImport("winspool.Drv")]
        public static extern bool StartDocPrinter(IntPtr printer, int level, ref DOCINFOA docInfo);

        [DllImport("winspool.Drv")]
        public static extern bool EndDocPrinter(IntPtr printer);

        [DllImport("winspool.Drv")]
        public static extern bool StartPagePrinter(IntPtr printer);

        [DllImport("winspool.Drv")]
        public static extern bool EndPagePrinter(IntPtr printer);

        [DllImport("winspool.Drv")]
        public static extern bool WritePrinter(IntPtr printer, byte[] bytes, int count, out int written);

        [StructLayout(LayoutKind.Sequential)]
        public struct DOCINFOA
        {
            public string pDocName;
            public string pOutputFile;
            public string pDataType;
        }

        public static bool SendBytesToPrinter(string printerName, byte[] bytes)
        {
            OpenPrinter(printerName, out IntPtr printer, IntPtr.Zero);

            DOCINFOA di = new DOCINFOA
            {
                pDocName = "Ticket",
                pDataType = "RAW"
            };

            StartDocPrinter(printer, 1, ref di);
            StartPagePrinter(printer);

            WritePrinter(printer, bytes, bytes.Length, out _);

            EndPagePrinter(printer);
            EndDocPrinter(printer);
            ClosePrinter(printer);

            return true;
        }
    }
}
