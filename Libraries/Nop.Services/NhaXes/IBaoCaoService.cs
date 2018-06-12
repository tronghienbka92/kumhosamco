using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Chonves;
using System;

namespace Nop.Services.NhaXes
{
    public partial interface IBaoCaoService
    {
        #region Xe xuat ben
        List<HistoryXeXuatBen> GetXeXuatBens(int NhaXeId, int XeVanChuyenId = 0, int[] laiphuxeids = null, DateTime? TuNgay = null, DateTime? DenNgay = null,int[] hanhtrinhIds = null);
        #endregion
        #region Phoi ve
        Decimal GetTongDoanhThuChuyenDi(int[] NguoVeIds, DateTime? TuNgay = null, DateTime? DenNgay = null);
        #endregion
       
    }
}
