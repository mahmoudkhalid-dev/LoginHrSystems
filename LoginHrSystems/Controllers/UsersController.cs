using LoginHrSystems.DTOs.Users;
using LoginHrSystems.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginHrSystems.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        #region Documentation
        /// <summary>
        /// تسجيل دخول
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var res = await _userService.LoginAsync(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// إنشاء مستخدم
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPost("CreateUser")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult CreateUser(CreateUserDto dto)
        {
            var res = _userService.CreateUserAsync(dto);
            return Ok("User Created Successfully");
        }

        #region Documentation
        /// <summary>
        /// تغيير دور المستخدم
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPatch("ChangeUserRoles")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult ChangeUserRoleDto(ChangeUserRoleDto dto)
        {
            var res = _userService.ChangeUserRoleAsync(dto);
            return Ok("User Roles Updated Successfully");
        }

    }
}
