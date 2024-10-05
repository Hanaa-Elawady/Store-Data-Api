using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repository;
using Store.Service.Dtos.Profiles;
using Store.Service.HandleResponse;
using Store.Service.Interfaces.ProductInterfaces;
using Store.Service.Services.CacheServices;
using Store.Service.Services.ProductServices;

namespace Store.Web.Extensions
{
    public static class AppServiceExtenstion
    {
        public static IServiceCollection AddApllicationService(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {   
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(model => model.Value?.Errors.Count > 0)
                                .SelectMany(model => model.Value?.Errors)
                                .Select(error => error.ErrorMessage)
                                .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;    
        }

    }
}
