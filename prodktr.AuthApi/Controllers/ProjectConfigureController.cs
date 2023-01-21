using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectConfigureController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectConfigureController(IProjectService projectService)
        {
            this._projectService = projectService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<projectconfigured>>> GetAllConfiguredProjects()
        {
            var users = await _projectService.GetAllConfiguredProjects();
            return users;
        }
    }
}
