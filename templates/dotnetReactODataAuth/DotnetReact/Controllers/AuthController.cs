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
    [ApiController]
    [Route("[controller]")]
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

            [AllowAnonymous] [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] UserForAuth userForRegisterDto)
            {
                var userToCreate =  _mapper.Map<User>(userForRegisterDto);
            
                var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

                return StatusCode(201);
            }
            [AllowAnonymous] [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] UserForAuth userForLoginDto)
            {
                var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);
                if (userFromRepo == null)
                {
                    return Unauthorized();
                }

                var claims = new[]{
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Username.ToString())
                };
            
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Token").ToString()));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires = System.DateTime.Now.AddDays(1).ToUniversalTime(),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new {token = tokenHandler.WriteToken(token)});
            }
        }
}

