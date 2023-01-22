using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [Route("api/")]
    [ApiController]
    public class ProjectConfigureController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectConfigureController(IProjectService projectService)
        {
            this._projectService = projectService;
        }
        [HttpGet("get-all-configure-project")]
        public async Task<ActionResult<List<ProjectResponse>>> GetAllConfiguredProjects()
        {
            return await _projectService.GetAllConfiguredProjects();
        }
    }
}
