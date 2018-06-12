using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.ChuyenPhatNhanh;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class DaiLyModel : BaseNopEntityModel
    {
        public int NhaXeId { get; set; }
        public string TenDaiLy { get; set; }

    }
}