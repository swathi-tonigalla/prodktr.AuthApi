using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IUserService _userService;

        public ClientsController( IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            var users = await _userService.GetAllClients();
            return users;
        }
    }
}
