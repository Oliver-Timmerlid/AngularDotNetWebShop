using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CartRow> CartRows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product - Rating (1-to-many)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Cart (1-to-many)
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - Cart (many-to-many) via CartRow
            modelBuilder.Entity<CartRow>()
                .HasKey(cr => new { cr.CartId, cr.ProductId });

            modelBuilder.Entity<CartRow>()
                .HasOne(cr => cr.Cart)
                .WithMany(c => c.CartRows)
                .HasForeignKey(cr => cr.CartId);

            modelBuilder.Entity<CartRow>()
                .HasOne(cr => cr.Product)
                .WithMany(p => p.CartRows)
                .HasForeignKey(cr => cr.ProductId);
        }

    }
}
