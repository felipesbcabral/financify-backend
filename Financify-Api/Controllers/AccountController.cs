using Financify_Api.Models;
using Financify_Api.Models.Requests;
using Financify_Api.Models.Responses;
using Financify_Api.Repositories.Interfaces;
using Financify_Api.Services.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financify_Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountRepository accountRepository, IEmailService emailService, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            var accounts = await _accountRepository.GetAllAsync();

            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Account>> GetById(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> Create([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            await _accountRepository.AddAsync(account);
            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Account>> Update([FromRoute] Guid id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingAccount = await _accountRepository.GetByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.FirstName = account.FirstName;
            existingAccount.LastName = account.LastName;
            existingAccount.Email = account.Email;
            existingAccount.Password = account.Password;
            existingAccount.Balance = account.Balance;

            await _accountRepository.UpdateAsync(existingAccount);

            return Ok(existingAccount);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                await _accountRepository.DeleteAsync(account);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the account.");
            }
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] ResetPasswordRequest request)
        {
            var user = await _accountRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest(new ApiResponse { Status = "Error", Message = "User not found" });
            }

            var token = _tokenService.GenerateRandomToken();
            user.ResetToken = token;
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _accountRepository.UpdateAsync(user);

            string emailContent = $"Olá! Você solicitou a recuperação de senha. Clique no link abaixo para redefinir sua senha:\n\n" +
                                  $"Redefinir Senha: https://financify-frontend.vercel.app/reset?token={token}\n\n";

            var emailMessage = new EmailMessage(new string[] { request.Email }, "Financify recuperação de Senha", emailContent);
            _emailService.SendEmail(emailMessage);

            return Ok(new ApiResponse { Status = "Success", Message = "Email sent successfully" });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _accountRepository.GetByResetTokenAsync(request.Token);

            if (user == null)
            {
                return BadRequest(new ApiResponse { Status = "Error", Message = "User not found" });
            }

            if (user.ResetToken == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            _tokenService.CreatePasswordHash(request.NewPassword, out string passwordHash);

            user.Password = passwordHash;
            user.ResetToken = null;
            user.ResetTokenExpires = null;

            await _accountRepository.UpdateAsync(user);

            return Ok(new ApiResponse { Status = "Success", Message = "Password successfully reset" });
        }

        [HttpGet("balance/{accountId}")]
        [Authorize]
        public async Task<ActionResult<Account>> GetAccountBalance(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPut("deposit/{id}")]
        [Authorize]
        public async Task<ActionResult<Account>> Deposit(Guid id, [FromBody] DepositRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            if (request.Valor <= 0)
            {
                return BadRequest("O valor do depósito deve ser maior que zero.");
            }

            // Realize as validações do método de pagamento, número do cartão, data de validade, código de segurança e senha aqui

            account.Balance += request.Valor;
            await _accountRepository.UpdateAsync(account);

            return Ok(account);
        }

    }
}
