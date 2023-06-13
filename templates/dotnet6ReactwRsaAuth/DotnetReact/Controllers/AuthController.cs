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
using System.Security.Cryptography;
using System;

namespace DotnetReact.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IMapper mapper, IConfiguration config)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._config = config;
        }

        [HttpGet("authenticate")]
        public IActionResult Authenticate()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForAuth userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

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

            using RSA rsa = RSA.Create();

            rsa.ImportRSAPrivateKey( // Convert the loaded key from base64 to bytes.
                source: Convert.FromBase64String(_config.GetValue<string>("JwtPrivate")), // Use the private key to sign tokens
                bytesRead: out int _); // Discard the out variable

            var signingCredentials = new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256Signature // Important to use RSA version of the SHA algo
            ){CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }};

            DateTime jwtDate = DateTime.Now;

            var jwt = new JwtSecurityToken(
                claims: new Claim[] {new Claim(JwtRegisteredClaimNames.Sub, userFromRepo.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, "test@mail.com"),
                        new Claim("id", "Customer_Test"),
                        new Claim("TestingCertified", "true"),
                        new Claim("Testing", "true")},
                notBefore: jwtDate,
                audience: "Auth",
                issuer: "Auth",
                expires: jwtDate.AddHours(24),
                signingCredentials: signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var returnValue = new
            {
                jwt = token,
                unixTimeExpiresAt = new DateTimeOffset(jwtDate).ToUnixTimeMilliseconds(),
            };

            return Ok(returnValue);
        }
    }
}

