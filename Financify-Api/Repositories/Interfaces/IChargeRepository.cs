using Financify_Api.Models;

namespace Financify_Api.Repositories.Interfaces
{
    public interface IChargeRepository
    {
        Task<IEnumerable<Charge>> GetAllCharges();

        Task<Charge> GetChargeById(int id);

        Task<Charge> CreateNewCharge(Charge charge);

        Task<Charge> UpdateCharge(Charge charge, int id);

        Task<bool> DeleteCharge(int id);
    }
}
