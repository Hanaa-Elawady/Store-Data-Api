using Microsoft.AspNetCore.Mvc;
using Store.Service.Dtos.BasketDtos;
using Store.Service.Interfaces;

namespace Store.Web.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task< ActionResult<CustomerBasketDto>> GetBasketAsync(string id)
            => Ok(await _basketService.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            => Ok( await _basketService.UpdateBasketAsync(basket));

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
            => Ok (await _basketService.DeleteBasketAsync(id));

    }
}
