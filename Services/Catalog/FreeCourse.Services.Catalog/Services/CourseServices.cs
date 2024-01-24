using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseServices: ICourseServices
    {
        private readonly IMongoCollection<Course> _course;
        private readonly IMongoCollection<Category> _category;
        private readonly IMapper _mapper;
        public CourseServices(IMapper mapper, IDataBaseSettings dataBaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(dataBaseSettings.DatabaseName);
            _course = database.GetCollection<Course>(dataBaseSettings.CourseCollectionName);
            _category = database.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync(Expression<Func<Course, bool>>? predicate = null)
        {
            var courseList = await _course.Find(predicate).ToListAsync();
            if (courseList.Any())
            {
                foreach (var course in courseList)
                {
                    course.Category = await _category.Find(c => c.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courseList = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courseList),200);
        }
        public async Task<Response<CourseDto>> GetById(string Id)
        {
            var course = await _course.Find(x => x.Id == Id).FirstAsync();
            if (course==null)
            {
                return Response<CourseDto>.Fail("Course Not Fount", 404);
            }
            course.Category =await _category.Find(c => c.Id == course.CategoryId).FirstOrDefaultAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
                
         }
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto course)
        {
            var newcourse = _mapper.Map<Course>(course);
            newcourse.CreatedDate = DateTime.UtcNow;
            await _course.InsertOneAsync(newcourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newcourse), 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseDto)
        {
            var updateCourse = _mapper.Map<Course>(courseDto);
            var result= await _course.FindOneAndReplaceAsync(c => c.Id == courseDto.Id,updateCourse);
            if (result==null)
            {
                return Response<NoContent>.Fail("Course not Found", 404);
            }
            return Response<NoContent>.Success(204);
        }
        public async Task<Response<NoContent>> DeleteAsync(string Id)
        {
            var result=await _course.DeleteOneAsync(c => c.Id == Id);
            if (result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Course not Found", 404);
        }


    }
}
