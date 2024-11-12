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
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeService _collegeService;

        public CollegeController(ICollegeService collegeService)
        {
            _collegeService = collegeService;
        }

        
        //[Authorize(Roles = "Admin,User")]
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CollegeModel>>> GetColleges()
        {
            try
            {
                var colleges = await _collegeService.GetAllCollegesAsync();
                return Ok(colleges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       // [Authorize(Roles = "Admin,User")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<CollegeModel>> GetCollege(long id)
        {
            try
            {
                var college = await _collegeService.GetCollegeByIdAsync(id);
                if (college == null)
                {
                    return NotFound();
                }
                return Ok(college);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CollegeModel>> PostCollege(CollegeModel college)
        {
            try
            {
                var createdCollege = await _collegeService.CreateCollegeAsync(college);
                return CreatedAtAction(nameof(GetCollege), new { id = createdCollege.CollegeId }, createdCollege);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutCollege(long id, CollegeModel college)
        {
            try
            {
                if (id != college.CollegeId)
                {
                    return BadRequest();
                }

                var updateSuccessful = await _collegeService.UpdateCollegeAsync(college);
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
        public async Task<IActionResult> DeleteCollege(long id)
        {
            try
            {
                var deleteSuccessful = await _collegeService.DeleteCollegeAsync(id);
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