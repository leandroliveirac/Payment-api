using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.Mappers;
using Payment_api.Application.Services;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Services;
using Payment_api.Domain.Services.Interfaces;
using Payment_api.Infra.Data.Context;
using Payment_api.Infra.Data.Repositories;

namespace Payment_api.Infra.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=DbTeste.db",
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

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
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ISellerService,SellerService>();
            services.AddScoped<ISaleService,SaleService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<ISellerAppService,SellerAppService>();
            services.AddScoped<IOrderAppService,OrderAppService>();

            
            #endregion
        
            services.AddAutoMapper(typeof(MapperConfigProfile));

            return services;
        }
    }
}