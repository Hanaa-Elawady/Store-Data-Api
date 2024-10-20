using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context , ILoggerFactory loggerFactory)
        {
            try 
            { 
                if(context.ProductBrands !=null && !context.ProductBrands.Any())
                {
                    var BrandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    if (brands != null)
                    {
                        await context.ProductBrands.AddRangeAsync(brands);
                    }
                }
                if(context.deliveryMethods !=null && !context.deliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    if (deliveryMethods != null)
                    {
                        await context.deliveryMethods.AddRangeAsync(deliveryMethods);
                    }
                }
                if(context.ProductTypes !=null && !context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    if (Types != null)
                    {
                        await context.ProductTypes.AddRangeAsync(Types);
                    }
                }
                if(context.Products !=null && !context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (products != null)
                    {
                        await context.Products.AddRangeAsync(products);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
