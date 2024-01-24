using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;
using System.Linq.Expressions;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseServices
    {
        Task<Response<List<CourseDto>>> GetAllAsync(Expression<Func<Course, bool>>? predicate = null);
        Task<Response<CourseDto>> GetById(string Id);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto course);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseDto);
        Task<Response<NoContent>> DeleteAsync(string Id);
    }
}
