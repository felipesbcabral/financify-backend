using Financify_Api.Data;
using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financify_Api.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly FinancifyContext _dbContext;

        public BalanceRepository(FinancifyContext financifyContext)
        {
            _dbContext = financifyContext;
        }

        public async Task<IEnumerable<Balance>> GetAllBalances()
        {
            return await _dbContext.Balances.ToListAsync();
        }

        public async Task<Balance> GetBalanceById(int id)
        {
            var balance = await _dbContext.Balances.FirstOrDefaultAsync(x => x.Id == id);

            if (balance == null)
            {
                throw new Exception($"Balance for the specified {id} was not found");
            }

            return balance;
        }

        public async Task<Balance> CreateNewBalance(Balance balance)
        {
            await _dbContext.Balances.AddAsync(balance);
            await _dbContext.SaveChangesAsync();

            return balance;
        }

        //public async Task<Balance> UpdateBalance(Balance account, int id)
        //{
        //    Balance balanceSaved = await GetBalanceById(id);

        //    if (balanceSaved == null)
        //    {
        //        throw new Exception($"Balance for the especified {id} was not found");
        //    }

        //    balanceSaved.Name = account.Name;
        //    balanceSaved.Balance = account.Balance;

        //    _dbContext.Balances.Update(balanceSaved);
        //    await _dbContext.SaveChangesAsync();

        //    return balanceSaved;
        //}
    }
}
