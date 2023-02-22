using Financify_Api.Data;
using Financify_Api.Models;

namespace Financify_Api.Repositories
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly FinancifyContext _dbContext;

        public ChargeRepository(FinancifyContext financifyContext)
        {
            _dbContext = financifyContext;
        }

        public async Task<IEnumerable<Charge>> GetAllCharges()
        {
            return await _dbContext.Charges.ToListAsync();
        }


        public async Task<Charge> GetChargeById(int id)
        {
            var charge = await _dbContext.Charges.FirstOrDefaultAsync(x => x.Id == id);

            if (charge == null)
            {
                throw new Exception($"Charge for the specified {id} was not found");
            }

            return charge;
        }


        public async Task<Charge> CreateNewCharge(Charge charge)
        {
            await _dbContext.Charges.AddAsync(charge);
            await _dbContext.SaveChangesAsync();

            return charge;
        }

        public async Task<Charge> UpdateCharge(Charge charge, int id)
        {
            Charge chargeSaved = await GetChargeById(id);

            if (chargeSaved == null)
            {
                throw new Exception($"Charge for the especified {id} was not found");
            }

            chargeSaved.Name = charge.Name;
            chargeSaved.DueDate = charge.DueDate;
            chargeSaved.Status = charge.Status;
            chargeSaved.Value = charge.Value;


            _dbContext.Charges.Update(chargeSaved);
            await _dbContext.SaveChangesAsync();

            return chargeSaved;
        }

        public async Task<bool> DeleteCharge(int id)
        {
            Charge chargeSaved = await GetChargeById(id);

            _dbContext.Charges.Remove(chargeSaved);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
