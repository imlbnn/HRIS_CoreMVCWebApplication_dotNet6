using System.ComponentModel.DataAnnotations;

namespace HRISBlazorServerApp.Models
{

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
