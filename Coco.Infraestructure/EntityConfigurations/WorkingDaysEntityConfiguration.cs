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
    public class WorkingDaysEntityConfiguration : IEntityTypeConfiguration<WorkingDays>
    {
        public void Configure(EntityTypeBuilder<WorkingDays> builder)
        {
            builder.ToTable("WorkingDays");
            builder.HasKey(x => x.Id);
        }
    }
}
