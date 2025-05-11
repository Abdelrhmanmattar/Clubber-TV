using code_quests.Core.DTOs;
using code_quests.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace code_quests.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserApp _userApp;

        public AccountController(IUserApp userApp)
        {
            _userApp = userApp;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(Regisiter_DTO _DTO)
        {
            if (ModelState.IsValid)
            {
                var regisiter_ = await _userApp.Regisiter_User(_DTO);
                if (regisiter_.StatusCode == HttpStatusCode.Created)
                {
                    return Ok(regisiter_);
                }
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogIn_DTO _DTO)
        {
            if (ModelState.IsValid)
            {
                var logIN = await _userApp.LogIn_User(_DTO);
                if (logIN.StatusCode == HttpStatusCode.OK) { return Ok(logIN); }
            }
            return BadRequest();
        }
    }
}
