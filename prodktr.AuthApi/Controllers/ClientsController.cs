using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ClientsController(IMapper mapper)
        {
            this._mapper = mapper;
        }
        [HttpGet("GetAll")]
        public ActionResult<string> TestAuthorization()
        {
            return Ok("You're authorized!");
        }
    }
}
