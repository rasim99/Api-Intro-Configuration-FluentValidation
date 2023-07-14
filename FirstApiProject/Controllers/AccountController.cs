using FirstApiProject.Dtos.Account;
using FirstApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult>Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user !=null) return BadRequest();
             user = new AppUser();
            user.UserName = registerDto.UserName;
            user.FullName = registerDto.FullName;
            user.Email = registerDto.Email;
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet] 
        public async Task<IActionResult> CreateRole()
        {
           var result= await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            result= await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            result= await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            return StatusCode(StatusCodes.Status201Created);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            AppUser user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
                if (user == null)
                {
                    return NotFound();
                }
            }
            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!result)
            {
                return NotFound();
            }

            //generate token

            var userRoles=await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var claimList=new List<Claim>();
            claimList.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));    
            claimList.Add(new Claim(ClaimTypes.Name, user.UserName));    
            claimList.Add(new Claim(ClaimTypes.Email, user.Email));    
            claimList.Add(new Claim("role", userRoles[0]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            


            return Ok(new { token =  tokenHandler.WriteToken(token), message = "Succeeded" });
        }
    }
}
