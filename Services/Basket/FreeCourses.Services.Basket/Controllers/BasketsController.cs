using FreeCourse.Shared.ControllerBases;
using FreeCourses.Services.Basket.Dtos;
using FreeCourses.Services.Basket.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourses.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController:CustomControllerBase
    {
        private readonly IBasketServices _basketServices;

        public BasketsController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }
        [HttpGet]
        public  async Task<IActionResult> Get()
        {
            return CreateActionResultInstance( await _basketServices.GetBasket("test"));
        }
        [HttpPost]
        public async Task<IActionResult> SaveorUpdate(BasketDto basketDto)
        {
            return CreateActionResultInstance(await _basketServices.SaveOrUpdate(basketDto));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string Id)
        {
            return CreateActionResultInstance(await _basketServices.DeleteBasket(Id));
        }
    }
}
