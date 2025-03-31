using Domain.Entities.Categories;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Roles;
using Domain.Entities.ShoppingCarts;
using Domain.Entities.Users;
using Domain.Entities.Waiters;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Waiter> Waiters { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
