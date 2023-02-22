using Financify_Api.Models;

namespace Financify_Api.Repositories.Interfaces
{
    public interface IBalanceRepository
    {
        Task<IEnumerable<Balance>> GetAllBalances();

        Task<Balance> GetBalanceById(int id);

        Task<Balance> CreateNewBalance(Balance charge);

        //Task<Balance> UpdateBalance(Balance charge, int id);
    }
}
