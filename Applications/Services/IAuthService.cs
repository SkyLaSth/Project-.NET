using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Applications.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterClient(string name, string email, string password);
        Task<string?> AuthenticateClient(string email, string password);
    }
}
