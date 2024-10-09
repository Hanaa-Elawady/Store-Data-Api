using Microsoft.OpenApi.Models;

namespace Store.Web.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc(
                    "v1"
                    , new OpenApiInfo {
                        Title = "Store Api"
                        ,Version = "v1" 
                        ,Contact= new OpenApiContact
                        {
                            Name ="Hanaa Mahmoud",
                            Email = "hanaa.m.elawady@gmail.com",
                            Url = new Uri("https://www.facebook.com/"),
                        }
                    });

                var securetyScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id ="bearer",
                        Type = ReferenceType.SecurityScheme
                    }

                };
                options.AddSecurityDefinition("bearer", securetyScheme);
                var secuertyRequirements = new OpenApiSecurityRequirement
                {
                    {securetyScheme , new[] {"bearer"} }
                };

                options.AddSecurityRequirement(secuertyRequirements);

            });

            return services;
        }
    }
}
