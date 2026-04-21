using Scalar.AspNetCore;

namespace Payment_api.WebAPI.Extensions;

public static class ScalarConfig
{
    public static void AddScalar(this IServiceCollection services)
    {        
        services.AddSwaggerGen();
    }

    public static void UseScalarDocs(this WebApplication app)
    {
        app.MapScalarApiReference("/scalar", options =>
        {
            options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
        });
   
    }
}
