using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodktr.AuthApi.Services.Interfaces;
using System.Data;

namespace prodktr.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashBoardController( IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }
        [HttpGet("Instrumentconfig/{id}")]
        public async Task<ActionResult<InstrumentConfuguredByYouDto>> GetInstrumentconfig_Configby_You(string id)
        {
            var users = await _dashboardService.GetInstrumentconfig_Configby_You(id);
            return users;
        }
        [HttpGet("AllInstrumentConfig")]
        public async Task<ActionResult<InstrumentResponseDto>> GetAllInstrumentConfigs()
        {
            return await _dashboardService.GetAllInstrumentConfigs();
        }
    }
}
