
using Bank.Domain.Models;
using Bank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly CoreDbContext _context;

        public ClientRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetById(int id) => await _context.Clients.FindAsync(id);

        public async Task<IEnumerable<Client>> GetAll() => await _context.Clients.ToListAsync();
        public async Task Add(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
