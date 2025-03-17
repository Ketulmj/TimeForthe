using Microsoft.AspNetCore.Mvc;
using TimeFourthe.Entities;
using TimeFourthe.Services;
using IdGenerator;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace TimeFourthe.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // For login
        [HttpPost("user/login")]
        public async Task<IActionResult> GetUsers([FromBody] User user)
        {
            var userExist = await _userService.GetUserAsync(user.Email);
            if (userExist != null)
            {
                if (userExist.Password == user.Password)
                {

                    // Response.Cookies.Append("auth", userExist.UserId);
                    return Ok(new { error = false, redirectUrl = "/timetable", message = "Succesfully Login" });
                }
                else
                {
                    return Ok(new { error = true, redirectUrl = "/login", message = "Password Incorrect" });
                }
            }
            return Ok(new { error = true, redirectUrl = "/login", message = "User not exists", data = userExist });
        }

        // for signup
        [HttpPost("user/signup")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userService.CreateUserAsync(user);
            // cookie generate
            // return Ok(new { orgId = user.OrgId, userId = user.UserId, name = user.Name, role = user.Role });

            return Ok(new { message = "User created successfully", id = user.Id });
        }

        // get teachers by OrgId
        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var teacherlist = await _userService.GetTechersByOrgIdAsync(Request.Query["OrgId"].ToString());
            return Ok(teacherlist);
        }

        [HttpGet("data")]
        public IActionResult GetModel()
        {
            return Ok(new { id = "Name" });
        }
    }
}