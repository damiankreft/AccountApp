using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository, ISqlRepository
    {
        private readonly AccountContext _context;
        
        public AccountRepository(AccountContext context)
        {
            _context = context;

        }

        /// <summary>
        /// 'This Little Maneuver's Gonna Cost Us 51 Years' - USE WISELY!
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetAllAsync()
        => await _context.Accounts.ToListAsync();

        public async Task<Account> GetAsync(Guid id)
        => await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Account> GetAsync(string email)
        => await _context.Accounts.SingleOrDefaultAsync(x => x.Email == email);

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Account account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}