﻿using Financify_Api.Models;
using Financify_Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financify_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
        [Authorize]
        public async Task<ActionResult<Account>> Create([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while deleting the account.");
            }
        }


        //[HttpGet]
        //[Route("user")]
        //[Authorize(Roles = "user, admin")] => User aqui pra autorizar o user eu preciso passar na geração do token username: client, password: client 
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //}

        //[HttpGet]
        //[Route("admin")]
        //[Authorize(Roles = "admin")] => Admin para entrar no admin e gerar um token admin eu preciso passar username: felp password: felp ai ele vai pra admin
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //}
    }
}
