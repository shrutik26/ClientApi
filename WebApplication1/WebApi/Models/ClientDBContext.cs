using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClientWork.Models
{
    public class ClientDBContext : DbContext
    {
        public ClientDBContext() : base("cli") { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }
    }
}