using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Services.Authentication;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.UI.Captcha;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;
using WebGrease.Css.Extensions;
using Nop.Web.Models.NhaXes;
using Nop.Core.Data;
using Nop.Services.NhaXes;
using Nop.Core.Caching;
using Nop.Core.Domain.News;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Services.Chonves;
using Nop.Services.Security;
using Nop.Core.Domain.Security;
using System.Globalization;
using Nop.Services.Catalog;
using Nop.Web.Models.VeXeKhach;
using Nop.Core.Domain.Chonves;
using Nop.Web.Models.QLPhoiVe;


namespace Nop.Web.Controllers
{
    public class QLPhoiVeController : BaseNhaXeController
    {
        public const int THOI_GIAN_GHE_DAT_CHO = 240;
        #region Khoi Tao
        private readonly IStateProvinceService _stateProvinceService;
        private readonly INhaXeService _nhaxeService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IPhieuGuiHangService _phieuguihangService;
        private readonly IHangHoaService _hanghoaService;
        private readonly ICustomerService _customerService;
        private readonly IChonVeService _chonveService;
        private readonly IDiaChiService _diachiService;
        private readonly INhanVienService _nhanvienService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreService _storeService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IXeInfoService _xeinfoService;
        private readonly IHanhTrinhService _hanhtrinhService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IBenXeService _benxeService;
        private readonly IVeXeService _vexeService;
        private readonly IPhoiVeService _phoiveService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAuthenticationService _authenticationService;
        private readonly INhaXeCustomerService _nhaxecustomerService;
        private readonly IGiaoDichKeVeXeService _giaodichkeveService;

        public QLPhoiVeController(
            IStateProvinceService stateProvinceService,
            INhaXeService nhaxeService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IPictureService pictureService,
            IPhieuGuiHangService phieuguihangService,
            IHangHoaService hanghoaService,
            ICustomerService customerService,
            IChonVeService chonveService,
            IDiaChiService diachiService,
            INhanVienService nhanvienService,
            IPermissionService permissionService,
            IDateTimeHelper dateTimeHelper,
            CustomerSettings customerSettings,
            DateTimeSettings dateTimeSettings,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerActivityService customerActivityService,
            IGenericAttributeService genericAttributeService,
            IStoreService storeService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IXeInfoService xeinfoService,
            IHanhTrinhService hanhtrinhService,
            IPriceFormatter priceFormatter,
            IBenXeService benxeService,
            IVeXeService vexeService,
            IPhoiVeService phoiveService,
            IShoppingCartService shoppingCartService,
            IAuthenticationService authenticationService,
            INhaXeCustomerService nhaxecustomerService,
            IGiaoDichKeVeXeService giaodichkeveService
            )
        {
            this._stateProvinceService = stateProvinceService;
            this._nhaxeService = nhaxeService;
            this._hanghoaService = hanghoaService;
            this._phieuguihangService = phieuguihangService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._customerService = customerService;
            this._chonveService = chonveService;
            this._diachiService = diachiService;
            this._nhanvienService = nhanvienService;
            this._permissionService = permissionService;
            this._dateTimeHelper = dateTimeHelper;
            this._customerSettings = customerSettings;
            this._dateTimeSettings = dateTimeSettings;
            this._customerRegistrationService = customerRegistrationService;
            this._customerActivityService = customerActivityService;
            this._genericAttributeService = genericAttributeService;
            this._storeService = storeService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._xeinfoService = xeinfoService;
            this._hanhtrinhService = hanhtrinhService;
            this._priceFormatter = priceFormatter;
            this._benxeService = benxeService;
            this._vexeService = vexeService;
            this._phoiveService = phoiveService;
            this._shoppingCartService = shoppingCartService;
            this._authenticationService = authenticationService;
            this._nhaxecustomerService = nhaxecustomerService;
            this._giaodichkeveService = giaodichkeveService;

        }
        #endregion
        #region Cac ham chung
        List<SelectListItem> PrepareNguonVeXeList(List<QuanlyPhoiVeModel.NguonVeXeItem> lstnguonve, int NguonVeXeId)
        {
            var nguonvexes = lstnguonve.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.MoTa;
                item.Selected = (c.Id == NguonVeXeId);
                return item;
            }).ToList();
            return nguonvexes;
        }
        List<QuanlyPhoiVeModel.NguonVeXeItem> GetNguonVeXeByHanhTrinhId(int HanhTrinhID)
        {
            var items = _hanhtrinhService.GetAllNguonVeXe(_workContext.NhaXeId, 0, HanhTrinhID).Where(c => c.HienThi && c.ParentId == 0);
            var nguonves = items
                .Select(c =>
                {

                    var item = new QuanlyPhoiVeModel.NguonVeXeItem();
                    item.Id = c.Id;
                    item.ThoiGianDen = c.ThoiGianDen;
                    item.ThoiGianDi = c.ThoiGianDi;
                    item.MoTa = string.Format("{0}-{1} - ({2}:{3})", c.ThoiGianDi.ToString("HH:mm"), c.ThoiGianDen.ToString("HH:mm"), c.LichTrinhInfo.MaLichTrinh, c.TenLoaiXe);
                    return item;
                })
                .ToList();
            return nguonves;
        }
        List<SelectListItem> PrepareHanhTrinhList(bool isAll = true, bool isChonHanhTrinh = false)
        {
            List<HanhTrinh> hanhtrinhs = new List<HanhTrinh>();
            //if (isAll)
              //  hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            //else
             //   hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, _workContext.CurrentVanPhong.Id);
            if (_workContext.CurrentVanPhong.KieuVanPhong == ENKieuVanPhong.TruSo)
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            else
                hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId, _workContext.CurrentNhanVien.VanPhongs.Select(c => c.Id).ToArray());
            var ddls = hanhtrinhs.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.MoTa;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();

            if (isChonHanhTrinh)
                ddls.Insert(0, new SelectListItem { Text = "--------------", Value = "0" });
            return ddls;
        }
        [NonAction]
        protected virtual void SoDoGheXeToSoDoGheXeModel(SoDoGheXe nvfrom, LoaiXeModel.SoDoGheXeModel nvto)
        {
            nvto.Id = nvfrom.Id;
            nvto.TenSoDo = nvfrom.TenSoDo;
            nvto.UrlImage = nvfrom.TenSoDo;
            nvto.SoLuongGhe = nvfrom.SoLuongGhe;
            nvto.KieuXeId = nvfrom.KieuXeId;
            nvto.SoCot = nvfrom.SoCot;
            nvto.SoHang = nvfrom.SoHang;


        }
        [NonAction]
        protected virtual string GetTenChang(PhoiVe phoive, ENPhanLoaiPhoiVe LoaiPhoiVe)
        {
            var _nguonve = phoive.nguonvexe;
            if (phoive.NguonVeXeConId > 0)
                _nguonve = phoive.nguonvexecon;
            //var chang = _hanhtrinhService.GetHanhTrinhGiaVeId(phoive.ChangId.GetValueOrDefault(0));
            var chang = _hanhtrinhService.GetHanhTrinhGiaVeThoiGianById(phoive.ChangId.GetValueOrDefault(0));
            if (chang != null)
            {
                var TenTinhDon = chang.DiemDi.TenDiemDon;
                var TenTinhDen = chang.DiemDen.TenDiemDon;
                //var TenTinhDon = _hanhtrinhService.GetStateProvinceByNguon(_nguonve.DiemDon.NguonId).Abbreviation;
                //var TenTinhDen = _hanhtrinhService.GetStateProvinceByNguon(_nguonve.DiemDen.NguonId).Abbreviation;
                //if (LoaiPhoiVe == ENPhanLoaiPhoiVe.IN_PHOI_VE)
                //{
                //    TenTinhDon = chang.DiemDon.TenDiemDon;
                //    TenTinhDen = chang.DiemDen.TenDiemDon;
                //}
                return string.Format("{0}-{1}", TenTinhDon, TenTinhDen);
            }
            else
            {
                return "";
            }

        }
        [NonAction]
        protected virtual string GetKhachDonDuong(PhoiVe phoive)
        {

            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                return "DD";
            else
                return "";
        }
        ActionResult TrangThaiKhongHopLe()
        {
            return Json("Trạng thái không hợp lệ", JsonRequestBehavior.AllowGet);
        }
        ActionResult KhongSoHuu()
        {
            return Json("Vé không sở hữu", JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Thong tin ghe"
        PhoiVe GetPhoiVeTrongChuyen(List<PhoiVe> phoives, SoDoGheXeQuyTac vitri, HistoryXeXuatBen chuyendi)
        {
            var pv = phoives.Where(c => c.SoDoGheXeQuyTacId == vitri.Id).FirstOrDefault();
            if (pv == null)
            {
                pv = new PhoiVe();
                pv.NgayDi = chuyendi.NgayDi;
                pv.NguonVeXeId = chuyendi.NguonVeId;
                pv.nguonvexe = chuyendi.NguonVeInfo;
                pv.TrangThai = ENTrangThaiPhoiVe.ConTrong;
                pv.sodoghexequytac = vitri;
                pv.SoDoGheXeQuyTacId = vitri.Id;
                return pv;
            }
            return pv;
        }
        /// <summary>
        /// Lay thong tin so do ghe xe
        /// </summary>
        /// <param name="NguonVeXeId"></param>
        /// <param name="NgayDi"></param>
        /// <param name="PhanLoai">0: phoi ve, 1:dung cho chuyen ve, 2: in phoive</param>
        /// <param name="TangIndex"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult _TabSoDoXe(int ChuyenDiId, int? TangIndex, int? PhanLoai)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            //lay thong tin chuyen di
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            Session["ChuyenDiId"] = ChuyenDiId;
            //lấy thong tin nguồn xe            
            if (chuyendi == null)
                return AccessDeniedView();

            var loaixe = _xeinfoService.GetById(chuyendi.NguonVeInfo.LoaiXeId);
            if (loaixe == null)
                return AccessDeniedView();

            //var nhaxe = this._workContext.CurrentNhaXe;
            var sodoghe = _xeinfoService.GetSoDoGheXeById(loaixe.SoDoGheXeID);
            var modelsodoghe = new LoaiXeModel.SoDoGheXeModel();
            if (_workContext.CurrentVanPhong.IsYeuCauDuyetHuy)
            {
                modelsodoghe.CanYeuCauHuy = true;
            }
            modelsodoghe.chuyendihientai = chuyendi.toModel(_localizationService);
            modelsodoghe.PhanLoai = (ENPhanLoaiPhoiVe)PhanLoai.GetValueOrDefault(0);
            SoDoGheXeToSoDoGheXeModel(sodoghe, modelsodoghe);
            // lay cac diem don , tao ma tran thong ke ket qua
            var diemdons = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(chuyendi.HanhTrinhId).Where(c => c.KhoangCach > 0).OrderBy(c => c.ThuTu).ToList();//.Select(c => c.Id).ToArray();
            var diemdonids = diemdons.Select(c => c.DiemDonId).ToArray();
            modelsodoghe.SoDiemDon = diemdons.Count();
            modelsodoghe.TongKet = new LoaiXeModel.TongKetPhoiToArrayModel[modelsodoghe.SoDiemDon + 1, modelsodoghe.SoCot + 2];
            for (int m = 0; m < modelsodoghe.SoDiemDon + 1; m++)
            {
                for (int n = 0; n < modelsodoghe.SoCot + 2; n++)
                {
                    modelsodoghe.TongKet[m, n] = new LoaiXeModel.TongKetPhoiToArrayModel();
                    if (m < modelsodoghe.SoDiemDon && n < modelsodoghe.SoCot + 1)
                    {
                        modelsodoghe.TongKet[m, n].DiemDonId = diemdons[m].DiemDonId;
                        modelsodoghe.TongKet[m, n].TenDiemDon = diemdons[m].diemdon.TenDiemDon;
                        modelsodoghe.TongKet[m, n].SoKhachXuong = 0;
                    }



                }
            }
            //Lấy thông tin ma tran
            var sodoghevitris = _xeinfoService.GetAllSoDoGheViTri(sodoghe.Id);
            var sodoghequytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(loaixe.Id);

            modelsodoghe.MaTran = new int[modelsodoghe.SoHang, modelsodoghe.SoCot];
            modelsodoghe.PhoiVes1 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            modelsodoghe.SoTang = 1;
            if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
            {
                modelsodoghe.SoTang = 2;
                modelsodoghe.PhoiVes2 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            }
            foreach (var s in sodoghevitris)
            {
                modelsodoghe.MaTran[s.y, s.x] = 1;
            }
            //lay thong tin phoi ve
            var phoives = _phoiveService.GetPhoiVeByChuyenDi(chuyendi.Id);
            foreach (var pv in phoives)
            {
                if (pv.TrangThai == ENTrangThaiPhoiVe.DatCho && pv.NgayTao.AddSeconds(THOI_GIAN_GHE_DAT_CHO) < DateTime.Now)
                {
                    _phoiveService.DeletePhoiVe(pv);
                }
            }
            if (sodoghequytacs != null && sodoghequytacs.Count > 0)
            {
                foreach (var s in sodoghequytacs)
                {
                    if (s.Tang == 1)
                    {                        
                        modelsodoghe.PhoiVes1[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes1[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {

                            modelsodoghe.PhoiVes1[s.y, s.x].Info = GetPhoiVeTrongChuyen(phoives, s, chuyendi);
                            if (modelsodoghe.PhoiVes1[s.y, s.x].Info.daily != null)
                                modelsodoghe.PhoiVes1[s.y, s.x].TenDaiLy = modelsodoghe.PhoiVes1[s.y, s.x].Info.daily.TenDaiLy;

                            if (modelsodoghe.PhoiVes1[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes1[s.y, s.x];
                                ViTriGhe.TenChang = GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);

                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                if (ViTriGhe.Info.LoaiTien == ENLoaiTien.DOLLA)
                                {
                                    ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToDollar();
                                }
                                int idkhachhang = ViTriGhe.Info.customer.Id;
                                if (idkhachhang == CommonHelper.KhachVangLaiId)
                                {
                                    var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                    ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                    ViTriGhe.SoDienThoai = null;

                                }

                                else
                                {
                                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(idkhachhang);
                                    if (khachhang != null)
                                    {
                                        ViTriGhe.TenKhachHang = khachhang.HoTen;
                                        ViTriGhe.SoDienThoai = khachhang.DienThoai;

                                    }
                                }
                                if (ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang || ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                                {

                                    var _nguonve = ViTriGhe.Info.nguonvexe;
                                    if (ViTriGhe.Info.NguonVeXeConId > 0)
                                        _nguonve = ViTriGhe.Info.nguonvexecon;

                                    /*int indexdiemdon = Array.IndexOf(diemdonids, ViTriGhe.Info.changgiave.DiemDenId);
                                    if (indexdiemdon >= 0)
                                    {
                                        modelsodoghe.TongKet[indexdiemdon, s.x].SoKhachXuong++;
                                        modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue = ViTriGhe.Info.GiaVeHienTai;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[indexdiemdon, modelsodoghe.SoCot + 1].TongKhach++;
                                        //tong tien toan bo
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongKhach++;
                                    }*/

                                }


                            }
                        }

                    }
                    else
                    {
                        modelsodoghe.PhoiVes2[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes2[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {
                            modelsodoghe.PhoiVes2[s.y, s.x].Info = GetPhoiVeTrongChuyen(phoives, s, chuyendi); ;
                            if (modelsodoghe.PhoiVes2[s.y, s.x].Info.daily != null)
                                modelsodoghe.PhoiVes2[s.y, s.x].TenDaiLy = modelsodoghe.PhoiVes2[s.y, s.x].Info.daily.TenDaiLy;
                            if (modelsodoghe.PhoiVes2[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes2[s.y, s.x];
                                ViTriGhe.TenChang = GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);
                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                if (ViTriGhe.Info.LoaiTien == ENLoaiTien.DOLLA)
                                {
                                    ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToDollar();
                                }
                                int idkhachhang = ViTriGhe.Info.customer.Id;
                                if (idkhachhang == CommonHelper.KhachVangLaiId)
                                {
                                    var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                    ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                    ViTriGhe.SoDienThoai = null;

                                }

                                else
                                {
                                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(idkhachhang);
                                    if (khachhang != null)
                                    {
                                        ViTriGhe.TenKhachHang = khachhang.HoTen;
                                        ViTriGhe.SoDienThoai = khachhang.DienThoai;

                                    }
                                }
                                if (ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang || ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                                {



                                    /*int indexdiemdon = Array.IndexOf(diemdonids, ViTriGhe.Info.ChangId.Value);
                                    if (indexdiemdon >= 0)
                                    {
                                        modelsodoghe.TongKet[indexdiemdon, s.x].SoKhachXuong++;
                                        modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue = ViTriGhe.Info.GiaVeHienTai;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[indexdiemdon, modelsodoghe.SoCot + 1].TongKhach++;
                                        //tong tien toan bo
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongKhach++;
                                    }*/

                                }

                            }

                        }
                    }
                }
            }
            //selected tab
            SaveSelectedTabIndex(TangIndex);
            modelsodoghe.isCheckChuyenVe = CheckChuyenVeCopy();
            return PartialView(modelsodoghe);
        }
        public LoaiXeModel.SoDoGheXeModel GetSoDoGhe(int ChuyenDiId, int? TangIndex,int? PhanLoai)
        {
            //lay thong tin chuyen di
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            Session["ChuyenDiId"] = ChuyenDiId;
            //lấy thong tin nguồn xe            
            if (chuyendi == null)
                return null;

            var loaixe = _xeinfoService.GetById(chuyendi.NguonVeInfo.LoaiXeId);
            if (loaixe == null)
                return null;

            //var nhaxe = this._workContext.CurrentNhaXe;
            var sodoghe = _xeinfoService.GetSoDoGheXeById(loaixe.SoDoGheXeID);
            var modelsodoghe = new LoaiXeModel.SoDoGheXeModel();
            if (_workContext.CurrentVanPhong.IsYeuCauDuyetHuy)
            {
                modelsodoghe.CanYeuCauHuy = true;
            }
            modelsodoghe.chuyendihientai = chuyendi.toModel(_localizationService);
            modelsodoghe.PhanLoai = (ENPhanLoaiPhoiVe)PhanLoai.GetValueOrDefault(0);
            SoDoGheXeToSoDoGheXeModel(sodoghe, modelsodoghe);
            // lay cac diem don , tao ma tran thong ke ket qua
            var diemdons = _hanhtrinhService.GetAllHanhTrinhDiemDonByHanhTrinhId(chuyendi.HanhTrinhId).Where(c => c.KhoangCach > 0).OrderBy(c => c.ThuTu).ToList();//.Select(c => c.Id).ToArray();
            var diemdonids = diemdons.Select(c => c.DiemDonId).ToArray();
            modelsodoghe.SoDiemDon = diemdons.Count();
            modelsodoghe.TongKet = new LoaiXeModel.TongKetPhoiToArrayModel[modelsodoghe.SoDiemDon + 1, modelsodoghe.SoCot + 2];
            for (int m = 0; m < modelsodoghe.SoDiemDon + 1; m++)
            {
                for (int n = 0; n < modelsodoghe.SoCot + 2; n++)
                {
                    modelsodoghe.TongKet[m, n] = new LoaiXeModel.TongKetPhoiToArrayModel();
                    if (m < modelsodoghe.SoDiemDon && n < modelsodoghe.SoCot + 1)
                    {
                        modelsodoghe.TongKet[m, n].DiemDonId = diemdons[m].DiemDonId;
                        modelsodoghe.TongKet[m, n].TenDiemDon = diemdons[m].diemdon.TenDiemDon;
                        modelsodoghe.TongKet[m, n].SoKhachXuong = 0;
                    }



                }
            }
            //Lấy thông tin ma tran
            var sodoghevitris = _xeinfoService.GetAllSoDoGheViTri(sodoghe.Id);
            var sodoghequytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(loaixe.Id);

            modelsodoghe.MaTran = new int[modelsodoghe.SoHang, modelsodoghe.SoCot];
            modelsodoghe.PhoiVes1 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            modelsodoghe.SoTang = 1;
            if (sodoghe.KieuXe == ENKieuXe.GiuongNam)
            {
                modelsodoghe.SoTang = 2;
                modelsodoghe.PhoiVes2 = new LoaiXeModel.PhoiVeAdvanceModel[modelsodoghe.SoHang + 1, modelsodoghe.SoCot + 1];
            }
            foreach (var s in sodoghevitris)
            {
                modelsodoghe.MaTran[s.y, s.x] = 1;
            }
            //lay thong tin phoi ve
            var phoives = _phoiveService.GetPhoiVeByChuyenDi(chuyendi.Id);
            if (sodoghequytacs != null && sodoghequytacs.Count > 0)
            {
                foreach (var s in sodoghequytacs)
                {
                    if (s.Tang == 1)
                    {
                        modelsodoghe.PhoiVes1[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes1[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {

                            modelsodoghe.PhoiVes1[s.y, s.x].Info = GetPhoiVeTrongChuyen(phoives, s, chuyendi);
                            if (modelsodoghe.PhoiVes1[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes1[s.y, s.x];
                                ViTriGhe.TenChang = GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);

                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                if (ViTriGhe.Info.LoaiTien == ENLoaiTien.DOLLA)
                                {
                                    ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToDollar();
                                }
                                int idkhachhang = ViTriGhe.Info.customer.Id;
                                if (idkhachhang == CommonHelper.KhachVangLaiId)
                                {
                                    var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                    ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                    ViTriGhe.SoDienThoai = null;

                                }

                                else
                                {
                                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(idkhachhang);
                                    if (khachhang != null)
                                    {
                                        ViTriGhe.TenKhachHang = khachhang.HoTen;
                                        ViTriGhe.SoDienThoai = khachhang.DienThoai;

                                    }
                                }
                                if (ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang || ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                                {

                                    var _nguonve = ViTriGhe.Info.nguonvexe;
                                    if (ViTriGhe.Info.NguonVeXeConId > 0)
                                        _nguonve = ViTriGhe.Info.nguonvexecon;

                                    /*int indexdiemdon = Array.IndexOf(diemdonids, ViTriGhe.Info.changgiave.DiemDenId);
                                    if (indexdiemdon >= 0)
                                    {
                                        modelsodoghe.TongKet[indexdiemdon, s.x].SoKhachXuong++;
                                        modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue = ViTriGhe.Info.GiaVeHienTai;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[indexdiemdon, modelsodoghe.SoCot + 1].TongKhach++;
                                        //tong tien toan bo
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongKhach++;
                                    }*/

                                }


                            }
                        }

                    }
                    else
                    {
                        modelsodoghe.PhoiVes2[s.y, s.x] = new LoaiXeModel.PhoiVeAdvanceModel();
                        modelsodoghe.PhoiVes2[s.y, s.x].KyHieu = s.Val;
                        if (s.y >= 1 && s.x >= 1)
                        {
                            modelsodoghe.PhoiVes2[s.y, s.x].Info = GetPhoiVeTrongChuyen(phoives, s, chuyendi); ;
                            if (modelsodoghe.PhoiVes2[s.y, s.x].Info.customer != null)
                            {
                                var ViTriGhe = modelsodoghe.PhoiVes2[s.y, s.x];
                                ViTriGhe.TenChang = GetTenChang(ViTriGhe.Info, modelsodoghe.PhanLoai);
                                ViTriGhe.LoaiKhach = GetKhachDonDuong(ViTriGhe.Info);
                                ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToTien(_priceFormatter);
                                if (ViTriGhe.Info.LoaiTien == ENLoaiTien.DOLLA)
                                {
                                    ViTriGhe.GiaVe = ViTriGhe.Info.GiaVeHienTai.ToDollar();
                                }
                                int idkhachhang = ViTriGhe.Info.customer.Id;
                                if (idkhachhang == CommonHelper.KhachVangLaiId)
                                {
                                    var _khachhang = _customerService.GetCustomerById(idkhachhang);
                                    ViTriGhe.TenKhachHang = _khachhang.GetFullName();
                                    ViTriGhe.SoDienThoai = null;

                                }

                                else
                                {
                                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(idkhachhang);
                                    if (khachhang != null)
                                    {
                                        ViTriGhe.TenKhachHang = khachhang.HoTen;
                                        ViTriGhe.SoDienThoai = khachhang.DienThoai;

                                    }
                                }
                                if (ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang || ViTriGhe.Info.TrangThai == ENTrangThaiPhoiVe.DaThanhToan)
                                {



                                    int indexdiemdon = Array.IndexOf(diemdonids, ViTriGhe.Info.ChangId.Value);
                                    if (indexdiemdon >= 0)
                                    {
                                        modelsodoghe.TongKet[indexdiemdon, s.x].SoKhachXuong++;
                                        modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue = ViTriGhe.Info.GiaVeHienTai;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, s.x].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[indexdiemdon, modelsodoghe.SoCot + 1].TongKhach++;
                                        //tong tien toan bo
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue + modelsodoghe.TongKet[indexdiemdon, s.x].TongTienValue;
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienText = modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongTienValue.ToTien(_priceFormatter);
                                        modelsodoghe.TongKet[modelsodoghe.SoDiemDon, modelsodoghe.SoCot + 1].TongKhach++;
                                    }

                                }

                            }

                        }
                    }
                }
            }
            //selected tab
            SaveSelectedTabIndex(TangIndex);
            modelsodoghe.isCheckChuyenVe = CheckChuyenVeCopy();
            return modelsodoghe;
        }
        public ActionResult _InPhoiVe(int ChuyenDiId, int? TangIndex,int? PhanLoai)
        {
            var modelsodoghe = GetSoDoGhe(ChuyenDiId,TangIndex.GetValueOrDefault(0),PhanLoai.GetValueOrDefault(0));
            return PartialView(modelsodoghe);
        }
        public ActionResult InPhoiVe(int ChuyenDiId,int PhanLoai)
        {
            var model = new DanhSachChuyenDiModel.ChuyenDiModel();
            model.ChuyenDiId=ChuyenDiId;
            model.PhanLoai=PhanLoai;
            return View(model);
        }
        public ActionResult InDSDonTra(int ChuyenDiId,bool istra)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var model = new DanhSachChuyenDiModel.ChuyenDiModel();
            model.istra = istra;
            model.TenVanPhong = _workContext.CurrentVanPhong.Ma;
            if(ChuyenDiId >0)
            {
                model.chuyendihientai = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId).toModel(_localizationService);
                var phoives = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenDiId);
                if (istra)
                    phoives.Where(c => !string.IsNullOrEmpty(c.ViTriXuongXe)).ToList();
                else
                    phoives.Where(c => !string.IsNullOrEmpty(c.ViTriLenXe)).ToList();
                model.phoives = phoives.Select(c =>
                    {
                        var _item = new PhoiVeModel();
                        _item = c.toModel();
                        var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(c.CustomerId);
                        if (khachhang != null)
                        {
                            _item.TenKhachHang = khachhang.HoTen;
                            _item.SoDienThoai = khachhang.DienThoai;
                        }
                        return _item;
                    }).ToList();
            }
            return View(model);
        }
        // so do ghe doc

        #endregion
        #region Quan ly phoi ve
        public ActionResult Index()
        {
            var model = new QLPhoiVeModel();
            if (Session["NgayDi"] != null)
                model.NgayDi = Convert.ToDateTime(Session["NgayDi"]);
            else
                model.NgayDi = DateTime.Now;
            model.ListHanhTrinh = PrepareHanhTrinhList(false);
            if (model.ListHanhTrinh.Count > 0)
            {
                if (Session["HanhTrinhId"] != null)
                    model.HanhTrinhId = (int)Session["HanhTrinhId"];
                else
                    model.HanhTrinhId = Convert.ToInt32(model.ListHanhTrinh[0].Value);
            }
            if (Session["KhungGioId"] != null)
                model.KhungGioId = (int)Session["KhungGioId"];
            else 
                model.KhungGioId= (int)ENKhungGio.All;
            model.khunggios = this.GetCVEnumSelectList<ENKhungGio>(_localizationService, model.KhungGioId);
            // loc theo loai xe
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.ListLoaiXes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Text = c.TenLoaiXe;
                item.Value = c.Id.ToString();
                return item;
            }).ToList();
            if (model.ListLoaiXes.Count > 0)
            {
                if (Session["LoaiXeId"] != null)
                    model.LoaiXeId = (int)Session["LoaiXeId"];
                else
                    model.LoaiXeId = (int)ENLoaiXe.GN_40_CHO;
            }                
            //lay tat ca nhan vien
            model.AllLaiXePhuXes = _nhaxeService.GetAllNhanVienByNhaXe(_workContext.NhaXeId, new ENKieuNhanVien[] { ENKieuNhanVien.LaiXe, ENKieuNhanVien.PhuXe }).Select(c =>
            {
                return new XeXuatBenItemModel.NhanVienLaiPhuXe(c.Id, c.ThongTin());
            }).ToList();
            //lay tat ca thong tin xe
            model.AllXeInfo = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.NhaXeId).Select(c =>
            {
                return new XeXuatBenItemModel.XeVanChuyenInfo(c.Id, c.BienSo);
            }).ToList();
            return View(model);
        }
        public ActionResult _TabChuyenDi(int HanhTrinhId, int KhungGioId, DateTime NgayDi, string ThongTinKhachHang, int LoaiXeId)
        {
            var model = new QLPhoiVeModel();
            model.HanhTrinhId = HanhTrinhId;
            model.LoaiXeId = LoaiXeId;
            model.NgayDi = NgayDi;
            model.KhungGioId = KhungGioId;
            model.isTaoChuyen = this.isRightAccess(_permissionService, StandardPermissionProvider.CVQLChuyen);
            if (Session["NgayDi"] != null && Session["HanhTrinhId"] !=null)
            {
                if (HanhTrinhId != (int)Session["HanhTrinhId"] || NgayDi != Convert.ToDateTime(Session["NgayDi"]))
                    Session["ChuyenDiId"] = null;
            }            
            Session["HanhTrinhId"] = HanhTrinhId;
            Session["KhungGioId"] = KhungGioId;
            Session["NgayDi"] = NgayDi;
            Session["LoaiXeId"] = LoaiXeId;            
            //lay thong tin chuyen di
            //model.chuyendis = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, NgayDi, HanhTrinhId, ENKhungGio.All, ThongTinKhachHang, true, LoaiXeId).Select(c => { return c.toModel(_localizationService); }).ToList();
            //lay thong tin chuyen di cua tat ca cac loai xe
            model.chuyendis = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, NgayDi, HanhTrinhId, ENKhungGio.All, "", true, 0).Select(c => { return c.toModel(_localizationService); }).ToList();
            if(model.chuyendis.Count == 0)
            {
                //them mac dinh chuyen di
                var listhanhtrinh = Enum.GetValues(typeof(ENHanhTrinh)).Cast<int>().ToArray();
                var listLoaiXe = Enum.GetValues(typeof(ENLoaiXe)).Cast<int>().ToArray();
                if(listhanhtrinh.Contains(HanhTrinhId))
                {
                    var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId, _workContext.NhaXeId)
                        .Where(c=>!c.KhoaLichTrinh);
                        //.Where(c => c.LoaiXeId == (int)ENLoaiXe.GN_40_CHO).Select(c => c.Id).ToList();
                    if(HanhTrinhId == (int)ENHanhTrinh.HCM_PH || HanhTrinhId ==(int)ENHanhTrinh.PH_HCM)
                        lichtrinhs = lichtrinhs.Where(c => c.LoaiXeId == (int)ENLoaiXe.GN_37_CHO).ToList();
                    else
                        lichtrinhs = lichtrinhs.Where(c => c.LoaiXeId == (int)ENLoaiXe.GN_40_CHO).ToList();
                    var lichtrinhids = lichtrinhs.Select(c => c.Id).ToList();
                    var nguonves = _hanhtrinhService.GetAllNguonVeXeByHanhTrinh(lichtrinhids).ToList();
                    foreach(var nguonve in nguonves)
                    {
                        var chuyendi = new HistoryXeXuatBen();
                        chuyendi.NhaXeId = _workContext.NhaXeId;
                        chuyendi.HanhTrinhId = HanhTrinhId;
                        chuyendi.NguonVeId = nguonve.Id;
                        chuyendi.NgayTao = DateTime.Now;
                        chuyendi.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
                        chuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                        chuyendi.NgayDi = NgayDi.Date.AddHours(nguonve.ThoiGianDi.Hour).AddMinutes(nguonve.ThoiGianDi.Minute);
                        _nhaxeService.InsertHistoryXeXuatBen(chuyendi);
                        TaoNhatKyChuyenDi(chuyendi.Id, "Tạo mới thông tin chuyến đi", chuyendi.TrangThai);
                    }
                }
                //var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId);
            }
            model.chuyendis = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, NgayDi, HanhTrinhId, (ENKhungGio)KhungGioId, ThongTinKhachHang, true, 0).Select(c => { return c.toModel(_localizationService); }).ToList();
            if (model.chuyendis.Count > 0)
            {
                //lay thong tin chuyen di gan nhat
                var message = "";
                foreach (var cd in model.chuyendis)
                {
                    var _thoigiandi = DateTime.Now.Date.AddHours(cd.NgayDi.Hour).AddMinutes(cd.NgayDi.Minute);
                    model.ChuyenDiId = cd.Id;
                    model.NguonVeId = cd.NguonVeId;
                    if (!String.IsNullOrEmpty(ThongTinKhachHang))
                    {
                        var khachhang = _nhaxeService.GetKhachHang(ThongTinKhachHang);
                        var phoives = _phoiveService.GetPhoiVeByChuyenDiId(cd.Id).Where(c=>c.CustomerId==khachhang.CustomerId).ToList();
                        
                        if(phoives.Count >0)
                        {
                            var phoive = phoives.SingleOrDefault();
                            message = "Đã tìm thấy khách hàng " + khachhang.HoTen + " ở ghế " + phoive.sodoghexequytac.Val;
                        }
                    }
                    if (_thoigiandi > DateTime.Now)
                    {
                        break;
                    }
                }
                model.Message = message;
                if(Session["ChuyenDiId"] !=null)
                {
                    model.ChuyenDiId = (int)Session["ChuyenDiId"];
                }
                
            }

            return PartialView(model);
        }

        #endregion
        #region "Dat mua ve"
        private string GetChonGeSession(bool isCreate = true)
        {
            if (Session["ChonGheGroup"] == null && isCreate)
            {
                Session["ChonGheGroup"] = Guid.NewGuid().ToString();
            }
            if (Session["ChonGheGroup"] == null)
            {
                return "";
            }
            return Session["ChonGheGroup"].ToString();
        }
        private void ClearChonGeSession()
        {
            Session["ChonGheGroup"] = null;
        }
        [HttpPost]
        public ActionResult ChonGheDatCho(int ChuyenDiId, string KiHieuGhe, int tang)
        {


            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(KiHieuGhe) || tang == 0)
                return Loi();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var item = new PhoiVe();
            item.ChuyenDiId = chuyendi.Id;
            item.NguonVeXeId = chuyendi.NguonVeId;
            item.NgayDi = chuyendi.NgayDi;
            item.SoDoGheXeQuyTacId = _vexeService.GetSoDoGheXeQuyTacID(chuyendi.NguonVeInfo.LoaiXeId, KiHieuGhe, tang);
            if (item.SoDoGheXeQuyTacId > 0)
            {
                item.TrangThai = ENTrangThaiPhoiVe.DatCho;
                item.isChonVe = false;//giao dich cua nha xe
                item.NguoiDatVeId = _workContext.CurrentNhanVien.Id;
                item.CustomerId = _workContext.CurrentCustomer.Id;
                item.SessionId = GetChonGeSession();
                item.GiaVeHienTai = chuyendi.NguonVeInfo.GiaVeHienTai;
                if (_phoiveService.DatVe(item))
                {

                    return ThanhCong();
                }
            }
            return Loi();
        }
        [HttpPost]
        public ActionResult HuyGheDatCho(int PhoiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai dat cho hoac khac session hoac khac nguoi dang dat
            if (phoive.TrangThai != ENTrangThaiPhoiVe.DatCho)
                return Loi();
            if (phoive.SessionId != GetChonGeSession(false) && phoive.NguoiDatVeId != _workContext.CurrentNhanVien.Id)
                return KhongSoHuu();
            _phoiveService.DeletePhoiVe(phoive);
           
            _giaodichkeveService.HuyBanVe(_workContext.NhaXeId, _workContext.CurrentNhanVien.Id, phoive.VeXeItemId.GetValueOrDefault(0));
            return ThanhCong();
        }
        public ActionResult KhachHangDatMuaVe(int ChuyenDiId,int PhoiVeId)
        {
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
            if(PhoiVeId > 0)
            {
                var item = _phoiveService.GetPhoiVeById(PhoiVeId);
                if(item !=null)
                {
                    var ngayban = chuyendi.NgayDi.Date;
                    var nguonves = _hanhtrinhService.GetAllHanhTrinhGiaVeThoiGian(_workContext.NhaXeId, chuyendi.HanhTrinhId).Where(c =>
                        c.TuNgay.Date <= ngayban
                        && c.DenNgay.Date >= ngayban).ToList();
                    model.ChangId = item.ChangId.GetValueOrDefault(0);
                    var nguonvecons = nguonves.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = string.Format("{0} {1} {2}", c.DiemDi.TenDiemDon, c.DiemDen.TenDiemDon, c.GiaVe.ToString()),
                        Selected = (c.Id==model.ChangId)
                    }).ToList();
                    model._changs = nguonvecons;
                    if (item.TrangThaiId == (int)ENTrangThaiPhoiVe.DaGiaoHang)
                        model.DaThanhToan = true;
                    else
                        model.DaThanhToan = false;
                    model.TenDaiLy = "";
                    if (item.daily != null)
                        model.TenDaiLy = item.daily.TenDaiLy;
                    model.ViTriLen = item.ViTriLenXe;
                    model.ViTriXuong = item.ViTriXuongXe;
                    model.GhiChu = item.GhiChu;
                    var customer = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(item.CustomerId);
                    model.SoDienThoai = customer.DienThoai;
                    model.TenKhachHang = customer.HoTen;
                    model.PhoiVeId_ChuyenVe = item.Id;
                    model.Id = customer.Id;
                }
            }
            else
            {
                model.PhoiVeId_ChuyenVe = 0;
                model.ChuyenDiId = ChuyenDiId;
                model.NguonVeXeIdDangChon = chuyendi.NguonVeId;
                model.NgayDiDangChon = chuyendi.NgayDi.ToString("yyyy-MM-dd");

                /* var nguonves1 = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId
                     );
                 model._changs = nguonves1.Select(c => new SelectListItem
                 {
                     Value = c.Id.ToString(),
                     Text = c.ToMoTaHanhTrinhGiaVe(),
                 }).ToList();
                 * */
                var ngayban = chuyendi.NgayDi.Date;
                var nguonves = _hanhtrinhService.GetAllHanhTrinhGiaVeThoiGian(_workContext.NhaXeId, chuyendi.HanhTrinhId).Where(c =>
                    c.TuNgay.Date <= ngayban
                    && c.DenNgay.Date >= ngayban).ToList();
                var nguonvecons = nguonves.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = string.Format("{0} {1} {2}", c.DiemDi.TenDiemDon, c.DiemDen.TenDiemDon, c.GiaVe.ToString())
                }).ToList();
                model._changs = nguonvecons;
                var vanphongs = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId);
                model.quaybanves = vanphongs.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = string.Format("", c.TenVanPhong, c.Ma)
                }).ToList();
            }
            
            return PartialView(model);

        }


        [HttpPost]
        public ActionResult ThanhToanGiuCho(int PhoiVeId,int ChuyenDiId, int ChangId, string TenKhachHang, string SoDienThoai, bool DaThanhToan,int DaiLyId, int Id, string GhiChu,string ViTriLen,string ViTriXuong, bool IsForKid, bool isKhachVangLai)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            int CustomerId = 0;
            if (!isKhachVangLai)
            {
                //if (string.IsNullOrEmpty(TenKhachHang))
                    //return Loi();
                if (string.IsNullOrEmpty(SoDienThoai))
                    return Loi();
                if (Id > 0)
                {
                    //cap nhat thong tin khach hang
                    var khachhang = _nhaxecustomerService.GetNhaXeCustomerById(Id);
                    khachhang.HoTen = TenKhachHang;
                    khachhang.DienThoai = SoDienThoai;
                    _nhaxecustomerService.UpdateNhaXeCustomer(khachhang);
                    CustomerId = khachhang.CustomerId;
                }
                else
                {
                    //them moi thong tin khach hang                
                    var khachhang = _nhaxecustomerService.CreateNew(_workContext.NhaXeId, TenKhachHang, SoDienThoai, "");
                    //dathanhtoan->daGiaoHang. chưa thanh toan->dang xu ly            
                    CustomerId = khachhang.CustomerId;
                }
            }
            else
                CustomerId = CommonHelper.KhachVangLaiId;
            var kq = "SuaThongTin";
            if(PhoiVeId > 0)
            {
                var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
                phoive.CustomerId = CustomerId;
                phoive.ViTriLenXe = ViTriLen;
                phoive.ViTriXuongXe = ViTriXuong;
                phoive.GhiChu = GhiChu;
                if (!DaThanhToan)
                {
                    var chang = _hanhtrinhService.GetHanhTrinhGiaVeThoiGianById(ChangId);
                    phoive.GiaVeHienTai = chang.GiaVe;
                    phoive.ChangId = ChangId;
                }
                    
                phoive.DaiLyId = null;
                if(DaiLyId>0)
                    phoive.DaiLyId = DaiLyId;
                _phoiveService.UpdatePhoiVe(phoive);
            }
            if(PhoiVeId==0)
            {
                kq = _phoiveService.NhaXeThanhToanGiuChoPhoiVeTheoChuyen(_workContext.NhaXeId, ChuyenDiId, ChangId, GetChonGeSession(false), _workContext.CurrentCustomer.Id, DaThanhToan,DaiLyId, CustomerId, GhiChu, ViTriLen, ViTriXuong, IsForKid);
                ClearChonGeSession();
            }           
            return Json(kq, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DatVeNhanh(int ChuyenDiId, int changid, string NgayDi)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();

            var kq = _phoiveService.NhaXeThanhToanNhanhTheoChuyen(ChuyenDiId, changid, GetChonGeSession(false), _workContext.CurrentNhanVien.Id);
            ClearChonGeSession();
            return Json(kq, JsonRequestBehavior.AllowGet);

        }

        #endregion
        #region "Huy hoac chuyen ghe"
        public ActionResult KhachHangChuyenVe(int PhoiVeId, int HanhTrinhId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //kiem tra tinh hop le cua phoi ve
            if (phoive.isChonVe)
                return AccessDeniedView();
            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy || phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
            {
                var khachhang = _nhaxecustomerService.GetNhaXeCustomerByCustomerId(phoive.CustomerId);
                var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
                model.PhoiVeId_ChuyenVe = PhoiVeId;
                model.NguonVeXeId_ChuyenVe = phoive.ChuyenDiId;
                model.TenKhachHang = khachhang.HoTen;
                model.SoDienThoai = khachhang.DienThoai;
                model.NgayDi_ChuyenVe = phoive.NgayDi;
                model.DaThanhToan = false;
                if (phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
                    model.DaThanhToan = true;
                //tim thoi gian gan nhat voi lich trinh xe dang co
                var chuyendis = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, phoive.NgayDi, HanhTrinhId, ENKhungGio.All, "", false);
                model.ListNguonVeXe_ChuyenVe = chuyendis.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = string.Format("{0} - ({1}:{2})", c.NguonVeInfo.ThoiGianDi.ToString("HH:mm"), c.NguonVeInfo.LichTrinhInfo.MaLichTrinh, c.NguonVeInfo.TenLoaiXe),
                    Selected = c.Id == model.NguonVeXeId_ChuyenVe
                }).ToList();

                return PartialView(model);
            }
            return AccessDeniedView();
        }

        [HttpPost]
        public ActionResult ChonChuyenVe(int PhoiVeId, int ChuyenDiId, string KiHieuGhe, int Tang, bool DaThanhToan)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            //lay thong tin phoi ve
            var _phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            //dat ve moi
            var item = new PhoiVe();
            item.ChuyenDiId = ChuyenDiId;
            item.NguonVeXeId = chuyendi.NguonVeId;
            item.NgayDi = chuyendi.NgayDi;
            var nguonvexe = _vexeService.GetNguonVeXeById(item.NguonVeXeId);
            item.SoDoGheXeQuyTacId = _vexeService.GetSoDoGheXeQuyTacID(nguonvexe.LoaiXeId, KiHieuGhe, Tang);
            if (item.SoDoGheXeQuyTacId > 0)
            {
                ENTrangThaiPhoiVe trangthai = ENTrangThaiPhoiVe.ChoXuLy;
                if (DaThanhToan)
                    trangthai = ENTrangThaiPhoiVe.DaGiaoHang;
                item.isChonVe = false;//giao dich cua nha xe
                item.NguoiDatVeId = _workContext.CurrentNhanVien.Id;
                item.CustomerId = _phoive.CustomerId;
                item.SessionId = GetChonGeSession();
                item.GiaVeHienTai = _phoive.GiaVeHienTai;
                item.ChangId = _phoive.ChangId;
                item.MaVe = _phoive.MaVe;
                item.GhiChu = _phoive.GhiChu;

                if (_phoiveService.DatVe(item, trangthai))
                {
                    //huy ve cu
                    _phoiveService.HuyPhoiVe(_phoive);
                    ClearChonGeSession();
                    return ThanhCong();
                }
            }


            return Loi();

        }

        [HttpPost]
        public ActionResult HuyVe(int PhoiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai giu cho hoac khong phai cua nha xe dat cho
            if (phoive.isChonVe)
                return KhongSoHuu();
            //neu o trang thai dat cho
            if (phoive.TrangThai == ENTrangThaiPhoiVe.DatCho && phoive.SessionId == GetChonGeSession(false) && phoive.NguoiDatVeId == _workContext.CurrentNhanVien.Id)
            {
                _phoiveService.DeletePhoiVe(phoive);
                return ThanhCong();
            }
            //dang gan ve thi ko dc huy
            //if (phoive.VeXeItemId.HasValue)
            //{
            //    return TrangThaiKhongHopLe();
            //}
            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
            {
                _phoiveService.HuyPhoiVe(phoive);
                //_giaodichkeveService.HuyBanVe(_workContext.NhaXeId, _workContext.CurrentNhanVien.Id, phoive.VeXeItemId.Value);
                return ThanhCong();
            }
            if (phoive.TrangThai == ENTrangThaiPhoiVe.DaGiaoHang)
            {
                _phoiveService.HuyPhoiVe(phoive);
                var vexe = _giaodichkeveService.GetVeXeItemById(phoive.VeXeItemId.Value);
                if (vexe == null)
                    return Json("KhongTonTai", JsonRequestBehavior.AllowGet);
                if (vexe.xexuatben == null)
                    return Json("ChuaCoChuyen", JsonRequestBehavior.AllowGet);
                if (vexe.xexuatben.NgayDi < DateTime.Now.AddMinutes(30))
                {
                    //  lưu ghi chu
                //  khách hủy sau 30p trước giờ xe chạy
                    phoive.TrangThai = ENTrangThaiPhoiVe.Huy;
                    phoive.GhiChu = "Hủy sau 30p trước giờ xe chạy";
                    phoive.NguoiHuyId = _workContext.CurrentNhanVien.Id;
                    _phoiveService.UpdatePhoiVe(phoive);
                    vexe.isKhachHuy = true;
                    _giaodichkeveService.UpdateVeXeItem(vexe);
                    return Json("HuyKhongHoanTien", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //hoan tra 90%
                    phoive.GiaVeHienTai = phoive.GiaVeHienTai * 0.01M;
                    phoive.TrangThai = ENTrangThaiPhoiVe.Huy;
                    phoive.GhiChu = "Hủy trước 30p trước giờ xe chạy";
                    phoive.NguoiHuyId = _workContext.CurrentNhanVien.Id;
                    _phoiveService.UpdatePhoiVe(phoive);
                    vexe.GiaVe = vexe.GiaVe * 0.1M;
                    vexe.isKhachHuy = true;
                    _giaodichkeveService.UpdateVeXeItem(vexe);

                }
                
                return ThanhCong();
            }
            
            return TrangThaiKhongHopLe();
        }
        [HttpPost]
        public ActionResult ThanhToanGiaoVe(int PhoiVeId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            //khong o trang thai giu cho hoac khong phai cua nha xe dat cho
            if (phoive.isChonVe)
                return KhongSoHuu();

            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
            {
                var kq= _phoiveService.ThanhToanGiaoVe(phoive);
                return Json(kq,JsonRequestBehavior.AllowGet);
            }
            return TrangThaiKhongHopLe();
        }
        #endregion
        #region gán seri
        public ActionResult _GanSoSeri(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var _phoiveitem = _phoiveService.GetPhoiVeById(Id);
            var model = new QuanlyPhoiVeModel.KhachHangDatMuaVeModel();
            model.Id = _phoiveitem.Id;
            model.MaVe = "";
            model.QuayBanVeId = 0;
            model.MauVeKyHieuId = 0;
            if (_phoiveitem.VeXeItemId.HasValue)
            {
                var _vexeitem = _giaodichkeveService.GetVeXeItemById(_phoiveitem.VeXeItemId.Value);
                if (_vexeitem != null)
                {
                    model.QuayBanVeId = _vexeitem.VanPhongId.Value;
                    model.MauVeKyHieuId = _vexeitem.MauVeKyHieuId;
                    model.MaVe = _vexeitem.SoSeri;
                }
            }
            model.quaybanves = _nhaxeService.GetAllVanPhongByNhaXeId(_workContext.NhaXeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("{0}({1})", c.TenVanPhong, c.Ma),
                Selected = c.Id == model.QuayBanVeId
            }).ToList();
            model.maukyhieus = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("{0}-{1}", c.MauVe, c.KyHieu),
                Selected = c.Id == model.MauVeKyHieuId
            }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult GanSeriVe(int Id, int QuayBanVeId, int MauVeKyHieuId, string MaVe)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            if (_giaodichkeveService.GanSoSerial(Id, _workContext.CurrentNhanVien.Id, QuayBanVeId, MauVeKyHieuId, MaVe))
                return ThanhCong();
            return Loi();
        }
        [HttpPost]
        public ActionResult GanSeriVeBoSung(int PhoiVeId,int ChuyenDiId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVHoatDongBanVe))
                return AccessDeniedView();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var phoive = _phoiveService.GetPhoiVeById(PhoiVeId);
            if (phoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
            {
                var kq = _phoiveService.GanSeriVeBoSung(phoive);
                return Json(kq, JsonRequestBehavior.AllowGet);
            }
            return Loi();
        }

        #endregion
        #region quản lý hủy vé
        public ActionResult ListVeYeuCauHuy()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLHuy))
                return AccessDeniedView();

            var model = new PhoiVeModel();
            model.NgayDisearch = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult ListVeYeuCauHuy(DataSourceRequest command, PhoiVeModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLHuy))
                return AccessDeniedView();

            var array = _phoiveService.GetPhoiVeYeuCauHuy(_workContext.CurrentVanPhong.Id);
            var phoives = array.Select(c =>
            {
                return c.toModel();
            }).ToList();

            var gridModel = new DataSourceResult
            {
                Data = phoives,
                Total = phoives.Count
            };

            return Json(gridModel);


        }
        [HttpPost]
        public ActionResult YeuCauHuyVe(int PhoiVeId, string LyDoHuy)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLHuy))
                return AccessDeniedView();
            var item = _phoiveService.GetPhoiVeById(PhoiVeId);
            if (item != null)
            {
                item.GhiChu = item.GhiChu + LyDoHuy;
                item.IsRequireCancel = true;
                _phoiveService.UpdatePhoiVe(item);
                return ThanhCong();
            }
            return Loi();
        }


        #endregion
        #region Thiet lap thong lai phu xe
        [HttpPost]
        public ActionResult ThietDatChuyenDi_Luu(string laiphuxeids, int XeVanChuyenId, int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            if (chuyendi != null)
            {
                // xóa hết các lái xe cũ
                chuyendi.LaiPhuXes.Clear();
                _nhaxeService.DeleteHistoryXeXuatBenNhanVien(chuyendi.Id);
                //up date lai xe mơi               
                int[] idlaiphuxes = Array.ConvertAll(laiphuxeids.Split(','), s => int.Parse(s));
                for (int i = 0; i < idlaiphuxes.Length; i++)
                {
                    var nhanvien = _nhanvienService.GetById(idlaiphuxes[i]);
                    if (nhanvien != null)
                    {
                        var _nhanvienxuatben = new HistoryXeXuatBen_NhanVien();
                        _nhanvienxuatben.NhanVien_Id = nhanvien.Id;
                        _nhanvienxuatben.HistoryXeXuatBen_Id = chuyendi.Id;
                        if (i == 0)
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.LaiXe;
                        else
                            _nhanvienxuatben.KieuNhanVien = ENKieuNhanVien.PhuXe;
                        chuyendi.LaiPhuXes.Add(_nhanvienxuatben);
                    }

                }
                chuyendi.TrangThai = ENTrangThaiXeXuatBen.DANG_DI;
                chuyendi.XeVanChuyenId = XeVanChuyenId;
                _nhaxeService.UpdateHistoryXeXuatBen(chuyendi);
                //luu log
                TaoNhatKyChuyenDi(chuyendi.Id, "Thiết đặt xe xuất bến", ENTrangThaiXeXuatBen.DANG_DI);
            }

            //lay lai thong tin
            chuyendi = _nhaxeService.GetHistoryXeXuatBenId(chuyendi.Id);
            return Json(chuyendi.toModel(_localizationService));

        }
        [HttpPost]
        public ActionResult ThietDatChuyenDi_Huy(int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            if (xexuatben == null)
                return Loi();
            xexuatben.XeVanChuyenId = null;
            xexuatben.LaiPhuXes.Clear();
            _nhaxeService.DeleteHistoryXeXuatBenNhanVien(xexuatben.Id);
            xexuatben.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
            _nhaxeService.UpdateHistoryXeXuatBen(xexuatben);
            //luu log
            TaoNhatKyChuyenDi(ChuyenDiId, "Hủy thiết đặt xe xuất bến", ENTrangThaiXeXuatBen.CHO_XUAT_BEN);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult HuyChuyenDi(int ChuyenDiId)
        {
            if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            var xexuatben = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            Session["ChuyenDiId"] = null;
            if (xexuatben == null)
                return Json("NO");
            var phoives = _phoiveService.GetPhoiVeByChuyenDi(ChuyenDiId, true);
            if (phoives.Count > 0)
            {
                return Json("NO");
            }
            var vexe = _giaodichkeveService.GetVeXeItems(xexuatben.Id);
            if (vexe.Count() > 0)
            {
                return Json("NO");
            }
            //kiem tra xem co thong tin tai chinh ko
            //var _taichinh = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(Id);
            //if (_taichinh != null)
            //{
            //    return Json("NO");
            //}
            xexuatben.TrangThai = ENTrangThaiXeXuatBen.HUY;
            _nhaxeService.UpdateHistoryXeXuatBen(xexuatben);
            //luu log
            TaoNhatKyChuyenDi(ChuyenDiId, "Hủy chuyến đi", ENTrangThaiXeXuatBen.HUY);
            return ThanhCong();
        }
        void TaoNhatKyChuyenDi(int ChuyenDiId, string note, ENTrangThaiXeXuatBen trangthai)
        {
            var _log = new HistoryXeXuatBenLog();
            _log.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            _log.NgayTao = DateTime.Now;
            _log.TrangThai = trangthai;
            _log.GhiChu = note;
            _log.XeXuatBenId = ChuyenDiId;
            _nhaxeService.InsertHistoryXeXuatBenLog(_log);

        }
       /// <summary>
       /// create by Mss.Mai
       /// cho phep chon gio và loai xe de tao chuyen
       /// </summary>
       /// <param name="HanhTrinhId"></param>
       /// <param name="LoaiXeId"></param>
       /// <returns></returns>
        public ActionResult _TaoChuyenDi(int HanhTrinhId, int LoaiXeId, DateTime NgayDi)
        {
            
            var model = new XeXuatBenItemModel();
            model.HanhTrinhId = HanhTrinhId;
            model.ThoiGianThuc = DateTime.Now;
            var hanhtrinh = _hanhtrinhService.GetHanhTrinhById(HanhTrinhId);
            model.TuyenXeChay = hanhtrinh.MoTa;
            model.NgayDi = NgayDi.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);
            model.LoaiXeId = LoaiXeId;
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.loaixes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.TenLoaiXe;
                item.Selected = (c.Id == model.LoaiXeId);
                return item;
            }).ToList();
            return PartialView(model);
        }
       
        [HttpPost]
        public ActionResult TaoMoiChuyenDi(int HanhTrinhId, string ThoiGianDi, int LoaiXeId, DateTime NgayDi)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            if(HanhTrinhId==0)
                return AccessDeniedView();
            if (LoaiXeId == 0)
                return AccessDeniedView();
            var chuyendi = new HistoryXeXuatBen();
            chuyendi.NhaXeId = _workContext.NhaXeId;
            chuyendi.HanhTrinhId = HanhTrinhId;
            DateTime _thoigiandi = Convert.ToDateTime(ThoiGianDi);
            //lay nguon ve gan voi thoi gian chon
            //var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId).Select(c=>c.Id).ToList();
            var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(HanhTrinhId).Where(c=>!c.KhoaLichTrinh).Select(c => c.Id).ToList();
            var nguonves = _hanhtrinhService.GetAllNguonVeXeByHanhTrinhLoaiXe(lichtrinhs,LoaiXeId).OrderBy(c => c.ThoiGianDi).ToList();
            NguonVeXe _nguonvexe = null;
            _thoigiandi = NgayDi.Date.AddHours(_thoigiandi.Hour).AddMinutes(_thoigiandi.Minute);

            foreach (var nv in nguonves)
            {
               
                DateTime fromDate = NgayDi.Date.AddHours(nv.ThoiGianDi.Hour).AddMinutes(nv.ThoiGianDi.Minute);
               
                //neu nam trong khoang thi break
                if (_thoigiandi >= fromDate )
                {
                    _nguonvexe = nv;
                    break;
                }
            }
            if(_nguonvexe==null)
            {
                return Json("Khung gio khong co trong lich trinh", JsonRequestBehavior.AllowGet);
            }

            chuyendi.NguonVeId = _nguonvexe.Id;
            chuyendi.NgayTao = DateTime.Now;
            chuyendi.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
            chuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            chuyendi.NgayDi = _thoigiandi;          
            _nhaxeService.InsertHistoryXeXuatBen(chuyendi);
            TaoNhatKyChuyenDi(chuyendi.Id, "Tạo mới thông tin chuyến đi", chuyendi.TrangThai);
            return ThanhCong();
        }
        public ActionResult _ChuyenSoDo(int ChuyenId)
        {
            var model = new XeXuatBenItemModel();
            model.Id = ChuyenId;
            var _chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenId);
            model.LoaiXeId = _chuyendi.NguonVeInfo.LoaiXeId;
            var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
            model.loaixes = loaixes.Select(c =>
            {
                var item = new SelectListItem();
                item.Value = c.Id.ToString();
                item.Text = c.TenLoaiXe;
                item.Selected = (c.Id == model.LoaiXeId);
                return item;
            }).ToList();
            if(_chuyendi.XeVanChuyenId>0)
            {
                model.XeVanChuyenId = _chuyendi.XeVanChuyenId.Value;
                model.BienSo = _chuyendi.xevanchuyen.BienSo;
            }
            model.AllXeInfo = _xeinfoService.GetAllXeInfoByLoaiXeId(model.LoaiXeId).Select(c =>
            {
                 var m= new XeXuatBenItemModel.XeVanChuyenInfo(c.Id,c.BienSo) ;                
                 m.LoaiXeId = c.LoaiXeId;
                 return m;
             }).ToList();
            //model.AllXeInfo = _xeinfoService.GetAllXeInfoByNhaXeId(_workContext.NhaXeId).Select(c =>
            //{
              //  var m= new XeXuatBenItemModel.XeVanChuyenInfo(c.Id,c.BienSo) ;                
               // m.LoaiXeId = c.LoaiXeId;
               /// return m;
           // }).ToList();
            return PartialView(model);
        }
        /// <summary>
        /// chuyen so do ghe: tao chuyen moi , update nguonveid, sodo,chuyendi
        /// </summary>
        /// <param name="HanhTrinhId"></param>
        /// <param name="NguonVeId"></param>
        /// <param name="NgayDi"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChuyenSoDoghe(int ChuyenId, int XeVanChuyenId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CVQLChuyen))
                return AccessDeniedView();
            if (XeVanChuyenId == 0 || ChuyenId == 0)
                return Loi();            
            var _chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenId);
            var xe = _xeinfoService.GetXeInfoById(XeVanChuyenId);
            
            //lay tat cac xe da dieu trong ngay
            var xevanchuyenids = _nhaxeService.GetAllChuyenDiTrongNgay(_workContext.NhaXeId, _chuyendi.NgayDi, _chuyendi.HanhTrinhId, ENKhungGio.All, "", true, xe.LoaiXeId).Select(c => c.XeVanChuyenId).ToArray();
            if (xevanchuyenids.Contains(XeVanChuyenId))
                return Json("XeDaDieu", JsonRequestBehavior.AllowGet);
            //count so ghe
            var phoives = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenId);
            //kiem tra so ghe trong
            var SlgheOld = _chuyendi.NguonVeInfo.loaixe;
            

            var loaixe = _xeinfoService.GetById(xe.LoaiXeId);
            //kiem tra so ghe rong 
            if (phoives.Count() > loaixe.sodoghe.SoLuongGhe)
                return Json("Không đủ ghế trống ", JsonRequestBehavior.AllowGet);
            //kiem tra quy tac trung
            // var sodoquytacold = _xeinfoService.GetAllSoDoGheXeQuyTac(SlgheOld.Id).Where(c => c.x > 0 && c.y > 0).Select(c => c.Val).ToList();
            // var sodoquytacnew = _xeinfoService.GetAllSoDoGheXeQuyTac(loaixe.Id).Where(c => c.x > 0 && c.y > 0).Select(c => c.Val).ToList();
            //foreach (var m in sodoquytacold)
            //{
            //    if (!sodoquytacnew.Contains(m))
            //        return Json("SoDoKhongKhop", JsonRequestBehavior.AllowGet);
            //}
            //tao chuyen moi, cung nguonve voi chuyen ban dau
            var chuyendi = new HistoryXeXuatBen();
            chuyendi.NhaXeId = _workContext.NhaXeId;
            chuyendi.HanhTrinhId = _chuyendi.HanhTrinhId;
            chuyendi.XeVanChuyenId = XeVanChuyenId;
            var _nguonve = _hanhtrinhService.GetNguonVeXeById(_chuyendi.NguonVeId);
            // lay nguon ve cung hanh trinh,  gio voi nguon ve hien tai va loai xe da chon
            var nguonvenew = _hanhtrinhService.GetNguonVeXeByloaixe(chuyendi.HanhTrinhId, _nguonve.ThoiGianDi, xe.LoaiXeId);
            if (nguonvenew == null)
                return Json("KhongNguonVe", JsonRequestBehavior.AllowGet);
            chuyendi.NguonVeId = nguonvenew.Id;
            chuyendi.NgayTao = DateTime.Now;
            chuyendi.TrangThai = ENTrangThaiXeXuatBen.CHO_XUAT_BEN;
            chuyendi.NguoiTaoId = _workContext.CurrentNhanVien.Id;
            chuyendi.NgayDi = _chuyendi.NgayDi;
            _nhaxeService.InsertHistoryXeXuatBen(chuyendi);
            Session["ChuyenDiId"] = chuyendi.Id;
            //update phoive
            foreach (var item in phoives)
            {
                item.ChuyenDiId = chuyendi.Id;
                //lay so do ghe con trong
                var sdgquytac = _xeinfoService.GetAllSoDoGheXeQuyTac(xe.LoaiXeId).Where(c => c.x > 0 && c.y > 0 && c.Val != "").ToList();
                var _sdquytac = sdgquytac.Where(c => c.Val == item.sodoghexequytac.Val).ToList();
                if (_sdquytac.Count > 0)
                {
                    var _sd=_sdquytac.FirstOrDefault();
                    var _phoive = _phoiveService.GetPhoiVe(nguonvenew.Id, _sd, chuyendi.NgayDi, true);
                    if(_phoive.TrangThai == ENTrangThaiPhoiVe.ConTrong)
                    {
                        item.SoDoGheXeQuyTacId = _sd.Id;
                        item.NguonVeXeId = nguonvenew.Id;
                        _phoiveService.UpdatePhoiVe(item);
                        //update vexeitem
                        if (item.vexeitem != null)
                        {
                            var vexe = item.vexeitem;
                            vexe.XeXuatBenId = chuyendi.Id;
                            _giaodichkeveService.UpdateVeXeItem(vexe);
                        }
                        continue;
                    }
                }
                foreach (var s in sdgquytac)
                {
                    var phoive = _phoiveService.GetPhoiVe(nguonvenew.Id, s, chuyendi.NgayDi, true);
                    if (phoive.TrangThai == ENTrangThaiPhoiVe.ConTrong)
                    {
                        //update phoive
                        item.SoDoGheXeQuyTacId = phoive.SoDoGheXeQuyTacId;
                        //if (item.sodoghexequytac.Val == s.Val)
                            //item.SoDoGheXeQuyTacId = s.Id;
                        item.NguonVeXeId = nguonvenew.Id;
                        _phoiveService.UpdatePhoiVe(item);
                        //update vexeitem
                        if (item.vexeitem != null)
                        {
                            var vexe = item.vexeitem;
                            vexe.XeXuatBenId = chuyendi.Id;
                            _giaodichkeveService.UpdateVeXeItem(vexe);
                        }
                        break;
                    }
                }
            }
            _chuyendi.TrangThaiId = (int)ENTrangThaiXeXuatBen.HUY;
            _nhaxeService.UpdateHistoryXeXuatBen(_chuyendi);
            return ThanhCong();
        }
        /// <summary>
        /// cau hinh gia ve cho nguon ve va lich trinh theo hanh trinh
        /// </summary>
        /// <param name="ChuyenId"></param>
        /// <returns></returns>
      
        public ActionResult CauHinhGiaVeChung()
        {
            var model = new QLHanhTrinhGiaVeModel();
            // tao lich trinh, nguon ve, tu hanh trinh
            var hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            foreach (var item in hanhtrinhs)
            {
                var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId);
                foreach (var lx in loaixes)
                {
                    var _hanhtrinhloaixe = new HanhTrinhGiaVeModel();
                    _hanhtrinhloaixe.HanhTrinhId = item.Id;
                    _hanhtrinhloaixe.TenHanhTrinh = item.MoTa;
                    _hanhtrinhloaixe.LoaiXeId = lx.Id;
                    _hanhtrinhloaixe.TenLoaiXe = lx.TenLoaiXe;
                    var lts = _hanhtrinhService.GetLichTrinhByHanhTrinhLoaiXe(item.Id, lx.Id);
                    if (lts.Count() > 0)
                    {
                        _hanhtrinhloaixe.GiaVe = lts.First().GiaVeToanTuyen;
                        _hanhtrinhloaixe.isTienDo = Convert.ToBoolean(lts.First().LoaiTienId);
                    }

                    model.ListHanhTrinh.Add(_hanhtrinhloaixe);
                }
            }
            //var hanhtrinhs = _hanhtrinhService.GetAllHanhTrinhByNhaXeId(_workContext.NhaXeId);
            //foreach (var item in hanhtrinhs)
            //{
            //    var lichtrinhs = _hanhtrinhService.GetAllLichTrinhByHanhTrinhId(item.Id);
            //    foreach (var m in lichtrinhs)
            //    {
            //        var loaixes = _xeinfoService.GetAllByNhaXeId(_workContext.NhaXeId).Where(c => c.Id != m.LoaiXeId).ToList();
            //        foreach (var lx in loaixes)
            //        {
            //            LichTrinh lt = new LichTrinh();

            //            lt.MaLichTrinh = m.MaLichTrinh;
            //            lt.HanhTrinhId = m.HanhTrinhId;                       


            //            lt.SoGioChay = m.SoGioChay;
            //            lt.ThoiGianDi = m.ThoiGianDi;
            //            lt.ThoiGianDen = m.ThoiGianDen;
            //            lt.TimeOpenOnline = m.TimeOpenOnline;
            //            lt.TimeCloseOnline = m.TimeCloseOnline;
            //            lt.KhoaLichTrinh = m.KhoaLichTrinh;
            //            lt.GiaVeToanTuyen = m.GiaVeToanTuyen;
            //            lt.NhaXeId = m.NhaXeId;
            //            lt.LoaiTienId = m.LoaiTienId;
            //            lt.IsLichTrinhGoc = m.IsLichTrinhGoc;
            //            lt.LoaiXeId = lx.Id;
            //            _hanhtrinhService.InsertLichTrinh(lt);
            //            // tao nguon ve
            //            var lts = new List<int>();
            //            lts.Add(lt.Id);
            //            var nguonve = _hanhtrinhService.GetAllNguonVeXeByHanhTrinh(lts);
            //            NguonVeXe nv = new NguonVeXe();
            //            nv = nguonve.FirstOrDefault();
            //            if (nv == null)
            //            {

            //                _hanhtrinhService.InsertNguonVeGoc(lt);
            //            }
            //        }
            //        // tao lich trinh, nguon ve 

            //    }

            //}
            //.Select(c =>
            //{
            //    var item = new HanhTrinhGiaVeModel();
            //    item.HanhTrinhId = c.Id;
            //    item.TenHanhTrinh = c.MoTa;

            //});

            return View(model);
        }
        [HttpPost]
        public ActionResult CauHinhGiaVeChung(int HanhTrinhId, int LoaiXeId, decimal GiaVe, bool isTienDo)
        {



            var lichtrinhs = _hanhtrinhService.GetLichTrinhByHanhTrinhLoaiXe(HanhTrinhId, LoaiXeId);
            foreach (var lt in lichtrinhs)
            {
                lt.GiaVeToanTuyen = GiaVe;
                lt.LoaiTienId = Convert.ToInt32(isTienDo);
                _hanhtrinhService.UpdateLichTrinh(lt);
                var lts = new List<int>();
                lts.Add(lt.Id);
                var nguonve = _hanhtrinhService.GetAllNguonVeXeByHanhTrinh(lts);
                if (nguonve.First() != null)
                {
                    var item = nguonve.First();
                    item.GiaVeHienTai = GiaVe;
                    item.LoaiTienId = Convert.ToInt32(isTienDo);
                    _hanhtrinhService.UpdateNguonVeXe(item);
                }
            }
            return Json(GiaVe);
        }
        
        #endregion
        #region Chuyen di tai chinh: thu chi , doanh thu, hoa hong
        ChuyenDiTaiChinhModel toChuyenDiTaiChinhModel(ChuyenDiTaiChinh entity)
        {
            var model = new ChuyenDiTaiChinhModel();
            model.Id = entity.Id;
            model.isCP1 = entity.isCP1;
            model.isCPText = "";
            model.NguoiTaoId = entity.NguoiTaoId;
            model.TenNguoiTao = entity.nguoitao.HoVaTen;
            model.LuotDiId = entity.LuotDiId;
            model.ThucThu = entity.ThucThu;
            model.DinhMucDau = entity.DinhMucDau;

            model.ThucDo = entity.ThucDo;
            model.GiaDau = entity.GiaDau;
            model.VeQuay = entity.VeQuay;
            model.LuotVeId = entity.LuotVeId.GetValueOrDefault(0);
            model.NgayTao = entity.NgayTao;
            model.XeVanChuyenId = entity.XeVanChuyenId.GetValueOrDefault();
            if (entity.xevanchuyen != null)
                model.BienSoXe = entity.xevanchuyen.BienSo;
            //kiem tra thong tin tinh luong

            foreach (var item in entity.GiaoDichThuChis)
            {

                var itemmodel = toChuyenDiTaiChinhThuChiModel(item);
                model.GiaoDichThuChis.Add(itemmodel);
            }
            //thong tin doanh thu

            model.DTLuotDi = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, entity.luotdi).Sum(c => c.GiaVe);
            return model;

        }
        ChuyenDiTaiChinhModel.ChuyenDiTaiChinhThuChiModel toChuyenDiTaiChinhThuChiModel(ChuyenDiTaiChinhThuChi entity)
        {
            var model = new ChuyenDiTaiChinhModel.ChuyenDiTaiChinhThuChiModel();
            model.Id = entity.Id;
            model.ChuyenDiTaiChinhId = entity.ChuyenDiTaiChinhId;
            model.loaithuchi = entity.loaithuchi;

            model.LoaiThuChiText = entity.loaithuchi.ToCVEnumText(_localizationService);
            model.SoTien = entity.SoTien / 1000;
            if (model.LoaiThuChiId > 100)
                model.SoTien = -model.SoTien;
            model.GhiChu = entity.GhiChu;

            return model;

        }

        public ActionResult _TabChiPhiChuyenDi(int ChuyenDiId)
        {
            var model = new ChuyenDiTaiChinhModel();
            var chuyeditc = _giaodichkeveService.GetChuyenDiTaiChinhByLuotId(ChuyenDiId);
            if (chuyeditc != null)
            {
                chuyeditc.VeQuay = _giaodichkeveService.GetVeXeItemsQuay(ChuyenDiId).Sum(c => c.GiaVe);
                _giaodichkeveService.UpdateChuyenDiTaiChinh(chuyeditc);
                model = toChuyenDiTaiChinhModel(chuyeditc);
                model.DoanhThuHang = _phieuguihangService.GetAll(_workContext.NhaXeId, ChuyenDiId).Sum(c => c.HangHoas.Sum(m => m.GiaCuoc * m.SoLuong));



            }
            else
            {
                //neu chua co thi tao moi
                var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
                chuyeditc = new ChuyenDiTaiChinh();
                chuyeditc.NhaXeId = _workContext.NhaXeId;
                chuyeditc.NguoiTaoId = _workContext.CurrentNhanVien.Id;
                chuyeditc.LuotDiId = ChuyenDiId;

                chuyeditc.XeVanChuyenId = chuyendi.XeVanChuyenId;
                _giaodichkeveService.InsertChuyenDiTaiChinh(chuyeditc);
                chuyeditc.VeQuay = _giaodichkeveService.GetVeXeItemsQuay(ChuyenDiId).Sum(c => c.GiaVe);
                _giaodichkeveService.UpdateChuyenDiTaiChinh(chuyeditc);
                // thong tin giao dich thu chi
                //chi
                var chitienan = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TIEN_AN_LAI_PHU_XE);
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_NHA_HANG, -chitienan));
                var chicauduong = _nhaxeService.GetGiaTriCauHinh(_workContext.NhaXeId, ENNhaXeCauHinh.TIEN_CAU_DUONG);
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_CAU_DUONG, -chicauduong));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.GUI_XE_QUA_DEM));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_BEN_XE));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_TIEN_DAU));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_CONG_AN));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_KHAC));
                _giaodichkeveService.InsertChuyenDiTaiChinhThuChi(new ChuyenDiTaiChinhThuChi(chuyeditc.Id, ENLoaiTaiChinhThuChi.CHI_SUA_CHUA_XE));

                var chuyeditcnew = _giaodichkeveService.GetChuyenDiTaiChinhById(chuyeditc.Id);
                model = toChuyenDiTaiChinhModel(chuyeditcnew);

            }
            return PartialView(model);
        }
        public ActionResult _TabLichSuBanVe(int ChuyenDiId)
        {
            var model = _phoiveService.GetLichSuBanVeByChuyenDiId(ChuyenDiId);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _CapNhapChuyenDiTaiChinh(ChuyenDiTaiChinhModel model, string GhiChu)
        {

            string[] arrinfo = model.ListThuChi.Split('|');
            for (int i = 0; i < arrinfo.Length; i = i + 2)
            {
                if (string.IsNullOrWhiteSpace(arrinfo[i]))
                {
                    arrinfo[i] = "0";
                }
                if (string.IsNullOrWhiteSpace(arrinfo[i + 1]))
                {
                    arrinfo[i + 1] = "0";
                }
                int LoaiThuChiId = Convert.ToInt32(arrinfo[i]);
                Decimal SoTien = Convert.ToDecimal(arrinfo[i + 1]) * 1000;
                var item = _giaodichkeveService.GetChuyenDiTaiChinhThuChiById(model.Id, LoaiThuChiId);
                if (item == null)
                    return Loi();
                if (LoaiThuChiId < 100)
                    item.SoTien = SoTien;
                else
                    item.SoTien = -SoTien;
                if (item.loaithuchi == ENLoaiTaiChinhThuChi.CHI_SUA_CHUA_XE)
                {
                    item.GhiChu = GhiChu;
                }
                _giaodichkeveService.UpdateChuyenDiTaiChinhThuChi(item);

            }
            //xe thuong phat dau           

            var Chuyenditc = _giaodichkeveService.GetChuyenDiTaiChinhById(model.Id);

            var doanhthu = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, Chuyenditc.luotdi).Sum(c => c.GiaVe);
            Chuyenditc.VeQuay = _giaodichkeveService.GetVeXeItemsQuay(model.LuotDiId).Sum(c => c.GiaVe);
            Chuyenditc.XeVanChuyenId = Chuyenditc.luotdi.XeVanChuyenId;
            Chuyenditc.ThucThu = doanhthu
                - Chuyenditc.VeQuay
                + Chuyenditc.GiaoDichThuChis.Sum(c => c.SoTien);
            _giaodichkeveService.UpdateChuyenDiTaiChinh(Chuyenditc);

            return ThanhCong();
        }

        [HttpPost]
        public ActionResult HuyCapNhatChiPhi(int Id)
        {
            if (Id == 0)
                return Loi();
            var chuyenditc = _giaodichkeveService.GetChuyenDiTaiChinhById(Id);
            _giaodichkeveService.DeleteAllChuyenDiTaiChinhThuChi(chuyenditc.GiaoDichThuChis.ToList());
            _giaodichkeveService.DeleteChuyenDiTaiChinh(chuyenditc);

            return ThanhCong();
        }
        #endregion
        #region Phoi ve bo sung
        public ActionResult PhoiVeBoSung_List(int ChuyenDiId)
        {
            //lay tat ca thong tin ve theo chuyen di roi format theo list
            var items = new List<PhoiVeBoSungModel>();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var vexeitems = _giaodichkeveService.GetVeXeBanTrenXe(_workContext.NhaXeId, chuyendi);
            var changids = vexeitems.Select(c => c.ChangId).Distinct().ToArray();
            //var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId).Where(c => changids.Contains(c.Id)).ToList();
            var nguonves = _hanhtrinhService.GetAllHanhTrinhGiaVeThoiGian(_workContext.NhaXeId, chuyendi.HanhTrinhId).Where(c => changids.Contains(c.Id)).ToList();
            foreach (var chang in nguonves)
            {
                var vexeitemchangs = vexeitems.Where(c => c.ChangId == chang.Id).ToList();
                var quyens = vexeitemchangs.Select(c => c.SoQuyen).Distinct().ToList();
                foreach (var soquyen in quyens)
                {
                    var vexeitemchangs_quyen = vexeitemchangs.Where(c => c.SoQuyen == soquyen).OrderBy(c => c.SoSeriNum).ToList();
                    var vexeitem = vexeitemchangs_quyen.First();
                    var item = new PhoiVeBoSungModel();
                    item.NhanVienId = vexeitem.NhanVienId.GetValueOrDefault(0);
                    item.SoQuyen = soquyen;
                    item.ChangId = chang.Id;
                    item.TenChang = chang.ToMoTaHanhTrinhGiaVeThoiGian();
                    item.TenMau = string.Format("{0} - {1}", vexeitem.MauVe, vexeitem.KyHieu);
                    item.MauVeId = vexeitem.MauVeKyHieuId;
                    item.TenNhanVien = vexeitem.nhanvien.HoVaTen;
                    item.SeriFrom = vexeitem.SoSeri;
                    item.SoLuong = vexeitemchangs_quyen.Count;
                    item.SeriTo = vexeitemchangs_quyen[vexeitemchangs_quyen.Count - 1].SoSeri;
                    items.Add(item);
                }
            }
            var gridModel = new DataSourceResult
            {
                Data = items,
                Total = items.Count
            };

            return Json(gridModel);
        }
        public ActionResult _PhoiVeBoSung_ThemMoi(int ChuyenDiId)
        {
            var model = new PhoiVeBoSungModel();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            model.ChuyenDiId = chuyendi.Id;
            model.HanhTrinhId = chuyendi.HanhTrinhId;
            //var nguonves = _hanhtrinhService.GetallHanhTrinhGiaVe(chuyendi.HanhTrinhId, _workContext.NhaXeId);
            var nguonves = _hanhtrinhService.GetAllHanhTrinhGiaVeThoiGian(_workContext.NhaXeId, chuyendi.HanhTrinhId);
            model.changs = nguonves.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ToMoTaHanhTrinhGiaVeThoiGian()
            }).ToList();
            model.maukyhieus = _giaodichkeveService.GetAllMauVeKyHieu(_workContext.NhaXeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = string.Format("{0}-{1}", c.MauVe, c.KyHieu),
            }).ToList();
            //model.nhanviens = chuyendi.LaiPhuXes.Select(c => new SelectListItem
            //{
                //Value = c.NhanVien_Id.ToString(),
                //Text = c.nhanvien.ThongTin(false)
           // }).ToList();
            var nhaviens = _nhanvienService.GetAllByVanPhongId(_workContext.CurrentNhanVien.VanPhongID.Value).Where(c=>c.KieuNhanVienID==(int)ENKieuNhanVien.LeTan || c.KieuNhanVienID==(int)ENKieuNhanVien.QuanLy).ToList();
            model.nhanviens = nhaviens.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.HoVaTen
                }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult PhoiVeBoSung_ThongTinSeri(PhoiVeBoSungModel model)
        {

            //var nguonves1 = _hanhtrinhService.GetallHanhTrinhGiaVe(model.HanhTrinhId, _workContext.NhaXeId).Where(c => c.Id == model.ChangId).FirstOrDefault();
            var nguonves = _hanhtrinhService.GetAllHanhTrinhGiaVeThoiGian(_workContext.NhaXeId,model.HanhTrinhId).Where(c => c.Id == model.ChangId).FirstOrDefault();
            //lay thong tin so seri from den to
            var vexeitems = _giaodichkeveService.GetTonVeXeItemsByNhanVien(_workContext.CurrentNhanVien.Id, nguonves.GiaVe, model.MauVeId, model.SoLuong);
            var quyenids = vexeitems.Select(c => c.SoQuyen).Distinct();
            model.SoLuong = 0;
            if (quyenids.Count() > 0)
            {
                var quyenid = quyenids.First();
                var _vexeitems = vexeitems.Where(c => c.SoQuyen == quyenid).OrderBy(c => c.SoSeriNum).ToList();

                model.SoLuong = _vexeitems.Count;
                model.SeriFrom = _vexeitems[0].SoSeri;
                model.SeriTo = _vexeitems[_vexeitems.Count - 1].SoSeri;
            }
            return Json(model);
        }
        [HttpPost]
        public ActionResult PhoiVeBoSung_ThemMoi(PhoiVeBoSungModel model)
        {
            string _serigiamgia = "";
            if (!string.IsNullOrEmpty(model.SeriGiamGia))
            {
                _serigiamgia = model.SeriGiamGia.Replace(" ", "");
            }
            _giaodichkeveService.PhoiVeBoSungThemMoi(model.ChuyenDiId, _workContext.CurrentNhanVien.Id, model.ChangId, model.MauVeId, Convert.ToInt32(model.SeriFrom), Convert.ToInt32(model.SeriTo), _serigiamgia);
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult PhoiVeBoSung_Huy(PhoiVeBoSungModel model)
        {
            _giaodichkeveService.PhoiVeBoSungHuy(model.ChuyenDiId, model.NhanVienId, model.ChangId, model.MauVeId, Convert.ToInt32(model.SeriFrom), Convert.ToInt32(model.SeriTo));
            return ThanhCong();
        }


        #endregion
        #region chuyen ve
        private List<PhoiVeCopyModel> ChuyenVeCopyGet()
        {
            var arrPhoiVeCopy = new List<PhoiVeCopyModel>();
            if (Session["ChuyenVePhoiVeIds"] != null)
            {
                arrPhoiVeCopy = (List<PhoiVeCopyModel>)Session["ChuyenVePhoiVeIds"];
            }
            return arrPhoiVeCopy;
        }
        private void ChuyenVeCopyAdd(PhoiVe item)
        {
            var arrPhoiveCopy = ChuyenVeCopyGet();
            var itemCopy = new PhoiVeCopyModel();
            itemCopy.Id = item.Id;
            itemCopy.Ma = item.MaVe;
            if(!arrPhoiveCopy.Where(c=>c.Id==itemCopy.Id).Any())
            {
                arrPhoiveCopy.Add(itemCopy);
            }
            Session["ChuyenVePhoiVeIds"] = arrPhoiveCopy;
        }
        [HttpPost]
        public ActionResult ChuyenVeCopy(string PhoiveIds, int ChuyenDiId)
        {
            if (PhoiveIds == "-1")
            {
                var phoives = _phoiveService.GetPhoiVeByChuyenDi(ChuyenDiId);
                foreach(var pv in phoives)
                {
                    ChuyenVeCopyAdd(pv);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(PhoiveIds))
                    return Loi();
                var phoiveid = Convert.ToInt32(PhoiveIds);
                var phoive = _phoiveService.GetPhoiVeById(phoiveid);
                if (phoive == null)
                    return Loi();
                if (phoive.TrangThai != ENTrangThaiPhoiVe.DaThanhToan && phoive.TrangThai != ENTrangThaiPhoiVe.DaGiaoHang && phoive.TrangThai != ENTrangThaiPhoiVe.ChoXuLy) 
                    return Loi();
                ChuyenVeCopyAdd(phoive);
            }
            return ThanhCong();
        }
        public bool CheckChuyenVeCopy()
        {
            var arrPhoiVeCopy = ChuyenVeCopyGet();
            if (arrPhoiVeCopy.Count == 0)
                return false;
            return true;
        }
        public ActionResult _TabPhoiVeCopy()
        {
            var model = new List<PhoiVeCopyModel>();
            model = ChuyenVeCopyGet();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult ChuyenVePaste(int ChuyenDiId,int SoDoGheId)
        {
            var arrPhoiVeId = ChuyenVeCopyGet();
            if (arrPhoiVeId.Count == 0)
                return Loi();
            var arrphoive = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenDiId);
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            //kiem tra xem co du cho trong tren chuyen moi khong
            if(arrPhoiVeId.Count > (chuyendi.NguonVeInfo.loaixe.sodoghe.SoLuongGhe- chuyendi.SoNguoi))
            {
                return Loi(string.Format("Chuyến đi không đủ chỗ trống, bạn hãy kiểm tra lại (số lượng vé chuyển là: {0})", arrPhoiVeId.Count));
            }
            //Ok, se uu tien ghe copy dau tien cho SoDoGheId
            var arrSoDoGheId = new List<int>();
            arrSoDoGheId.Add(SoDoGheId);
            var sodoghequytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(chuyendi.NguonVeInfo.LoaiXeId);
            //tim tat ca vi tri con trong
            if(sodoghequytacs !=null && sodoghequytacs.Count > 0)
            {
                foreach(var s in sodoghequytacs)
                {
                    if(s.y >=1 && s.x >=1)
                    {
                        //xem tai vi tri nay co nguoi dat chua
                        bool isEmpty = true;
                        foreach(var pv in arrphoive)
                        {
                            //kiem tra thong tin dat ve da het han 2phut
                            if(pv.TrangThai == ENTrangThaiPhoiVe.DatCho && pv.NgayTao.AddSeconds(THOI_GIAN_GHE_DAT_CHO) < DateTime.Now)
                            {
                                continue;
                            }
                            if(pv.SoDoGheXeQuyTacId == s.Id)
                            {
                                isEmpty = false;
                                break;
                            }
                        }
                        if(isEmpty && !arrSoDoGheId.Contains(s.Id))
                        {
                            arrSoDoGheId.Add(s.Id);
                        }
                    }
                }
            }
            bool isHasErr = false;
            for (int i = 0; i < arrPhoiVeId.Count; i++)
            {
                bool isOK = false;
                while(!isOK)
                {
                    isOK = ChuyenVaPhoiVe(arrPhoiVeId[i].Id, ChuyenDiId, arrSoDoGheId[0]);
                    if(arrSoDoGheId.Count>0)
                    {
                        //loai bo vị tri ghe dau tien
                        arrSoDoGheId.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }
                //nếu hết vòng chuyển mà ko chuyen dc thi bao loi
                //cho nay se co 1 so ve chuyen dc, mot so ve khong chuyen dc
                if(!isOK)
                {
                    isHasErr = true;
                }
                if (arrSoDoGheId.Count == 0)
                    break;
            }
            ClearChonGeSession();
            Session["ChuyenVePhoiVeIds"] = null;
            if (isHasErr)
                return Loi("Có lỗi trong quá trình chuyển vé");
            return ThanhCong();
        }
        bool ChuyenVaPhoiVe(int PhoiVeId,int ChuyenDiId,int SoDoGheXeQuyTacId,bool isCopy=false)
        {
            var _phoiveitemnew = DatCho(ChuyenDiId, SoDoGheXeQuyTacId, isCopy);
            if (_phoiveitemnew == null)
                return false;
            //update thong tin old->new
            var _phoiveitemold = _phoiveService.GetPhoiVeById(PhoiVeId);
            if(isCopy)
            {
                var _phoiveitems = _phoiveService.GetPhoiVeDatChoBySession(GetChonGeSession(false));
                foreach(var _phoiveitem in _phoiveitems)
                {
                    _phoiveitem.isChonVe = _phoiveitemold.isChonVe;
                    _phoiveitem.GhiChu = _phoiveitemold.GhiChu;
                    _phoiveitem.CustomerId = _phoiveitemold.CustomerId;
                    _phoiveitem.TrangThaiId = _phoiveitemold.TrangThaiId;
                    _phoiveitem.ChangId = _phoiveitemold.ChangId;
                    _phoiveitem.DaiLyId = _phoiveitemold.DaiLyId;
                    _phoiveitem.GiaVeHienTai = _phoiveitemold.GiaVeHienTai;
                    var sodoghequytac = _xeinfoService.GetSoDoGheXeQuyTacById(_phoiveitem.SoDoGheXeQuyTacId);
                    _phoiveitem.sodoghexequytac = sodoghequytac;
                    _phoiveService.UpdatePhoiVe(_phoiveitem);
                }
            }
            else
            {
                _phoiveitemnew.isChonVe = _phoiveitemold.isChonVe;
                _phoiveitemnew.GhiChu = _phoiveitemold.GhiChu;
                _phoiveitemnew.CustomerId = _phoiveitemold.CustomerId;
                _phoiveitemnew.TrangThaiId = _phoiveitemold.TrangThaiId;
                _phoiveitemnew.ChangId = _phoiveitemold.ChangId;
                _phoiveitemnew.VeXeItemId = _phoiveitemold.VeXeItemId;
                _phoiveitemnew.MaVe = _phoiveitemold.MaVe;
                _phoiveitemnew.DaiLyId = _phoiveitemold.DaiLyId;
                _phoiveitemnew.GiaVeHienTai = _phoiveitemold.GiaVeHienTai;
                _phoiveitemnew.VeChuyenDenId = _phoiveitemold.Id;
                _phoiveitemnew.NguoiChuyenId = _workContext.CurrentNhanVien.Id;
                _phoiveitemnew.ViTriLenXe = _phoiveitemold.ViTriLenXe;
                _phoiveitemnew.ViTriXuongXe = _phoiveitemold.ViTriXuongXe;
                _phoiveService.UpdatePhoiVe(_phoiveitemnew);

                _phoiveitemold.TrangThaiId = (int)ENTrangThaiPhoiVe.Huy;
                _phoiveitemold.GhiChu = _phoiveitemold.GhiChu + string.Format("Lý do hủy: Chuyển sang chuyến đi mới (Id={0})", ChuyenDiId);
                _phoiveService.UpdatePhoiVe(_phoiveitemold);
                //luu lai nhat ky
                var phoivenote =new PhoiVeNote();
                phoivenote.Id=_phoiveitemnew.Id;
                var note = "HD" + _phoiveitemold.MaVe + " được chuyển sang " + _phoiveitemnew.MaVe + " lúc " + DateTime.Now.ToString() + " bởi " + string.Format("{0}{1}", _workContext.CurrentNhanVien.HoVaTen, _workContext.CurrentNhanVien.Email);
                phoivenote.Note=note;
                //_phoiveService.InsertPhoiVeNote(phoivenote);
                
            }
            return true;
        }
        private PhoiVe DatCho(int ChuyenDiId,int SoDoGheXeQuyTacId,bool isCopy=false)
        {
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            if(chuyendi == null)
            {
                return null;
            }
            //trang thai khong hop le
            if(chuyendi.TrangThai == ENTrangThaiXeXuatBen.KET_THUC || chuyendi.TrangThai == ENTrangThaiXeXuatBen.HUY)
            {
                return null;
            }
            //kiem tra xem co ai dat chua
            var checkphoive = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenDiId).Where(c => c.SoDoGheXeQuyTacId == SoDoGheXeQuyTacId).FirstOrDefault();
            if(checkphoive != null)
            {
                //neu co roi thi kiem tra thoi gian dat truoc do neu chua qua 2 phut thi moi out
                if(checkphoive.NgayTao.AddSeconds(THOI_GIAN_GHE_DAT_CHO) >DateTime.Now)
                {
                    //trong truoc hop dat ve, nhung paste thong tin thi bo qua cho dat nay, neu da dat roi
                    if (isCopy)
                        return checkphoive;
                    return null;
                }
                else
                {
                    //kiem tra trang thai ghe
                    if (checkphoive.TrangThai == ENTrangThaiPhoiVe.ChoXuLy)
                    {
                        //huy ghe nay di
                        _phoiveService.DeletePhoiVe(checkphoive);
                    }
                    else
                        return null;
                }
            }
            //tao thong tin phoi ve
            var phoive = new PhoiVe();
            phoive.ChuyenDiId = ChuyenDiId;
            phoive.SoDoGheXeQuyTacId = SoDoGheXeQuyTacId;
            phoive.NguoiDatVeId = _workContext.CurrentNhanVien.Id;
            phoive.NgayDi = chuyendi.NgayDi;
            phoive.TrangThaiId = (int)ENTrangThaiPhoiVe.ChoXuLy;
            phoive.NguonVeXeId = chuyendi.NguonVeId;
            phoive.SessionId = GetChonGeSession(true);
            phoive.GiaVeHienTai = chuyendi.NguonVeInfo.GiaVeHienTai;
            //_phoiveService.DatVe(phoive)
            if(_phoiveService.DatVe(phoive))
                return phoive;
            else
                return null;
        }
        private void ChuyenVeCopyDelete(int PhoiVeId)
        {
            var arrDatVeCopy = ChuyenVeCopyGet();
            var itemcopy = arrDatVeCopy.Where(c => c.Id == PhoiVeId).FirstOrDefault();
            if (itemcopy != null)
                arrDatVeCopy.Remove(itemcopy);
            Session["ChuyenVeDatVeIds"] = arrDatVeCopy;
        }
        [HttpPost]
        public ActionResult ChuyenVePasteThongTin(int ChuyenDiId,int SoDoGheId)
        {
            var arrPhoiVeId = ChuyenVeCopyGet();
            if (arrPhoiVeId.Count == 0)
                return Loi();
            var chuyendi = _nhaxeService.GetHistoryXeXuatBenId(ChuyenDiId);
            var arrphoive = _phoiveService.GetPhoiVeByChuyenDiId(ChuyenDiId);
            //kiem tra xem co du cho trong tren chuyen moi khong
            if (arrPhoiVeId.Count > (chuyendi.NguonVeInfo.loaixe.sodoghe.SoLuongGhe - chuyendi.SoNguoi))
            {
                return Loi(string.Format("Chuyến đi không đủ chỗ trống, bạn hãy kiểm tra lại (số lượng vé chuyển là: {0})", arrPhoiVeId.Count));
            }
            //Ok, se uu tien ghe copy dau tien de paste thong tin
            bool isOK = ChuyenVaPhoiVe(arrPhoiVeId[0].Id, ChuyenDiId, SoDoGheId, true);
            if (!isOK)
                return Loi("Có lỗi trong quá trình paste thông tin");
            return ThanhCong();
        }
        [HttpPost]
        public ActionResult ChuyenVeDelete(int PhoiVeId)
        {
            //xoa tung ve 
            if (PhoiVeId > 0)
            {
                ChuyenVeCopyDelete(PhoiVeId);
            }
            else if (PhoiVeId == -1)
            {
                //xóa ta ca ve da copy
                Session["ChuyenVeDatVeIds"] = null;
            }
            return ThanhCong();
        }
        #endregion
        //#region chuyen ve
        //private bool CheckChuyenVeCopy()
        //{
        //    var arrDatVeCopy = ChuyenVeCopyGet();
        //    if (arrDatVeCopy.Count == 0) return false;
        //    return true;
        //}
        //private List<DatVeCopyModel> ChuyenVeCopyGet()
        //{
        //    var arrDatVeCopy = new List<DatVeCopyModel>();
        //    if (Session["ChuyenVeDatVeIds"] != null)
        //    {
        //        arrDatVeCopy = (List<DatVeCopyModel>)Session["ChuyenVeDatVeIds"];
        //    }
        //    return arrDatVeCopy;
        //}
        //private void ChuyenVeCopyAdd(DatVe item)
        //{
        //    var arrDatVeCopy = ChuyenVeCopyGet();
        //    var itemcopy = new DatVeCopyModel();
        //    itemcopy.Id = item.Id;
        //    itemcopy.Ma = item.Ma;
        //    if (!arrDatVeCopy.Where(c => c.Id == itemcopy.Id).Any())
        //    {
        //        arrDatVeCopy.Add(itemcopy);
        //    }
        //    Session["ChuyenVeDatVeIds"] = arrDatVeCopy;
        //}
        //private void ChuyenVeCopyDelete(int DatVeId)
        //{
        //    var arrDatVeCopy = ChuyenVeCopyGet();
        //    var itemcopy = arrDatVeCopy.Where(c => c.Id == DatVeId).FirstOrDefault();
        //    if (itemcopy != null)
        //        arrDatVeCopy.Remove(itemcopy);
        //    Session["ChuyenVeDatVeIds"] = arrDatVeCopy;
        //}
        //[HttpPost]
        //public ActionResult ChuyenVeCopy(string DatVeIds, int ChuyenDiId)
        //{

        //    if (DatVeIds == "-1")
        //    {
        //        //copy ta ca ve trong chuyen di
        //        var chuyendi = _limousinebanveService.GetChuyenDiById(ChuyenDiId);
        //        var datves = chuyendi.DatVeHopLes();
        //        foreach (var dv in datves)
        //        {
        //            ChuyenVeCopyAdd(dv);
        //        }

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(DatVeIds))
        //            return Loi();
        //        var arrDatVeId = DatVeIds.Split(',');
        //        foreach (var s in arrDatVeId)
        //        {
        //            //copy tung ve 
        //            var datve = _limousinebanveService.GetDatVeById(Convert.ToInt32(s));
        //            if (datve == null)
        //            {
        //                return Loi();
        //            }
        //            //datve thai khong hop le
        //            if (datve.trangthai != ENTrangThaiDatVe.DA_XEP_CHO)
        //            {
        //                return Loi();
        //            }
        //            ChuyenVeCopyAdd(datve);
        //        }

        //    }
        //    return ThanhCong();
        //}

        //[HttpPost]
        //public ActionResult ChuyenVeDelete(int DatVeId)
        //{
        //    //xoa tung ve 
        //    if (DatVeId > 0)
        //    {
        //        ChuyenVeCopyDelete(DatVeId);
        //    }
        //    else if (DatVeId == -1)
        //    {
        //        //xóa ta ca ve da copy
        //        Session["ChuyenVeDatVeIds"] = null;
        //    }
        //    return ThanhCong();
        //}
        //[HttpPost]
        //public ActionResult ChuyenVePaste(int ChuyenDiId, int SoDoGheId)
        //{
        //    var arrDatVeId = ChuyenVeCopyGet();
        //    if (arrDatVeId.Count == 0)
        //        return Loi();
        //    var chuyendi = _limousinebanveService.GetChuyenDiById(ChuyenDiId);
        //    var arrdatve = chuyendi.DatVes.Where(c => c.trangthai != ENTrangThaiDatVe.HUY);
        //    //kiem tra xem co du cho trong tren chuyen di nay khong
        //    if (arrDatVeId.Count > chuyendi.lichtrinhloaixe.loaixe.sodoghe.SoLuongGhe - arrdatve.Count())
        //    {
        //        return Loi(string.Format("Chuyến đi không đủ chỗ trống, bạn hãy kiểm tra lại (số lượng vé chuyển là: {0})", arrDatVeId.Count));
        //    }
        //    //OK, se uu tien ghe copy dau tien cho SoDoGheId
        //    var arrSoDoGheId = new List<int>();
        //    arrSoDoGheId.Add(SoDoGheId);

        //    var sodoghequytacs = _xeinfoService.GetAllSoDoGheXeQuyTac(chuyendi.lichtrinhloaixe.LoaiXeId);
        //    //tim tat ca vi tri con trong
        //    if (sodoghequytacs != null && sodoghequytacs.Count > 0)
        //    {
        //        foreach (var s in sodoghequytacs)
        //        {
        //            if (s.y >= 1 && s.x >= 1)
        //            {
        //                //xem tai vi tri nay co nguoi dat chua
        //                bool isEmpty = true;
        //                foreach (var dv in arrdatve)
        //                {
        //                    //kiem tra thong tin ve da het han 2 phut
        //                    if (dv.trangthai == ENTrangThaiDatVe.MOI && dv.NgayTao.AddSeconds(THOI_GIAN_GHE_DAT_CHO) < DateTime.Now)
        //                    {
        //                        continue;
        //                    }
        //                    if (dv.SoDoGheId == s.Id)
        //                    {
        //                        isEmpty = false;
        //                        break;
        //                    }
        //                }
        //                if (isEmpty && !arrSoDoGheId.Contains(s.Id))
        //                {
        //                    arrSoDoGheId.Add(s.Id);

        //                }
        //            }
        //        }
        //    }
        //    bool isHasErr = false;
        //    for (int i = 0; i < arrDatVeId.Count; i++)
        //    {
        //        bool isOK = false;
        //        while (!isOK)
        //        {
        //            isOK = ChuyenVaDatVe(arrDatVeId[i].Id, ChuyenDiId, arrSoDoGheId[0]);
        //            if (arrSoDoGheId.Count > 0)
        //            {
        //                //loai bo vi tri ghe dau tien
        //                arrSoDoGheId.RemoveAt(0);
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        //neu het vong chuyen ma ko chuyen dc thi bao loi
        //        //cho nay se co mot so ve chuyen dc, mot so ve khong chuyen dc
        //        if (!isOK)
        //        {
        //            isHasErr = true;
        //        }
        //        if (arrSoDoGheId.Count == 0)
        //            break;
        //    }
        //    clearDatGheSession();
        //    Session["ChuyenVeDatVeIds"] = null;
        //    if (isHasErr)
        //        return Loi("Có lỗi trong quá trình chuyển vé");
        //    return ThanhCong();
        //}
        //[HttpPost]
        //public ActionResult ChuyenVePasteThongTin(int ChuyenDiId, int SoDoGheId)
        //{
        //    var arrDatVeId = ChuyenVeCopyGet();
        //    if (arrDatVeId.Count == 0)
        //        return Loi();
        //    var chuyendi = _limousinebanveService.GetChuyenDiById(ChuyenDiId);
        //    var arrdatve = chuyendi.DatVes.Where(c => c.trangthai != ENTrangThaiDatVe.HUY);
        //    //kiem tra xem co du cho trong tren chuyen di nay khong
        //    if (arrDatVeId.Count > chuyendi.lichtrinhloaixe.loaixe.sodoghe.SoLuongGhe - 1)
        //    {
        //        return Loi("Chuyến đi không đủ chỗ trống, bạn hãy kiểm tra lại sơ đồ");
        //    }
        //    //OK,
        //    //lay ve dau tien de paste thong tin
        //    bool isOK = ChuyenVaDatVe(arrDatVeId[0].Id, ChuyenDiId, SoDoGheId, true);
        //    if (!isOK)
        //        return Loi("Có lỗi trong quá trình đặt thông tin vé");
        //    return ThanhCong();
        //}
        //bool ChuyenVaDatVe(int DatVeId, int ChuyenDiId, int SoDoGheId, bool isCopy = false)
        //{
        //    var _datveitemnew = DatCho(ChuyenDiId, SoDoGheId, isCopy);
        //    if (_datveitemnew == null)
        //    {
        //        return false;
        //    }
        //    //update thong tin old- > new
        //    var _datveitemold = _limousinebanveService.GetDatVeById(DatVeId);
        //    if (isCopy)
        //    {
        //        var _datveitems = _limousinebanveService.GetDatVeBySession(_workContext.NhaXeId, ChuyenDiId, getDatGheSession);
        //        foreach (var _datveitem in _datveitems)
        //        {
        //            _datveitem.isDonTaxi = _datveitemold.isDonTaxi;
        //            _datveitem.DiaChiNha = _datveitemold.DiaChiNha;
        //            _datveitem.GhiChu = _datveitemold.GhiChu;
        //            _datveitem.DiemDonId = _datveitemold.DiemDonId;
        //            _datveitem.KhachHangId = _datveitemold.KhachHangId;
        //            _datveitem.isThanhToan = _datveitemold.isThanhToan;
        //            _datveitem.isNoiBai = _datveitemold.isNoiBai;
        //            _datveitem.trangthai = _datveitemold.trangthai;
        //            _datveitem.isLenhDonTaXi = _datveitemold.isLenhDonTaXi;
        //            _datveitem.MaTaXi = _datveitemold.MaTaXi;
        //            _datveitem.isDaXacNhan = _datveitemold.isDaXacNhan;
        //            _datveitem.TenDiemDon = _datveitemold.TenDiemDon;
        //            _datveitem.TenDiemTra = _datveitemold.TenDiemTra;
        //            _datveitem.GiaTien = _datveitemold.GiaTien;
        //            _datveitem.TenKhachHangDiKem = _datveitemold.TenKhachHangDiKem;
        //            _limousinebanveService.UpdateDatVe(_datveitem);
        //        }

        //    }
        //    else
        //    {
        //        _datveitemnew.isDonTaxi = _datveitemold.isDonTaxi;
        //        _datveitemnew.DiaChiNha = _datveitemold.DiaChiNha;
        //        _datveitemnew.GhiChu = _datveitemold.GhiChu;
        //        _datveitemnew.DiemDonId = _datveitemold.DiemDonId;
        //        _datveitemnew.KhachHangId = _datveitemold.KhachHangId;
        //        _datveitemnew.isThanhToan = _datveitemold.isThanhToan;
        //        _datveitemnew.isNoiBai = _datveitemold.isNoiBai;
        //        _datveitemnew.trangthai = _datveitemold.trangthai;
        //        _datveitemnew.isLenhDonTaXi = _datveitemold.isLenhDonTaXi;
        //        _datveitemnew.MaTaXi = _datveitemold.MaTaXi;
        //        _datveitemnew.isDaXacNhan = _datveitemold.isDaXacNhan;
        //        _datveitemnew.TenDiemDon = _datveitemold.TenDiemDon;
        //        _datveitemnew.TenDiemTra = _datveitemold.TenDiemTra;
        //        _datveitemnew.VeChuyenDenId = _datveitemold.Id;
        //        _datveitemnew.TenKhachHangDiKem = _datveitemold.TenKhachHangDiKem;
        //        //add by lent, trong truong hop chuyen tu hanh trinh khac sang hanh trinh moi, thi van luu hanh trinh nhu cu
        //        _datveitemnew.GiaTien = _datveitemold.GiaTien;
        //        _datveitemnew.HanhTrinhId = _datveitemold.HanhTrinhId;
        //        _datveitemnew.LichTrinhId = _datveitemold.LichTrinhId;

        //        _limousinebanveService.UpdateDatVe(_datveitemnew);
        //        _datveitemold.trangthai = ENTrangThaiDatVe.HUY;
        //        _datveitemold.GhiChu = _datveitemold.GhiChu + string.Format("Lý do hủy: Chuyển sang chuyến đi mới (Id={0})", ChuyenDiId);
        //        _limousinebanveService.UpdateDatVe(_datveitemold);
        //        // luu lai nhat ky
        //        var note = "HD " + _datveitemold.Ma + " được chuyển sang " + _datveitemnew.chuyendi.Ma + " lúc " + _datveitemnew.chuyendi.NgayDiThuc + " bởi " + _datveitemnew.nguoitao.HoVaTen;
        //        _limousinebanveService.InsertDatVeNote(_datveitemold.Id, note);
        //    }



        //    return true;
        //}
        //[HttpPost]
        //public ActionResult ChuyenVe(int DatVeId, int ChuyenDiId, int SoDoGheId)
        //{
        //    if (this.CheckNoAccessIntoNhaXe(_workContext, _permissionService, StandardPermissionProvider.CVHoatDongBanVe))
        //        return AccessDeniedView();
        //    //kiem tra vi tri hien tai con trong ko ?
        //    if (!ChuyenVaDatVe(DatVeId, ChuyenDiId, SoDoGheId))
        //        return Loi();
        //    clearDatGheSession();
        //    return ThanhCong();
        //}
        //#endregion
    }
}