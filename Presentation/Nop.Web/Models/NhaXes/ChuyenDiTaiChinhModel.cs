using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class ChuyenDiTaiChinhModel : BaseNopEntityModel
    {
        public ChuyenDiTaiChinhModel()
        {
            isCP1 = true;
            isCPText = "CP1";
            GiaoDichThuChis = new List<ChuyenDiTaiChinhThuChiModel>();
            NTVMucLuongId = -1;
            LaiXeMucLuongId = -1;
        }
        public int XeVanChuyenId { get; set; }
        public string BienSoXe { get; set; }
        public int NguoiTaoId { get; set; }
        public string TenNguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public bool isCP1 { get; set; }
        public string isCPText { get; set; }
        public string ListThuChi { get; set; }
        public int LuotDiId { get; set; }
        public int LuotVeId { get; set; }
        public decimal ThucThu { get; set; }
        public decimal VeQuay { get; set; }
        public decimal DTLuotDi { get; set; }
        public decimal DTLuotVe { get; set; }
        public decimal DinhMucDau { get; set; }
        public decimal ThucDo { get; set; }
        public decimal DoanhThuHang { get; set; }
        public decimal GiaDau { get; set; }
        public int LaiXeTinhLuongId { get;set; }
        public int LaiXeMucLuongId { get; set; }
        public int NTVTinhLuongId { get; set; }
        public int NTVMucLuongId { get; set; }
        public List<ChuyenDiTaiChinhThuChiModel> GiaoDichThuChis { get; set; }
        public class ChuyenDiTaiChinhThuChiModel:BaseNopEntityModel
        {
           
           
            public int ChuyenDiTaiChinhId { get; set; }
            public int STT { get; set; }
            public int LoaiThuChiId { get; set; }
            public string LoaiThuChiText { get; set; }
            public ENLoaiTaiChinhThuChi loaithuchi
            {
                get
                {
                    return (ENLoaiTaiChinhThuChi)LoaiThuChiId;
                }
                set
                {
                    LoaiThuChiId = (int)value;
                }
            }
            public decimal SoTien { get; set; }
            public string GhiChu { get; set; }
            public decimal Luong2Quay { get; set; }
        }
    }
}