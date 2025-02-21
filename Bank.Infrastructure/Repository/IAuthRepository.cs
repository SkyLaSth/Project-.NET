using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Models;

namespace Bank.Infrastructure.Repository
{
    public interface IAuthRepository
    {
        Task<Client?> GetClientByEmail(string email);
        Task AddClient(Client client);
    }
}
