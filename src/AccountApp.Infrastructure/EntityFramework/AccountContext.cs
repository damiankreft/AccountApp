using AccountApp.Core.Domain;
using AccountApp.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.Infrastructure.EntityFramework
{
    public class AccountContext : DbContext
    {
        private readonly SqlSettings _settings;
        public DbSet<Account> Accounts { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options, SqlSettings settings) : base(options)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_settings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_settings.DbName);
                return;
            }

            optionsBuilder.UseSqlServer(_settings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accountBuilder = modelBuilder.Entity<Account>();
            accountBuilder.HasKey(x => x.Id);
        }
    }
}