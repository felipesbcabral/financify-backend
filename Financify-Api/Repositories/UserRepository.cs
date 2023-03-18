using Financify_Api.Data;
using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Financify_Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FinancifyContext _dbContext;

    public UserRepository(FinancifyContext financifyContext)
    {
        _dbContext = financifyContext;
    }


    public User Get(string username, string password)
    {
        var user = _dbContext.Set<User>()
            .AsEnumerable()
            .FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase)
                             && x.Password == password);

        return user ?? throw new InvalidOperationException("User not found");
    }
}
