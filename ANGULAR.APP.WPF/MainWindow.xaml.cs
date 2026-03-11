using ANGULAR.APP.WPF.Dto;
using Microsoft.Web.WebView2.Core;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Text.Json;
using System.Windows;
using System.Management;

namespace ANGULAR.APP.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
#if DEBUG
            webView.Source = new Uri("http://localhost:4200");
#else

            string folder = $"{AppDomain.CurrentDomain.BaseDirectory}/browser";

            webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "app",
                folder,
                Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow
            );

            webView.Source = new Uri("https://app/index.html");

#endif
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }


        private async void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var data = JsonSerializer.Deserialize<Message>(e.WebMessageAsJson);

            if (data.action == "getPorts")
            {
                List<string?> puertos = new List<string?>();

                puertos.AddRange(SerialPort.GetPortNames().ToList());
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'USB%'");

                foreach (ManagementObject device in searcher.Get())
                {
                    puertos.Add(device["DeviceID"]?.ToString());
                }

                var respuesta = JsonSerializer.Serialize(new
                {
                    action = "portsResult",
                    requestId = data.requestId,
                    data = puertos
                });

                webView.CoreWebView2.PostWebMessageAsJson(respuesta);
            }

            if (data.action == "printTicket")
            {
                Imprimir(data.data);
                var respuesta = JsonSerializer.Serialize(new
                {
                    action = "printResult",
                    requestId = data.requestId,
                    success = true
                });

                webView.CoreWebView2.PostWebMessageAsJson(respuesta);
            }
        }

        void Imprimir(string ticket)
        {
            PrintDocument pd = new PrintDocument();

            pd.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(
                    ticket,
                    new Font("Consolas", 10),
                    Brushes.Black,
                    new RectangleF(0, 0, 300, 1000)
                );
            };

            pd.Print();
        }
    }
}