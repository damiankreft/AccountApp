using System.Collections.Generic;
using AccountApp.Core.Domain;

namespace AccountApp.Core.Repositories
{
    public interface IAccountRepository
    {
         Account Get(int id);
         Account Get(string email);
         IEnumerable<Account> GetAll();
         void Add(Account account);
         void Update(Account account);
         void Remove(Account account);
    }
}