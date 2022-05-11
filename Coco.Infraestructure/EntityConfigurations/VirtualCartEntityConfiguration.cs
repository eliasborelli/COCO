using Coco.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Infraestructure.EntityConfigurations
{
    public class VirtualCartEntityConfiguration : IEntityTypeConfiguration<VirtualCart>
    {
        public void Configure(EntityTypeBuilder<VirtualCart> builder)
        {
            builder.ToTable("VirtualCart");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.TotalAmount).IsRequired(false).HasPrecision(18, 2);
            builder.Property(x => x.DiscountAmount).IsRequired(false).HasPrecision(18, 2);
        }
    }
}
