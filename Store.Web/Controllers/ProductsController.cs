using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.Dtos;
using Store.Service.Interfaces.ProductInterfaces;
using Store.Web.Helper;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandsDto>>> GetAllBrands()
            =>Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes()
            =>Ok(await _productService.GetAllTypesAsync());

        [HttpGet]
        [Cache(30)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery]ProductSpecification input)
            =>Ok(await _productService.GetAllProductsAsync(input));

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int? id)
            =>Ok(await _productService.GetProductByIdAsync(id));
    }
}
 