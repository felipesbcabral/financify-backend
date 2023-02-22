namespace Financify_Api.Controllers
{
    public class ChargeController
    {
        [Route("api/[controller]")]
        public class ChargeController : ControllerBase
        {
            private readonly IChargeRepository _chargeRepository;

            public ChargeController(IChargeRepository chargeRepository)
            {
                _chargeRepository = chargeRepository;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Charge>>> GetAllCharges()
            {
                IEnumerable<Charge> charges = await _chargeRepository.GetAllCharges();
                return Ok(charges);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Charge>> GetChargeById(int id)
            {
                Charge charge = await _chargeRepository.GetChargeById(id);
                return Ok(charge);
            }

            [HttpPost]
            public async Task<ActionResult<Charge>> CreateNewCharge([FromBody] Charge chargeModel)
            {
                Charge charge = await _chargeRepository.CreateNewCharge(chargeModel);
                return Ok(charge);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<Charge>> UpdateCharge([FromBody] Charge chargeModel, int id)
            {
                chargeModel.Id = id;
                Charge charge = await _chargeRepository.UpdateCharge(chargeModel, id);
                return Ok(charge);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<Charge>> DeleteCharge(int id)
            {
                bool deleted = await _chargeRepository.DeleteCharge(id);
                return Ok(deleted);
            }
        }
    }
}
