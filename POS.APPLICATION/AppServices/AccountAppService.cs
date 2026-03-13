using POS.APPLICATION.Constanst;
using POS.APPLICATION.Dto;
using POS.APPLICATION.InfraServices.Interfaces;
using POS.APPLICATION.Interfaces.AppServices;

namespace POS.APPLICATION.AppServices
{
    public class AccountAppService : IAccountAppService
    {
        public AccountAppService(IHttpRequestService httpRequestService)
        {

        }

        public Task<UserDto> Login(string username, string password, string nit)
        {
            UserDto userDto = new UserDto()
            {
                Name = username,
                LastName = "--",
                Email = username + "@gmail.com"
            };

            return Task.FromResult(userDto);
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<ConfigDto> Config(UserDto userDto)
        {
            ConfigDto configDto = new ConfigDto()
            {
                DeviceId = Guid.NewGuid().ToString(),
                Empresa = "Mi Empresa",
                Impresora = PrintConstants.DefaultPrinter,
                Token = ""
            };

            return Task.FromResult(configDto);
        }
    }
}
