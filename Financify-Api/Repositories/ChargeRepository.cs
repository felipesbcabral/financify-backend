using Financify_Api.Data;
using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financify_Api.Repositories
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly FinancifyContext _dbContext;

        public ChargeRepository(FinancifyContext financifyContext)
        {
            _dbContext = financifyContext;
        }

        public async Task<Charge> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Charge>()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Charge>> GetAllAsync()
        {
            return await _dbContext.Set<Charge>()
                .ToListAsync();
        }

        public async Task AddAsync(Charge charge)
        {
            charge.CreatedAt = charge.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Set<Charge>().AddAsync(charge);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Charge charge)
        {
            charge.UpdatedAt = DateTime.UtcNow;
            _dbContext.Entry(charge).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Charge charge)
        {
            _dbContext.Set<Charge>().Remove(charge);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Charge>> GetByAccountIdAsync(Guid accountId)
        {
            return await _dbContext.Set<Charge>()
                .Where(c => c.AccountId == accountId)
                .ToListAsync();
        }
    }
}
