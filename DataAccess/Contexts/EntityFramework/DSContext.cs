using AppCore.DataAccess.Configs;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.EntityFramework
{
    public class DSContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<PostNumber> PostNumbers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductionFacility> ProductionFacilities { get; set; }
        public DbSet<ProductionFacilityProduct> ProductionFacilityProducts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasOne(u => u.District)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasOne(u => u.City)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasOne(u => u.PostNumber)
                .WithMany(pN => pN.Users)
                .HasForeignKey(u => u.PostNumberId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserProduct>()
                .HasOne(uP => uP.Product)
                .WithMany(p => p.Users)
                .HasForeignKey(uP => uP.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserProduct>()
                .HasOne(uP => uP.User)
                .WithMany(u => u.Products)
                .HasForeignKey(uP => uP.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductionFacility>()
                .HasOne(pF => pF.District)
                .WithMany(d => d.ProductionFacilities)
                .HasForeignKey(pF => pF.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductionFacilityProduct>()
                .HasOne(pFP => pFP.Product)
                .WithMany(p => p.ProductionFacilities)
                .HasForeignKey(pFP => pFP.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductionFacilityProduct>()
                .HasOne(pFP => pFP.ProductionFacility)
                .WithMany(pF => pF.Products)
                .HasForeignKey(pFP => pFP.ProductionFacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(user => user.EMail)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Password)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Name)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(product => product.Name)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(product => product.Price)
                .IsUnique();
        }
    }
}
