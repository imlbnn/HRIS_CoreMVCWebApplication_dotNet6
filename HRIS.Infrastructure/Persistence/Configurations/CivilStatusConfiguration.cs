using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Persistence.Configurations
{
    public class CivilStatusConfiguration : IEntityTypeConfiguration<CivilStatus>
    { 
        public void Configure(EntityTypeBuilder<CivilStatus> builder)
        {
            builder.ToTable("CivilStatus");

            builder.HasKey(t => t.Code);

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

        }
    }
}
