using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financify_Api.Controllers
{
    [Route("api/[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceController(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Balance>>> GetAllBalances()
        {
            IEnumerable<Balance> balances = await _balanceRepository.GetAllBalances();
            return Ok(balances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> GetBalanceById(int id)
        {
            Balance balance = await _balanceRepository.GetBalanceById(id);
            return Ok(balance);
        }

        [HttpPost]
        public async Task<ActionResult<Balance>> CreateNewBalance([FromBody] Balance balanceModel)
        {
            Balance balance = await _balanceRepository.CreateNewBalance(balanceModel);
            return Ok(balance);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Balance>> UpdateBalance([FromBody] Balance balanceModel, int id)
        //{
        //    balanceModel.Id = id;
        //    Balance balance = await _balanceRepository.UpdateBalance(balanceModel, id);
        //    return Ok(balance);
        //}
    }
}
