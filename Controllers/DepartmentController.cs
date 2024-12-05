using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DepartmentModel>>> GetDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin,User")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DepartmentModel>> GetDepartment(long id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult<DepartmentModel>> PostDepartment(DepartmentModel department)
        {
            try
            {
                var createdDepartment = await _departmentService.CreateDepartmentAsync(department);
                return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.DepartmentId }, createdDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutDepartment(long id, DepartmentModel department)
        {
            try
            {
                if (id != department.DepartmentId)
                {
                    return BadRequest();
                }

                var updateSuccessful = await _departmentService.UpdateDepartmentAsync(department);
                if (!updateSuccessful)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteDepartment(long id)
        {
            try
            {
                var deleteSuccessful = await _departmentService.DeleteDepartmentAsync(id);
                if (!deleteSuccessful)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}