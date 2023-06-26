using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.Mappers;
using Payment_api.Application.Services;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.Services;
using Payment_api.Domain.Services;
using Payment_api.Infra.Data.Context;
using Payment_api.Infra.Data.Repositories;

namespace Payment_api.Infra.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Mysql");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connection,ServerVersion.AutoDetect(connection)
                ,m => m.MigrationsAssembly("Payment-api.WebAPI")
                )
            );                

            #region Repositories
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPhoneRepository,PhoneRepository>();
            services.AddScoped<ISaleRepository,SaleRepository>();
            services.AddScoped<ISellerRepository,SellerRepository>();            
            #endregion

            #region Services            
            services.AddScoped<ISellerService,SellerService>();
            services.AddScoped<ISaleService,SaleService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<ICategoryAppService,CategoryAppService>();
            services.AddScoped<IProductAppService,ProductAppService>();
            services.AddScoped<ISellerAppService,SellerAppService>();
            services.AddScoped<IOrderAppService,OrderAppService>();
            services.AddScoped<ISaleAppService,SaleAppService>();            
            #endregion
        
            services.AddAutoMapper(typeof(MapperConfigProfile));

            return services;
        }
    }
}