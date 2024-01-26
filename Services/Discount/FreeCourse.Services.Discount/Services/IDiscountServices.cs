using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountServices
    {
        Task<Response<List<Models.Discount>>> GetAll();
        Task<Response<Models.Discount>> GetById(int Id);
        Task<Response<NoContent>> Creat(Models.Discount discount);
        Task<Response<NoContent>> Update(Models.Discount discount);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<Models.Discount>> GetByCodeAndUserId(string code,string userId);
    }
}
