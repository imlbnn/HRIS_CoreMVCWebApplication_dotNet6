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
    public class DepartmentSectionConfiguration : IEntityTypeConfiguration<DepartmentalSection>
    {
        public void Configure(EntityTypeBuilder<DepartmentalSection> builder)
        {
            builder.ToTable("DepartmentalSections");

            builder.HasKey(t => new { t.DepartmentCode, t.Code });

            builder.Property(t => t.DepartmentCode).HasMaxLength(1);

            builder.Property(t => t.Code).HasMaxLength(2);

            builder.Property(t => t.Description);

            builder.Property(t => t.DateCreated).IsRequired();

            builder.Property(t => t.CreatedBy)
                .IsRequired();

            builder.Property(t => t.ModifiedBy);

            builder.Property(t => t.DateModified);

            builder.Property(t => t.DeletedBy);


            builder.Property(t => t.DeletedDate);

            builder.Property(t => t.ModifiedBy);


            builder.Property(t => t.IsDeleted).HasDefaultValue(false);

            builder.HasOne(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentCode)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
