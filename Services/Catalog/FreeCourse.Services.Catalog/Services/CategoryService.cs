using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService: ICategoryServices
    {
       private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDataBaseSettings dataBaseSettings)
        {
            _mapper = mapper;
            var client=new MongoClient(dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(dataBaseSettings.DatabaseName);
            _categories = database.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categories.Find(c => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categories.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
        }
        public async Task<Response<CategoryDto>> GetByIdAsync(string Id)
        {
            var category=await _categories.Find(c=>c.Id==Id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not Found", 200);
            }
            return  Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
