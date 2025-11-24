using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentaion.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        //Login
        // Post : BaseUrl/api/Authentication/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var Result = await _authenticationService.LoginAsync(loginDTO);
            return HandleResult(Result);
        }

        // Register
        // Post : BaseUrl/api/Authentication/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var Result = await _authenticationService.RegisterAsync(registerDTO);
            return HandleResult(Result);
        }


        // Get : Get/api/Authentication/emailexists
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        // Get : Get/api/Authentication/CurrentUser
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimValueTypes.Email)!;
            var Result = await _authenticationService.GetUserByEmailAsync(Email);
            return HandleResult(Result);
        }

    }
}
