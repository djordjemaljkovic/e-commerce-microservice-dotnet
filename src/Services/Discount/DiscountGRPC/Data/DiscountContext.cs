using DiscountGRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountGRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                    new Coupon { Id = 1 , ProductName = "Iphone", Description = "Apple mobile phone", Amount = 1300},
                    new Coupon { Id = 2 , ProductName = "Samsung", Description = "Android mobile phone", Amount = 900 }
                ); 
        }
    }
}
