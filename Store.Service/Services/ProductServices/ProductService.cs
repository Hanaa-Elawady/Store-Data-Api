using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.Dtos;
using Store.Service.Helper;
using Store.Service.Interfaces;

namespace Store.Service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand , int>().GetAllAsNoTrackingAsync();
            IReadOnlyList<BrandsDto> mappedBrands = _mapper.Map<IReadOnlyList<BrandsDto>>(brands);
            return mappedBrands;

        }

        public async Task<PaginatedResultDto<ProductDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecification(input); 

            var products = await _unitOfWork.Repository<Product, int>().GetWithSpecsAllAsync(specs);
            var CountSpecs = new ProductCountWithSpecs(input);
            var Count = await _unitOfWork.Repository<Product, int>().CountSpecsAsync(CountSpecs);
            IReadOnlyList<ProductDto> mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return new PaginatedResultDto<ProductDto>(input.PageIndex ,input.PageSize, Count , mappedProducts);

        }

        public async Task<IReadOnlyList<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();
            IReadOnlyList<TypeDto> mappedtypes = _mapper.Map<IReadOnlyList<TypeDto>>(types);

            return mappedtypes;

        }

        public async Task<ProductDto> GetProductByIdAsync(int? productId)
        {
            if (productId == null)
                throw new Exception("Id is null");

            var specs = new ProductWithSpecification(productId);

            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecsByIdAsync(specs);

            if (product == null)
                throw new Exception("Product not found");

            ProductDto mappedProducts = _mapper.Map<ProductDto>(product);

            return mappedProducts;
        }
    }
}
