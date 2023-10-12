using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourProjectName.Models;

namespace YourProjectName.Infrastructure.Configuration
{
    public class MyDataConfiguration : IEntityTypeConfiguration<MyData>
    {
        public void Configure(EntityTypeBuilder<MyData> builder)
        {
            builder.ToTable("MyData");
            builder.HasKey(b => b.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.Contact)
                .HasMaxLength(150)
                .IsUnicode(false);
            builder.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        }
    }
}
