using System.Collections.Generic;

namespace HRIS.Net6_CQRSApproach.Model
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
    }
}