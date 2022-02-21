using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetReact.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DotnetReact.Models.Dto;

namespace DotnetReact.Controllers
{
[Authorize]
[Route("[controller]")]
[ApiController]
public class UsersController : Controller
{       
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IAuthRepository repo, IMapper mapper){
            this._repo = repo;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForProfile>>(users);
            return Ok(usersToReturn);
        }

        [HttpDelete]
        [Route("Users({username})")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] string username)
        {
            await _repo.DeleteUser(username);
            return Ok();
        }

}
}

