using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class BangDieuChuyenModel
    {
        public BangDieuChuyenModel()
        {
            LoaiXes = new List<SelectListItem>();
        }
        public DateTime NgayDi { get; set; }
        public IList<SelectListItem> LoaiXes { get; set; }
        public int LoaiXeId2 { get; set; }
        public List<BanDieuChuyenHanhTrinh> arrBangDieuChuyen { get; set; }

        public class BanDieuChuyenHanhTrinh
        {
            public BanDieuChuyenHanhTrinh()
            {
                LichTrinhItems = new List<BangDieuChuyenItem>();
            }
            public HanhTrinh hanhtrinhinfo { get; set; }
            public List<BangDieuChuyenItem> LichTrinhItems { get; set; }
        }
        public class BangDieuChuyenItem
        {
            public BangDieuChuyenItem(int _id, int _hantrinhid,string _tenhanhtrinh,int _lichtrinhid,string _tenlichtrinh)
            {
                Id = _id;
                HanhTrinhId = _hantrinhid;
                TenHanhTrinh = _tenhanhtrinh;
                LichTrinhId = _lichtrinhid;
                TenLichTrinh = _tenlichtrinh;
            }
            public int Id { get; set; }
            public int HanhTrinhId { get; set; }
            public string TenHanhTrinh { get; set;}
            public int LichTrinhId { get; set; }
            public string TenLichTrinh { get; set; }
            public List<XeXuatBenItemModel> chuyendis { get; set; }
        }
        public string ThongTinXeDaDieu { get; set; }
        public string ThongTinXeChuaDieu { get; set; }
    }
}