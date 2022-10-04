using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Context.EFConfigurations
{
    // public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    // {
    //     public void Configure(EntityTypeBuilder<Admin> builder)
    //     {
    //         builder.ToTable("Admins");
    //         builder.Property(a => a.Id)
    //         .HasColumnType("int").IsRequired();
    //         builder.HasIndex(e => e.Email).IsUnique();
            
    //     }
    // }
}