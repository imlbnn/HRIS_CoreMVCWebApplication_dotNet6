using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Persistence.Configurations
{
    public class CustomEmployeeConfiguration : IEntityTypeConfiguration<CustomEmployee>
    {
        public void Configure(EntityTypeBuilder<CustomEmployee> builder)
        {
            builder.ToTable("CustomEmployees");

            builder.HasKey(t => new { t.ID, t.DefinedEmpID });

            builder.Property(t => t.ID)
                .UseIdentityColumn(1,1)
               .IsRequired();

            builder.Property(t => t.EmpID)
                .IsRequired();

            builder.Property(t => t.DefinedEmpID)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(t => t.DateCreated)
                ;

            builder.Property(t => t.CreatedBy)
                .IsRequired();

            builder.Property(t => t.ModifiedBy);


            builder.Property(t => t.DateModified);

            builder.Property(t => t.DeletedBy);


            builder.Property(t => t.DeletedDate);

            builder.Property(t => t.ModifiedBy);


            builder.Property(t => t.IsDeleted).HasDefaultValue(false);

            //builder.HasOne(t => t.Employee)
            //    .WithMany()
            //    .HasForeignKey(t => t.EmpID)
            //    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
