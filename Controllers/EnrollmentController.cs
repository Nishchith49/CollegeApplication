using CollegeApplication.IService;
using CollegeApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<EnrollmentModel>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin,User")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<EnrollmentModel>> GetEnrollmentById(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                if (enrollment == null)
                    return NotFound();

                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult<EnrollmentModel>> CreateEnrollment(EnrollmentModel enrollmentModel)
        {
            try
            {
                var createdEnrollment = await _enrollmentService.CreateEnrollmentAsync(enrollmentModel);
                return CreatedAtAction(nameof(GetEnrollmentById), new { id = createdEnrollment.EnrollmentId }, createdEnrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, EnrollmentModel enrollmentModel)
        {
            try
            {
                if (id != enrollmentModel.EnrollmentId)
                    return BadRequest("Enrollment Id mismatch");

                var result = await _enrollmentService.UpdateEnrollmentAsync(id, enrollmentModel);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            try
            {
                var result = await _enrollmentService.DeleteEnrollmentAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}