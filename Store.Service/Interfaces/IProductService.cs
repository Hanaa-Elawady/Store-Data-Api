using Store.Repository.Specification.ProductSpecs;
using Store.Service.Dtos;
using Store.Service.Helper;

namespace Store.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int? productId);
        Task<PaginatedResultDto<ProductDto>> GetAllProductsAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<TypeDto>> GetAllTypesAsync();


    }
}
