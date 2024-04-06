using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AutoService.Entities
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=TradeEntities")
        {
        }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<PickupPoint> PickupPoint { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.NameClient)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasOptional(e => e.OrderProduct)
                .WithRequired(e => e.Order);

            modelBuilder.Entity<OrderProduct>()
                .Property(e => e.Count)
                .IsUnicode(false);

            modelBuilder.Entity<PickupPoint>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<PickupPoint>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.PickupPoint)
                .HasForeignKey(e => e.OrderPickupPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderProduct)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.UserRole)
                .WillCascadeOnDelete(false);
        }
    }
}
