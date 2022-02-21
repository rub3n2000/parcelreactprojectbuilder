using System.Security.Claims;
using System.Threading.Tasks;
using DotnetReact.Models;
using DotnetReact.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DotnetReact.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace DotnetReact.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {       
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IMapper mapper, IConfiguration config){
            this._repo = repo;
            this._mapper = mapper;
            this._config = config;
        }

        [HttpGet("authenticate")]
        public IActionResult Authenticate() {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForAuth userForRegisterDto)
        {
            var userToCreate =  _mapper.Map<User>(userForRegisterDto);
        
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForAuth userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetValue<string>("Token").ToString()));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            DateTime jwtDate = DateTime.Now;

             var jwt = new JwtSecurityToken(
                claims: new Claim[] {new Claim(JwtRegisteredClaimNames.Sub, userFromRepo.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, "test@test.com"),
                        new Claim("id", "Customer_Test"),
                        new Claim("Testcertified", "true"),
                        new Claim("Testing", "true")},
                notBefore: jwtDate,
                audience: "Auth",
                issuer: "Auth",
                expires: jwtDate.AddHours(24),
                signingCredentials: creds
            );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new {token = tokenValue});
        }
    }
}

