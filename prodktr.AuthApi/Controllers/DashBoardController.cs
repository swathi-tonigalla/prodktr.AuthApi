using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> TestAuthorization()
        {
            return Ok("You're authorized!");
        }
    }
}
