using Store.Data.Entities;
using System.Linq.Expressions;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductCountWithSpecs : BaseSpecification<Product>
    {
        public ProductCountWithSpecs(ProductSpecification specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
                             && (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
                             && (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))

            )
        {

        }
    }
}
