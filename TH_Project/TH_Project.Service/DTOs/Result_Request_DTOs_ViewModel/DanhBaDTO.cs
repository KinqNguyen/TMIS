using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class DanhBaRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
        public long? IdDoiTac { get; set; }


    }
    public class DanhBaResult
    {
        public long Id { get; set; }
        public long IdDoiTac { get; set; }
        public long IdNhanVien { get; set; }
        public string TenCongTyDoiTac { get; set; } //(SO0001)
        public string TenNhanVien { get; set; } //(SO0001)
        public string XungHo { get; set; }
        public string ViTriNhanVien { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string ChucVu { get; set; }
        public string GhiChu { get; set; }
        public string DiDong { get; set; }
        public string Mail { get; set; }
        public Statuses Status { get; set; }


    }


    public class DanhBaNhanVienEdit
    {
        public long? IdNhanVien { get; set; }
        public string Mail { get; set; }
        public string GhiChu { get; set; }
        public string DiDong { get; set; }
        public string Slug { get; set; }
        public Statuses Status { get; set; }


    }

    public class DanhBaNhanVienCreate
    {
        public long? IdNhanVien { get; set; }
        public string Mail { get; set; }
        public string GhiChu { get; set; }
        public string DiDong { get; set; }
        public string Slug { get; set; }
        public Statuses Status { get; set; }


    }

    public class DanhBaDoiTacCreate
    {
        public long? IdDoiTac { get; set; }
        public string XungHo { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string ChucVu { get; set; }
        public string GhiChu { get; set; }
        public string Slug { get; set; }

        public string DiDong { get; set; }
        public string Mail { get; set; }
        public Statuses Status { get; set; }


    }

    public class DanhBaDoiTacEdit
    {
        public long? IdDoiTac { get; set; }
        public string XungHo { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string ChucVu { get; set; }
        public string GhiChu { get; set; }
        public string Slug { get; set; }

        public string DiDong { get; set; }
        public string Mail { get; set; }
        public Statuses Status { get; set; }


    }
}
