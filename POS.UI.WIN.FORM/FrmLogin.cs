using Newtonsoft.Json;
using POS.APPLICATION.AppServices;
using POS.APPLICATION.Constanst;
using POS.APPLICATION.InfraServices.Interfaces;
using POS.APPLICATION.Interfaces.AppServices;
using POS.INFRA.HTTP.InfraServices;

namespace POS.UI.WIN.FORM
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            IHttpRequestService http = new HttpRequestService();
            IAccountAppService accountAppService = new AccountAppService(http);
            var user = await accountAppService.Login(txtUsuario.Text, txtClave.Text, txtNit.Text);
            var config = await accountAppService.Config(user);

            string ruta = PrintConstants.ProgramData;

            Directory.CreateDirectory(ruta);
            File.WriteAllText(Path.Combine(ruta, "config.json"), JsonConvert.SerializeObject(config, Formatting.Indented));

            MessageBox.Show($"Usuario autorizado: \n{JsonConvert.SerializeObject(user, Formatting.Indented)}");

        }
    }
}
