using HRIS.Application.Common.Models;
using HRIS.Infrastructure.Identity;
using HRIS.Net6_CQRSApproach.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRIS.API.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(ApplicationUser user);
        Task<List<Claim>> GetAllValidClaims(ApplicationUser user);

        //Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest);

        DateTime UnixTimeStampToDateTime(long unixTimeStamp);

        string RandomString(int length);
        
    }
}
