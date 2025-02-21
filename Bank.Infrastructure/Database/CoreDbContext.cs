using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Database
{
    public class CoreDbContext : DbContext
    {

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        { }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
