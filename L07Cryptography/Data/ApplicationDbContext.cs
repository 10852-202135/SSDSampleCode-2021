using L07Cryptography.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace L07Cryptography.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Password> Passwords { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<L07Cryptography.Models.BankAccountDp> BankAccountDp { get; set; }

    }
}
