using Financify_Api.Models;

namespace Financify_Api.Repositories.Interfaces
{
    public interface IChargeRepository
    {
        Task<Charge> GetByIdAsync(Guid id);
        Task<IEnumerable<Charge>> GetAllAsync();
        Task AddAsync(Charge charge);
        Task UpdateAsync(Charge charge);
        Task DeleteAsync(Charge charge);
    }
}
