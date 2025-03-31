using Application.Data;
using Domain.Entities.Categories;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Roles;
using Domain.Entities.ShoppingCarts;
using Domain.Entities.Users;
using Domain.Entities.Waiters;
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Categories;
using Infrastructure.Persistence.Repositories.Orders;
using Infrastructure.Persistence.Repositories.Products;
using Infrastructure.Persistence.Repositories.Roles;
using Infrastructure.Persistence.Repositories.ShoppingCarts;
using Infrastructure.Persistence.Repositories.Users;
using Infrastructure.Persistence.Repositories.Waiters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IApplicationDbContext>(options =>
                options.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(options =>
                options.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWaiterRepository, WaiterRepository>();

            return services;
        }
    }
}
