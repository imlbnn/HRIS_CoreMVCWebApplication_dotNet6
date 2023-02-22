using System.ComponentModel.DataAnnotations;

namespace HRIS.Application.Common.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}