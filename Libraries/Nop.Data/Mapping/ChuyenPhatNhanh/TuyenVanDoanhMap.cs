using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class TuyenVanDoanhMap : NopEntityTypeConfiguration<TuyenVanDoanh>
    {
        public TuyenVanDoanhMap()
        {
            this.ToTable("CV_TuyenVanDoanh");
            this.HasKey(c => c.Id);
            
        }
    }
}
