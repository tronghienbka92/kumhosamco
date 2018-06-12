using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Models.NhaXes;
namespace Nop.Web.Models.QLPhoiVe
{
    public class QLHanhTrinhGiaVeModel
    {
        public QLHanhTrinhGiaVeModel()
        {
            ListHanhTrinh = new List<HanhTrinhGiaVeModel>();
        }

        public List<HanhTrinhGiaVeModel> ListHanhTrinh { get; set; }
      
    }
    public class HanhTrinhGiaVeModel
    {
        public int HanhTrinhId { get; set; }
        public string TenHanhTrinh { get; set; }
        public int LoaiXeId { get; set; }
        public string TenLoaiXe { get; set; }
        public decimal GiaVe { get; set; }
        public int LoaiTienId { get; set; }
        public bool isTienDo { get; set; }
        public IList<SelectListItem> ListLoaiTien { get; set; }

    }
}