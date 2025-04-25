using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnType("uuid");

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.IsCancelled)
                .IsRequired();

            builder.HasOne(s => s.Customer)
                .WithMany()
                .IsRequired();

            builder.Property(s => s.Branch)
                .IsRequired();

            
            builder.HasMany(s => s.SaleItems)
                .WithOne()
                .HasForeignKey("SaleId");
        }
    }
}
