using LoginHrSystems.DTOs.Roles;
using LoginHrSystems.DTOs.Users;
using LoginHrSystems.Services.Contract;
using LoginHrSystems.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginHrSystems.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class RolesController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        #region Documentation
        /// <summary>
        /// قائمة الأدوار
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var res = await _roleService.GetAllRolesAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// قائمة صلاحيات المستخدم
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpGet("GetUserPermissions")]
        public async Task<IActionResult> GetUserPermissions(int userId)
        {
            try
            {
                var res = await _userService.GetUserPermissionsAsync(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// إضافة دور
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddingRoleDto dto)
        {
            try
            {
                await _roleService.AddRoleAsync(dto);
                return Ok("Role was added");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// تعديل صلاحيات دور
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPatch("UpdateRolePermissions")]
        public async Task<IActionResult> UpdateRolePermissions(UpdateRolePermissionsDto dto)
        {
            try
            {
                await _roleService.UpdateRolePermissionsAsync(dto);
                return Ok("Role permissions were updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// تعديل صلاحيات مستخدم
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPatch("EditUserPermissions")]
        public async Task<IActionResult> EditUserPermissions(EditUserPermissionsDto dto)
        {
            try
            {
                await _userService.EditUserPermissionsAsync(dto);
                return Ok("User permissions were updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// حذف دور
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPatch("DeleteRole")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            try
            {
                await _roleService.DeleteRoleAsync(roleId);
                return Ok("Role was deleted");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


    }
}
