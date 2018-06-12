

namespace Nop.Core.Domain.Chonves
{
    public enum ENTrangThaiHopDong
    {
        TatCa=0,
        Moi = 1,
        DangChoDuyet = 2,
        DaDuyet = 10,
        KetThuc = 11,
        Huy = 20

    }
    public enum ENLoaiHopDong
    {
        ChietKhauTheoGiaVe = 1,
        ChietKhauTheoVe = 2
    }
    public enum ENTrangThaiNguonVe
    {
        BiKhoa = 0,
        TrongNhaXe =1,
        WebVaNhaXe=2
    }
    public enum ENLoaiDiaDiem
    {
        Tinh = 1,
        QuanHuyen = 2,
        BenXe = 3        
    }
    public enum ENGiaHanHopDong
    {
         BaThang=3,
         SauThang=6,
         MotNam=12,
         HaiNam=24,
         NamNam=60
    }
    public enum ENTrangThaiPhoiVe
    {
        KhongTonTai=0,
        ConTrong=1,
        DatCho=2, //dat cho cho xu ly thanh toan cua khach hang
        GiuCho=3, //dat hang nhung chua thanh toan        
        ChoXuLy = 4, //dat hang co don dat
        DaThanhToan=10, //da thanh toan, chua giao hang
        DaGiaoHang=20, //da giao hang, da thanh toan
        KetThuc=30,
        Huy=40
    }
    
    public enum ENPhuongThucVanChuyen
    {
        PhuongTienCaNhan = 1,
        BuuDien = 2,
        ChuyenPhatNhanh = 3,
       
    }
    public enum ENPhuongThucThanhToan
    {
       
        ATM = 1,
        NganHang = 2,
        TrucTiep = 3,
        
    } 
    public enum ENKhungGio
    {
        All=0,
        Sang=1,
        Chieu=2,
        Toi=3
        
    }
    public enum ENHanhTrinh
    {
        MD_PT=57,
        PT_MT=58,
        HCM_BMT=59,
        BTM_HCM=60,
        AS_BMT=71,
        BMT_AS=72,
        MT_HT=74,
        HT_MT=75,
        PH_HCM=76,
        HCM_PH=77,
        KL_MT=78
    }
    public enum ENLoaiXe
    {
        GN_40_CHO=80,
        GN_37_CHO=82
    }
}
