﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodktr.AuthApi.Data.Interfaces;
using prodktr.AuthApi.Services;
using prodktr.AuthApi.Services.Interfaces;
using System.Data;

namespace prodktr.AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IAuthService _authService;

        public AuthController(IAuthRepository authRepo,IAuthService authService)
        {
            _authRepo = authRepo;
            this._authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserDto request)
        {
            var response = await _authService.RegisterUser(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

       
        [HttpPost("Login")]
        public async Task<AuthResponseDto> Login(UserDto request)
        {
            var response = await _authService.Authenticate(request);
            
            return response;
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var response = await _authService.RefreshToken();
            //if (response.Success)
            //    return Ok(response);

            return "";
        }
        [HttpGet, Authorize(Roles = "User,Administrator")]
        public ActionResult<string> TestAuthorization()
        {
            return Ok("You're authorized!");
        }
    }
}
