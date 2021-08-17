using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;

namespace AccountApp.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        public static ISet<Account> _accounts = new HashSet<Account>
        {
            new Account("test1@example.com", "Fureya", "nF+P29uNPG9J1ZFczoc4LuP1EDawerwWegEYp63BtSDenarJwyCCIA==", "someSalt"),
            new Account("example@test.com", "Soja", "passwordSecret", "someSalt"),
        };

        public async Task AddAsync(Account account)
        {
            await Task.FromResult(_accounts.Add(account));
        }

        public async Task<Account> GetAsync(int id)
            => await Task.FromResult(_accounts.Single(x => x.Id == id));

        public async Task<Account> GetAsync(string email)
            => await Task.FromResult(_accounts.FirstOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await Task.FromResult(_accounts);
        }

        public async Task RemoveAsync(Account account)
        {
            _accounts.Remove(account);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Account account)
        {
            await Task.CompletedTask;
        }
    }
}