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
    public class VirtualProductCartEntityConfiguration : IEntityTypeConfiguration<VirtualProductCart>
    {
        public void Configure(EntityTypeBuilder<VirtualProductCart> builder)
        {
            builder.ToTable("VirtualProductCart");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.DiscountAmount).IsRequired(false).HasPrecision(18, 2);
        }
    }
}
