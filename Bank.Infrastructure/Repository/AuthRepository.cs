using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Models;
using Bank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CoreDbContext _context;

        public AuthRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetClientByEmail(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
    }
}
