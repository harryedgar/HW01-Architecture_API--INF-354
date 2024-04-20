using Architecture_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architecture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var results = await _courseRepository.GetAllCourseAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            try
            {
                var newCourse = await _courseRepository.AddCourseAsync(course);
                return CreatedAtAction(nameof(AddCourse), new { id = newCourse.CourseId }, newCourse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPut]
        [Route("EditCourse/{id}")]
        public async Task<IActionResult> EditCourse(int id, [FromBody] Course course)
        {
            try
            {
                var existingCourse = await _courseRepository.GetCourseIdAsync(id);

                if (existingCourse == null)
                {
                    return NotFound();
                }

                existingCourse.Name = course.Name;
                existingCourse.Description = course.Description;
                existingCourse.Duration = course.Duration;

                await _courseRepository.EditCourseAsync(existingCourse);

                return Ok(existingCourse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseId(int id)
        {
            var course = await _courseRepository.GetCourseIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }                                                                                                                                                        

        [HttpDelete]
        [Route("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var courseToDelete = await _courseRepository.GetCourseIdAsync(id);

                if (courseToDelete == null)
                {
                    return NotFound();
                }

                await _courseRepository.DeleteCourseAsync(courseToDelete);

                return Ok("Course deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
    }
}
