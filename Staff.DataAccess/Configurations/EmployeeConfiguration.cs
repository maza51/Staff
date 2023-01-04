using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Staff.Domain;

namespace Staff.DataAccess.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.MiddleName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.DateBirth).IsRequired();
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(30); ;
        builder.Property(x => x.Email).IsRequired().HasMaxLength(30); ;
        builder.Property(x => x.Position).IsRequired().HasMaxLength(30); ;

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Employees);
    }
}