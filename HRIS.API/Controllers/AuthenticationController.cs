using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using HRIS.Net6_CQRSApproach.Model;
using HRIS.Application.Common.Models;
using HRIS.Infrastructure;
using HRIS.Infrastructure.Identity;
using HRIS.API.Interfaces;

namespace HRIS.API.Controllers
{
    [ApiExplorerSettings(GroupName = "AUTH")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly TokenValidationParameters _tokenValidationParams;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ApplicationDbContext _apiDbContext;
        private readonly IJwtTokenGenerator _jWTConfiguration;

        public AuthenticationController(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                //IOptionsMonitor<JwtConfig> optionsMonitor,
                //TokenValidationParameters tokenValidationParams,
                ILogger<AuthenticationController> logger,
                ApplicationDbContext apiDbContext
                , IJwtTokenGenerator jWTConfiguration
                )
        {

            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            //_tokenValidationParams = tokenValidationParams;
            _apiDbContext = apiDbContext;
            _jWTConfiguration = jWTConfiguration;
        }


        [HttpPost]
        [Route("RegisterUser")]
        //Run and use Postman to call this request
        public async Task<IActionResult> Register([FromBody] RegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByNameAsync(user.Username);

                if (existingUser != null)
                {
                    return BadRequest(new TransactionResponse()
                    {
                        Errors = new List<string>() {
                                "Username already in use"
                            },
                        Success = false
                    });
                }

                var newUser = new ApplicationUser() { Email = user.Email, UserName = user.Username };

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                if (isCreated.Succeeded)
                {
                    var jwtToken = _jWTConfiguration.GenerateJwtToken(newUser);

                    return Ok("User has been registered");
                }
                else
                {
                    return BadRequest(new TransactionResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new TransactionResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        [HttpDelete]
        [Route("DeleteUser")]
        //Run and use Postman to call this request
        public async Task<IActionResult> Delete(string username)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return Ok("User has been deleted");
                else
                    return BadRequest(new TransactionResponse()
                    {
                        Errors = new List<string>() {
                                "Failed: User deletion"
                            },
                        Success = false
                    });
            }
            else
                return BadRequest(new TransactionResponse()
                {
                    Errors = new List<string>() {
                                "User Not Found"
                            },
                    Success = false
                });
        }

        [HttpPost]
        [Route("Login")]
        //Run and use Postman to call this request
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(loginRequest.Username);

                if (existingUser == null)
                {
                    return BadRequest(new TransactionResponse()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

                if (!isCorrect)
                {
                    return BadRequest(new TransactionResponse()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var user = new ApplicationUser()
                {
                    Email = existingUser.Email,
                    UserName = existingUser.UserName,
                    Id = existingUser.Id,
                };


                var jwtToken = _jWTConfiguration.GenerateJwtToken(user);

                return Ok(new
                {
                    Message = "Login Successful",
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        //[HttpPost]
        //[Route("RefreshToken")]
        //public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await VerifyAndGenerateToken(tokenRequest);

        //        if (result == null)
        //        {
        //            return BadRequest(new TransactionResponse()
        //            {
        //                Errors = new List<string>() {
        //                    "Invalid tokens"
        //                },
        //                Success = false
        //            });
        //        }

        //        return Ok(result);
        //    }

        //    return BadRequest(new TransactionResponse()
        //    {
        //        Errors = new List<string>() {
        //            "Invalid payload"
        //        },
        //        Success = false
        //    });
        //}

        [HttpGet]
        [Route("GetUsers")]
        //Run and use Postman to call this request
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        [HttpGet]
        [Route("GetUserByUsername")]
        //Run and use Postman to call this request
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
        {
            var user = await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();

            return Ok(user);
        }




    }
}
