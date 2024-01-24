using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomControllerBase
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) 
        {
            var response = await _courseServices.GetById(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseServices.GetAllAsync(c=>true);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api[controller]/GetAllUserById/{userId}")]
        public async Task<IActionResult> GetAllUserById(string userId)
        {
            var response = await _courseServices.GetAllAsync(c=>c.UserId== userId);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseServices.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);

        }
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto course)
        {
            var response = await _courseServices.UpdateAsync(course);
            return CreateActionResultInstance(response);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseServices.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }

    }
}
