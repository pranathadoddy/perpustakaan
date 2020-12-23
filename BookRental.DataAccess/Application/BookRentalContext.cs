using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookRental.DataAccess.Application
{
    public partial class BookRentalContext : DbContext
    {
        public BookRentalContext()
        {
        }

        public BookRentalContext(DbContextOptions<BookRentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComBook> ComBooks { get; set; }
        public virtual DbSet<ComCustomer> ComCustomers { get; set; }
        public virtual DbSet<ComRental> ComRentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ComBook>(entity =>
            {
                entity.ToTable("com_Book");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(56);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(56);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ComCustomer>(entity =>
            {
                entity.ToTable("com_Customer");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(56);

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(56);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ComRental>(entity =>
            {
                entity.ToTable("com_Rental");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(56);

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(56);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ComRentals)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_com_Book_to_com_Rental");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ComRentals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_com_Customer_to_com_Rental");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
