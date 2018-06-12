using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class TuyenVanDoanh : BaseEntity
    {
        public string TenTuyen { get; set; }
        public string TenVietTat { get; set; }
        public int NhaXeId { get; set; }
      
    }
}
