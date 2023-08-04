using System;
using System.Collections.Generic;
using AOWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AOWebApp.Data
{
    public partial class AmazonOrdersContext : DbContext
    {
        public AmazonOrdersContext()
        {
        }

        public AmazonOrdersContext(DbContextOptions<AmazonOrdersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemCategory> ItemCategories { get; set; } = null!;
        public virtual DbSet<ItemMarkupHistory> ItemMarkupHistories { get; set; } = null!;
        public virtual DbSet<ItemsInOrder> ItemsInOrders { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=MASTER\\SQLEXPRESS;Initial Catalog=AmazonOrders;Integrated Security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("addressID");

                entity.Property(e => e.AddressLine)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("addressLine");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("postcode");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("region");

                entity.Property(e => e.Suburb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("suburb");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.AddressId).HasColumnName("addressID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.MainPhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mainPhoneNumber");

                entity.Property(e => e.SecondaryPhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("secondaryPhoneNumber");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_address");
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.HasKey(e => e.OrderNumber)
                    .HasName("pk_orderNumber");

                entity.Property(e => e.OrderNumber).HasColumnName("orderNumber");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.DatePaid)
                    .HasColumnType("date")
                    .HasColumnName("datePaid");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customerID");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.ItemCost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("itemCost");

                entity.Property(e => e.ItemDescription)
                    .IsUnicode(false)
                    .HasColumnName("itemDescription");

                entity.Property(e => e.ItemImage)
                    .IsUnicode(false)
                    .HasColumnName("itemImage");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("itemName");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_itemCategory");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("pk_itemCategories");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.CategoryName)
                    .IsUnicode(false)
                    .HasColumnName("categoryName");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parentCategoryID");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("fk_parentCategory");
            });

            modelBuilder.Entity<ItemMarkupHistory>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.StartDate });

                entity.ToTable("ItemMarkupHistory");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.Markup)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("markup")
                    .HasDefaultValueSql("((1.3))");

                entity.Property(e => e.Sale)
                    .IsRequired()
                    .HasColumnName("sale")
                    .HasDefaultValueSql("('False')");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemMarkupHistories)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMarkupHistory_Items");
            });

            modelBuilder.Entity<ItemsInOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderNumber, e.ItemId })
                    .HasName("pk_itemsInOrder");

                entity.ToTable("ItemsInOrder");

                entity.Property(e => e.OrderNumber).HasColumnName("orderNumber");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.NumberOf).HasColumnName("numberOf");

                entity.Property(e => e.TotalItemCost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalItemCost");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsInOrders)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_items");

                entity.HasOne(d => d.OrderNumberNavigation)
                    .WithMany(p => p.ItemsInOrders)
                    .HasForeignKey(d => d.OrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderNumber");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("reviewID");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.ItemId).HasColumnName("itemID");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("date")
                    .HasColumnName("reviewDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReviewDescription)
                    .IsUnicode(false)
                    .HasColumnName("reviewDescription");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_review");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_item_review");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
