using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class NhanVienListModel
    {
        public NhanVienListModel()
        {
            KieuNhanViens = new List<SelectListItem>();
            TrangThais = new List<SelectListItem>();
        }
        [NopResourceDisplayName("ChonVe.NhaXe.NhanVien.TenNhanVien")] 
        public string TenNhanVien { get; set; }
        public int KieuNhanVienId { get; set; }
        public int TrangThaiId { get; set; }
        public IList<SelectListItem> KieuNhanViens { get; set; }
        public IList<SelectListItem> TrangThais { get; set; }
    }
}