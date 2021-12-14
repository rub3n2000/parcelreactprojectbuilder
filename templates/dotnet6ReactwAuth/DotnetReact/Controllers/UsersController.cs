using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetReact.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DotnetReact.Models.Dto;

namespace DotnetReact.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {       
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IAuthRepository repo, IMapper mapper){
            this._repo = repo;
            this._mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForProfile>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet] [AllowAnonymous]
        [Route("{username}")]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            var user = await _repo.GetUser(username);
            var userToReturn = _mapper.Map<UserForProfile>(user);
            return Ok(userToReturn);
        }

        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string email)
        {
            await _repo.DeleteUser(email);
            return Ok();
        }
    }
}

