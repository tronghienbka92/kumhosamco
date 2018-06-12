using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class HanhTrinhGiaVeThoiGian : BaseEntity
    {
        public int HanhTrinhId { get; set; }
        public int DiemDiId { get; set; }
        public int DiemDenId { get; set; }
        public int NhaXeId { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public decimal GiaVe { get; set; }
        public virtual HanhTrinh HanhTrinh { get; set; }
        public virtual DiemDon DiemDi { get; set; }
        public virtual DiemDon DiemDen { get; set; }
        public bool isDola { get; set; }
    }
}
