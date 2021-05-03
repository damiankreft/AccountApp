using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Core.Domain;

namespace AccountApp.Core.Repositories
{
    public interface IAccountRepository
    {
         Task<Account> GetAsync(int id);
         Task<Account> GetAsync(string email);
         Task<IEnumerable<Account>> GetAllAsync();
         Task AddAsync(Account account);
         Task UpdateAsync(Account account);
         Task RemoveAsync(Account account);
    }
}