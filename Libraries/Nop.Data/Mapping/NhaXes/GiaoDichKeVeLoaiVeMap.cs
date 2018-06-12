using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Data.Mapping.NhaXes
{
    public class GiaoDichKeVeLoaiVeMap : NopEntityTypeConfiguration<GiaoDichKeVeLoaiVe>
    {
        public GiaoDichKeVeLoaiVeMap()
        {
            this.ToTable("CV_GiaoDichKeVeLoaiVe");
            this.HasKey(c => c.Id);
            this.HasRequired(c => c.giaodichkeve)
             .WithMany(u=>u.keveloaives)
             .HasForeignKey(c => c.GiaoDichKeVeId);
            this.Ignore(c => c.LoaiVe);
        } 
    }
}
