using AutoMapper;
using ProductManagementAPI.DTO;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ProductManagementAPI.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration _appConfig { get; }
        public IMapper _mapper { get; }
        IAccountDBRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountDBRepository accRepo,
                                 IMapper mapper,
                                 IConfiguration appConfig,
                                 UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _repo = accRepo;
            _mapper = mapper;
            _appConfig = appConfig;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Signup")] // route attribute
        public async Task<IActionResult> Register(SignUpDTO userDTO) // method
        {
            var user = _mapper.Map<ApplicationUser>(userDTO); // to map the SignUpDTO to an ApplicationUser object

            if (ModelState.IsValid)
            {
                var val = await _repo.SignUpUserAsync(user, userDTO.Password); // repository interface
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var issuer = _appConfig["JWT:Issuer"];
            var audience = _appConfig["JWT:Audience"];
            var key = _appConfig["JWT:Key"];

            if (ModelState.IsValid)
            {
                var loginResult = await _repo.SignInUserAsync(loginDTO); // user authentication
                if (loginResult.Succeeded)
                {
                    // generate a token
                    var user = await _repo.FindUserByEmailAsync(loginDTO.UserName);
                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id) // Set the user ID as NameIdentifier claim
                        };


                        var keyBytes = Encoding.UTF8.GetBytes(key);
                        var theKey = new SymmetricSecurityKey(keyBytes); 
                        var creds = new SigningCredentials(theKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(issuer, audience, null, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

                        // token is serialized
                        //  converting a token into a standardized format that can be transmitted or stored - JSON format
                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), userId = user.Id });
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid username or password!"); // wrong credentials for login
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }


    }
}
