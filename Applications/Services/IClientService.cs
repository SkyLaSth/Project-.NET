using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Models;

namespace Bank.Applications.Services
{
    public interface IClientService
    {
        Task<Client?> GetClientById(int id);
        Task<IEnumerable<Client>> GetAllClients();
        Task AddClient(Client client);
        Task UpdateClient(Client client);
        Task DeleteClient(int id);
    }
}
