using POS.APPLICATION.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.APPLICATION.Interfaces.AppServices
{
    public interface IAccountAppService
    {
        Task<UserDto> Login(string username, string password, string nit);
        Task<ConfigDto> Config(UserDto userDto);
    }
}
