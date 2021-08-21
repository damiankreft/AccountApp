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
            new Account() 
            {
                Email = "test1@example.com", 
                Username = "Fureya", 
                Password = "nF+P29uNPG9J1ZFczoc4LuP1EDawerwWegEYp63BtSDenarJwyCCIA==",
                Salt =  "someSalt",
                Role = "user"
            },
            new Account()
            {
                Email = "example@test.com",
                Username =  "Soja",
                Password =  "passwordSecret", 
                Salt = "someSalt",
                Role = "user"
            },
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