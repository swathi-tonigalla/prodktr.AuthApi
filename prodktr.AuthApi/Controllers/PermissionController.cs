using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodktr.AuthApi.Services;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [Route("api/")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            this._permissionService = permissionService;
        }
        [HttpGet("get-net-user-permission/{uid}")]
        public async Task<ActionResult<UserPermissions>> GetNetUserPermission(string uid)
        {
            var users = await _permissionService.GetNetUserPermission(uid);
            return users;
        }
    }
}
