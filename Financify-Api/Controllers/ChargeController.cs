using AutoMapper;
using Financify_Api.Models;
using Financify_Api.Models.Responses;
using Financify_Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financify_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChargeController : ControllerBase
    {
        private readonly IChargeRepository _chargeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ChargeController(IChargeRepository chargeRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _chargeRepository = chargeRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChargeResponse>>> GetAll()
        {
            try
            {
                var charges = await _chargeRepository.GetAllAsync();

                if (charges == null)
                {
                    return NotFound();
                }

                var chargeResponses = _mapper.Map<List<ChargeResponse>>(charges);

                return Ok(chargeResponses);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao obter as cobranças.");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Charge>> GetById(Guid id)
        {
            var charge = await _chargeRepository.GetByIdAsync(id);
            if (charge == null)
            {
                return NotFound();
            }

            var chargeResponse = _mapper.Map<ChargeResponse>(charge);

            return Ok(chargeResponse);
        }

        [HttpPost("accounts/{accountId}")]
        [Authorize]
        public async Task<ActionResult<Charge>> Create(Guid accountId, [FromBody] Charge charge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtém a Account vinculada à Charge a partir do Id fornecido na rota da requisição
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
            {
                return NotFound("A Account não foi encontrada.");
            }

            // Vincula a Charge à Account
            charge.AccountId = accountId;
            charge.CreatedAt = DateTime.UtcNow;
            await _chargeRepository.AddAsync(charge);

            // Recupera a Charge recém-criada do banco de dados com todas as propriedades preenchidas
            var createdCharge = await _chargeRepository.GetByIdAsync(charge.Id);

            var chargeResponse = _mapper.Map<ChargeResponse>(createdCharge);

            return Ok(chargeResponse);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Charge>> Update(Guid id, [FromBody] Charge charge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingCharge = await _chargeRepository.GetByIdAsync(id);

            if (existingCharge == null)
            {
                return NotFound();
            }

            existingCharge.Name = charge.Name;
            existingCharge.Description = charge.Description;
            existingCharge.DueDate = charge.DueDate;
            existingCharge.Value = charge.Value;
            existingCharge.Status = charge.Status;

            await _chargeRepository.UpdateAsync(existingCharge);

            var chargeResponse = _mapper.Map<ChargeResponse>(existingCharge);

            return Ok(chargeResponse);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            var charge = await _chargeRepository.GetByIdAsync(id);
            if (charge == null)
            {
                return NotFound();
            }

            await _chargeRepository.DeleteAsync(charge);

            return NoContent();
        }

        [HttpGet("accounts/{accountId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChargeResponse>>> GetAllByAccountId(Guid accountId)
        {
            try
            {
                var charges = await _chargeRepository.GetByAccountIdAsync(accountId);

                if (charges == null)
                {
                    return NotFound();
                }

                var chargeResponses = _mapper.Map<List<ChargeResponse>>(charges);

                return Ok(chargeResponses);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao obter as cobranças.");
            }
        }
    }
}
