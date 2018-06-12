using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Core.Domain.NhaXes
{
    public class GiaoDichKeVeLoaiVe : BaseEntity
    {
        public int GiaoDichKeVeId { get; set; }
        public virtual GiaoDichKeVe giaodichkeve { get; set; }
        public int LoaiVeId { get; set; }
        public ENLoaiVeXeItem LoaiVe
        {
            get
            {
                return (ENLoaiVeXeItem)LoaiVeId;
            }
            set
            {
                LoaiVeId = (int)value;
            }
        }
    }
}
