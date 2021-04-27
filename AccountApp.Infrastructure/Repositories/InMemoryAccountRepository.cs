using System.Collections.Generic;
using System.Linq;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;

namespace AccountApp.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        public static ISet<Account> _accounts = new HashSet<Account>
        {
            new Account("test1@example.com", "secretPassword"),
            new Account("example@test.com", "passwordSecret"),
        };

        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public Account Get(int id)
            => _accounts.Single(x => x.Id == id);

        public Account Get(string email)
            => _accounts.Single(x => x.Email == email.ToLowerInvariant());

        public IEnumerable<Account> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Account account)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}