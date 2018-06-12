using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class ThongKeItem 
    {
        public string Nhan { get; set; }
        public string NhanSapXep { get; set; }
        public int TrangThaiPhoiVeId { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiPhoiVeId;
            }
            set
            {
                TrangThaiPhoiVeId = (int)value;
            }
        }
        public Decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongDat { get; set; }
        public int SoLuongChuyen { get; set; }
        public int SoLuongHuy { get; set; }
        public Decimal GiaTri1 { get; set; }
        public Decimal GiaTri2 { get; set; }
        public int ItemId { get; set; }
        public DateTime ItemDataDate { get; set; }
        public int ItemDataYear { get; set; }
        public int ItemDataMonth { get; set; }
        public int ItemDataDay { get; set; }
        public string LoaiVe { get; set; }
        public string Seri { get; set; }
    }
    
    public class DoanhThuItem
    {
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public Decimal DoanhThu { get; set; }
       
    }
    public class DoanhThuTheoXeItem
    {
        public int NguonVeXeId { get; set; }
        public string ThongTinChuyenDi { get; set; }
        public int XeId { get; set; }
        public string Nhan { get; set; }
        public string NhanSapXep { get; set; }
        public Decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
        public string KyHieuGhe { get; set; }
        public int TrangThaiPhoiVeId { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiPhoiVeId;
            }
            set
            {
                TrangThaiPhoiVeId = (int)value;
            }
        }
        public Decimal GiaTri1 { get; set; }
        public Decimal GiaTri2 { get; set; }
        public Nop.Core.Domain.Chonves.NguonVeXe NguonVeXe { get; set; }
        public int ItemId { get; set; }
        public DateTime ItemDataDate { get; set; }
        public int ItemDataYear { get; set; }
        public int ItemDataMonth { get; set; }
        public int ItemDataDay { get; set; }
        public decimal GiaVe { get; set; }
    }
    public class KhachHangMuaVeItem
    {
        public int CustomerId { get; set; }
        public Customers.Customer customer { get; set; }
        public Nop.Core.Domain.Chonves.NguonVeXe nguonve { get; set; }
        public int NguonVeXeId { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string ThongTinChuyenDi { get; set; }
        public int TrangThaiPhoiVeId { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiPhoiVeId;
            }
            set
            {
                TrangThaiPhoiVeId = (int)value;
            }
        }
        public string KyHieuGhe { get; set; }
        public bool isChonVe { get; set; }
        public decimal GiaVe { get; set; }
        
        public DateTime NgayDi { get; set; }
        public int SoLuot { get; set; }
    }
    public class BaoCaoLaiXePhuXe:BaseEntity
    {
        public string HoVaTen { get; set; }
        public string CMT_Id { get; set; }
        public string DienThoai { get; set; }
        public string KieuNhanVienText { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int TongChuyenDi { get; set; }
        public decimal TongLuong { get; set; }
    }
    public class HanhTrinhDateItem
    {


        public int Nhan { get; set; }
        public string NgayDi { get; set; }
        public int SoChuyen { get; set; }
        public int SoKhach { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal DoanhThuVeDiTruoc { get; set; }
        public decimal DoanhThuVeDaiLy { get; set; }
        public decimal DoanhThuVeDiNgay { get; set; }
        public decimal TongDoanhThu
        {
            get { return DoanhThuVeDiTruoc + DoanhThuVeDaiLy + DoanhThuVeDiNgay; }
        }
        public int HanhTrinhId { get; set; }
        public string TenHanhTrinh { get; set; }
    }
    public class DateItem 
    {
        public string NgayDi { get; set; }
        public int nhan { get; set; }

        public int day { get; set; }

        public int month { get; set; }
        public int year { get; set; }
    }
    public class ThongKeLuotXuatBenItem
    {
        public int[] NhanVienIds { get; set; }
        public int SoLuot { get; set; }
        public decimal DoanhThu { get; set; }
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }


    }
    public class ThongKeDoanhThuTuyenItem
    {
        public int[] NhanVienIds { get; set; }
        public int HanhTrinhId { get; set; }
        public int SoChuyen { get; set; }
        public int SoKhach { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal DoanhThuVeBanTruoc { get; set; }
        public decimal DoanhThuVeDaiLy { get; set; }
        public decimal DoanhThuVeDiNgay { get; set; }
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }


    }
    public class VeItemHanhTrinh
    {
        public int STT { get; set; }
        public DateTime NgayBan { get; set; }
        public int SoQuyen { get; set; }
        public int SeriDau { get; set; }
        public int SeriCuoi { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string Tuyen { get; set; }


    }

    public class VeHuyItem
    {
        public int STT { get; set; }
        public DateTime NgayDi { get; set; }
       
        public int SeriVe { get; set; }
        public string Tuyen { get; set; }
        public string TenKhachHang { get; set; }
        public string DienThoai { get; set; }
        public string LyDoHuy { get; set; }
        public decimal GiaVe { get; set; }
        


    }
    public class DoanhThuTheoGio
    {
        public int STT { get; set; }
        public DateTime NgayDi { get; set; }

        public string BienSo { get; set; }
        public string TenLaiXe { get; set; }
        public string TenPhuXe { get; set; }      
        public string TenHanhTrinh { get; set; }
        public int TongKhach { get; set; }
        public decimal TongDT { get; set; }

    }
    public class DoanhThuHangNgay
    {
      
        public DateTime NgayDi { get; set; }

        public decimal DTVeBanNgay { get; set; }
        public decimal DTVeBanTruoc { get; set; }
        public decimal DTVeHuy { get; set; }
        public decimal DTHangHoa { get; set; }
        public int SoChuyen { get; set; }
       

    }
    public class LichSuBanVe
    {
        public string TenNhanVien { get; set; }
        public int SoLuongDat { get; set; }
        public int SoLuongHuy { get; set; }
        public int SoLuongChuyen { get; set; }
    }
}
