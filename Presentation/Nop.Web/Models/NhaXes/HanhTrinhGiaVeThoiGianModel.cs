using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Nop.Web.Models.NhaXes
{
    public class HanhTrinhGiaVeThoiGianModel : BaseNopEntityModel
    {
        public HanhTrinhGiaVeThoiGianModel()
        {
            DiemDis = new List<SelectListItem>();
            DiemDens = new List<SelectListItem>();
        }
        public int HanhTrinhId { get; set; }
        [UIHint("Date")]
        public DateTime TuNgay { get; set; }
        [UIHint("Date")]
        public DateTime DenNgay { get; set; }
        public decimal GiaVe { get; set; }
        public int DiemDiId { get; set; }
        public IList<SelectListItem> DiemDis { get; set; }
        public int DiemDenId { get; set; }
        public IList<SelectListItem> DiemDens { get; set; }
        public string TenDiemDi { get; set; }
        public string TenDiemDen { get; set; }
        public bool isDoLa { get; set; }
    }
}