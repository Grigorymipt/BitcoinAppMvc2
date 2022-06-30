using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BitcoinAppMvc.Models;
using System.Runtime.CompilerServices;
using DreamTeam.Bitcoin.Data.Repository;

namespace BitcoinAppMvc.Data
{
    public class BitcoinAppMvcContext : DbContext
    {
        public BitcoinAppMvcContext()
        {
            Database.EnsureCreated();
        }

        public BitcoinAppMvcContext(DbContextOptions<BitcoinAppMvcContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            this.Database.Migrate();
            this.TransactionDataRepository = new TransactionDataRepository(this);
            
        }
        public DbSet<TransactionData> TransactionData { get; set; }

        public TransactionDataRepository TransactionDataRepository { get; set; }
        
    }
}

