using FreeCourse.Shared.Dtos;
using FreeCourses.Services.Basket.Dtos;

namespace FreeCourses.Services.Basket.Services
{
    public interface IBasketServices
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> DeleteBasket(string userId);
        Task<Response<bool>> DeleteItem(string BasketDtoId);
    }
}
