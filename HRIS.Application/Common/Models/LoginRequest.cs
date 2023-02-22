using System.ComponentModel.DataAnnotations;

namespace HRIS.Application.Common.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}