using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class DaiLyMap : NopEntityTypeConfiguration<DaiLy>
    {
        public DaiLyMap()
        {
            this.ToTable("CV_DaiLy");
            this.HasKey(c => c.Id);
   
        }
    }
}
