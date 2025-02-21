using Bank.Domain.Models;
using Bank.Infrastructure.Repository;

namespace Bank.Applications.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client?> GetClientById(int id)
        {
            return await _clientRepository.GetById(id);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _clientRepository.GetAll();
        }

        public async Task AddClient(Client client)
        {
            await _clientRepository.Add(client);
        }

        public async Task UpdateClient(Client client)
        {
            await _clientRepository.Update(client);
        }

        public async Task DeleteClient(int id)
        {
            await _clientRepository.Delete(id);
        }
    }
}
