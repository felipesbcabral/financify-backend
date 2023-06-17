using Financify_Api.Models;
using Financify_Api.Models.Requests;

namespace Financify_Api.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid id);

        Task<IEnumerable<Account>> GetAllAsync();

        Task AddAsync(Account account);

        Task UpdateAsync(Account account);

        Task DeleteAsync(Account account);

        Task<Account> GetByEmailAsync(string email);

        Task<Account> GetByResetTokenAsync(string resetToken);
    }
}
