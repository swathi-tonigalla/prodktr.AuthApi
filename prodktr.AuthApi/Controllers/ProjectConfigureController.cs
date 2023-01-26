using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<ActionResult<List<Project>>> GetAllConfiguredProjects()
        {

            var response = await _projectService.GetAllConfiguredProjects();
            return response;
        }
        //[HttpGet("get-all-configure-project")]
        //public async Task<ActionResult<string>> GetAllConfiguredProjects()
        //{

        //    var response =  await _projectService.LoadJson();
        //    return response;
        //}

    }
}
