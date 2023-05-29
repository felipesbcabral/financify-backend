using Financify_Api.Models;

namespace Financify_Api.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Account GetByEmail(string email);

        Task<Account> GetByIdAsync(Guid id);

        Task<IEnumerable<Account>> GetAllAsync();

        Task AddAsync(Account account);

        Task UpdateAsync(Account account);

        Task DeleteAsync(Account account);
    }
}
