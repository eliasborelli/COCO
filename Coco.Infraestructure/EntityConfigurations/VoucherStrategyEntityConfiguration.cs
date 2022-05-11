﻿using Coco.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Infraestructure.EntityConfigurations
{
    public class VoucherStrategyEntityConfiguration : IEntityTypeConfiguration<VoucherStrategy>
    {
        public void Configure(EntityTypeBuilder<VoucherStrategy> builder)
        {
            builder.ToTable("VoucherStrategy");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
