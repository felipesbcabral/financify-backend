﻿using Financify_Api.Data;
using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financify_Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinancifyContext _dbContext;

        public AccountRepository(FinancifyContext financifyContext)
        {
            _dbContext = financifyContext;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Account>()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _dbContext.Set<Account>().ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            await _dbContext.Set<Account>().AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _dbContext.Entry(account).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account account)
        {
            _dbContext.Set<Account>().Remove(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
