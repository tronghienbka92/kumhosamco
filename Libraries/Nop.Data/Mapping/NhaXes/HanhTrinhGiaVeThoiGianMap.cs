using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.NhaXes
{
    public class HanhTrinhGiaVeThoiGianMap : NopEntityTypeConfiguration<HanhTrinhGiaVeThoiGian>
    {
        public HanhTrinhGiaVeThoiGianMap()
        {
            this.ToTable("CV_HanhtrinhGiaveThoiGian");
            this.HasKey(u => u.Id);
            this.Property(u => u.GiaVe).HasPrecision(18, 0);
            this.HasRequired(c => c.HanhTrinh)
            .WithMany()
            .HasForeignKey(c => c.HanhTrinhId);

            this.HasRequired(c => c.DiemDi)
           .WithMany()
           .HasForeignKey(c => c.DiemDiId);

            this.HasRequired(c => c.DiemDen)
           .WithMany()
           .HasForeignKey(c => c.DiemDenId);
        }
    }
}
