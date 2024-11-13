using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.SqlServer.Configuration
{
    public class DonutConfiguration : IEntityTypeConfiguration<Donut>
    {
        public void Configure(EntityTypeBuilder<Donut> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(19,6)");

            builder.ToTable(table =>
            {
                table.HasCheckConstraint("CK_Donuts_Price", "[Price]>=0");
            });
        }
    }
}
