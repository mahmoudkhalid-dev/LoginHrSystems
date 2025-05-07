using LoginHrSystems.DTOs.Employees;
using LoginHrSystems.DTOs.Roles;
using LoginHrSystems.DTOs.Users;
using LoginHrSystems.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace LoginHrSystems.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        #region Documentation
        /// <summary>
        /// قائمة الموظفين
        /// </summary>
        /// <remarks>
        /// # Sample request:
        /// ```json
        /// GET /GetEmployees
        /// {
        ///     "applyNameFilter": true,
        ///     "name": "Ali",
        ///     "applyJobTitleFilter": true,
        ///     "jobTitle": "Dev",
        ///     "applySalaryRange": true,
        ///     "salaryFrom": 500,
        ///     "salaryTo": 5000,
        ///     "isDetailed": true,
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpGet("GetEmployees")]
        [Authorize(Policy = "Permission:GetAllEmployees")]
        public async Task<IActionResult> GetEmployees(
            bool applyNameFilter,
            string? name,
            bool applyJobTitleFilter,
            string? jobTitle,
            bool applySalaryRange,
            double salaryFrom,
            double salaryTo,
            bool isDetailed
        )
        {
            try
            {
                var res = await _employeeService.GetEmployeesAsync(applyNameFilter, name, applyJobTitleFilter, jobTitle, applySalaryRange, salaryFrom, salaryTo, isDetailed);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        #region Documentation
        /// <summary>
        /// تفاصيل موظف
        /// </summary>
        /// <remarks>
        /// # Sample response:
        /// ```json
        /// GET /GetEmployeesDetails
        /// {
        ///     "id": 5,
        ///     "code": 1,
        ///     "name": "Ahmed",
        ///     "phone": "01227383543",
        ///     "email": "ahmed@gmail.com",
        ///     "jobTitle": "dev",
        ///     "address": "tanra",
        ///     "department": "dev",
        ///     "yearsOfExperience": 3,
        ///     "salary": 5000,
        ///     "salaryCurrency": "USD",
        ///     "bonus": 500,
        ///     "bonusCurrency": "USD",
        ///     "createdAt": "2025-05-08T02:16:31.1982547",
        ///     "isActive": true,
        ///     "isDeleted": false
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpGet("GetEmployeesDetails")]
        [Authorize(Policy = "Permission:GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeesDetails(int employeeId)
        {
            try
            {
                var res = await _employeeService.GetEmployeeDetailsAsync(employeeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// إضافة موظف
        /// </summary>
        /// <remarks>
        /// # Sample request:
        /// ```json
        /// HttpPost /AddEmployee
        /// {
        ///     "Code": 1,
        ///     "Name": "string",
        ///     "Phone": "string",
        ///     "Email": "string",
        ///     "JobTitle": "string",
        ///     "Address": "string",
        ///     "Department": "string",
        ///     "YearsOfExperience": 3,
        ///     "Salary": 1.55,
        ///     "SalaryCurrency": "string",
        ///     "Bonus": 1.55,
        ///     "BonusCurrency": "string",
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPost("AddEmployee")]
        [Authorize(Policy = "Permission:AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddingEmployeeDto dto)
        {
            try
            {
                await _employeeService.AddEmployeeAsync(dto);
                return Ok("Employee was added");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// تعديل بيانات موظف
        /// </summary>
        /// <remarks>
        /// # Sample request:
        /// ```json
        /// HttpPatch /UpdateEmployee
        /// {
        ///     "applyNameFilter": true,
        ///     "name": "Ali",
        ///     "applyJobTitleFilter": true,
        ///     "jobTitle": "Dev",
        ///     "applySalaryRange": true,
        ///     "salaryFrom": 500,
        ///     "salaryTo": 5000,
        ///     "isDetailed": true,
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpPatch("UpdateEmployee")]
        [Authorize(Policy = "Permission:UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, UpdateEmployeeDto dto)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(employeeId, dto);
                return Ok("Employee was updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        #region Documentation
        /// <summary>
        /// حذف موظف
        /// </summary>
        /// <remarks>
        /// # Sample request:
        /// ```json
        /// HttpDelete /DeleteEmployee
        /// {
        ///     "employeeId": 1
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request or exception</response>
        /// <response code="500">Internal server error</response>
        #endregion
        [HttpDelete("DeleteEmployee")]
        [Authorize(Policy = "Permission:DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(employeeId);
                return Ok("Employee were deleted");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

    }
}
