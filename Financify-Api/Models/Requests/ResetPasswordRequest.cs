using System.ComponentModel.DataAnnotations;

namespace Financify_Api.Models.Requests
{
    public class ResetPasswordRequest
    {
        [Required, EmailAddress, Display(Name = "Registerd email address")]
        public string Email { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string Token { get; set; }
    }
}
