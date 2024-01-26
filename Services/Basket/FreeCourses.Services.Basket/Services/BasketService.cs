using FreeCourse.Shared.Dtos;
using FreeCourses.Services.Basket.Dtos;
using System.Text.Json;

namespace FreeCourses.Services.Basket.Services
{
    public class BasketService : IBasketServices
    {
        private readonly RedisServices _redisServices;

        public BasketService(RedisServices redisServices)
        {
            _redisServices = redisServices;
        }

        public async Task<Response<bool>> DeleteBasket(string userId)
        {
            var status = await _redisServices.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Deleted not success", 500);
        }

        public Task<Response<bool>> DeleteItem(string BasketDtoId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket=await _redisServices.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Basket is Not Fount",404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket),200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisServices.GetDb().StringSetAsync(basketDto.UserId,JsonSerializer.Serialize(basketDto));
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Could not Update or Save",500);
        }
    }
}
