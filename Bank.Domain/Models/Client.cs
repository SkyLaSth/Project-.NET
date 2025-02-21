using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Base;

namespace Bank.Domain.Models
{
    public class Client : BaseEntity
    {

        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required string Salt {  get; set; }
    }
}
