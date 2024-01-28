using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomControllerBase
    {
        private readonly IDiscountServices _discountServices;

        public DiscountsController(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response =await _discountServices.GetAll();
            return CreateActionResultInstance(response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _discountServices.GetById(id);
            return CreateActionResultInstance(response);

        }
        [HttpPost]
        public async Task<IActionResult> Create(Models.Discount discount)
        {
            var response = await _discountServices.Creat(discount);
            return CreateActionResultInstance(response); 

        }
    }
}
