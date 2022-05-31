using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class TaiKhoanNganHangRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
        public long? IdDoiTac { get; set; }


    }

    public class TaiKhoanNganHangNhanVienRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
    }

    public class TaiKhoanNganHangDoiTacRequest : PageFilter
    {
        public string Keyword { get; set; }
        public Statuses Status { get; set; }
        public long? IdDoiTac { get; set; }
    }


    public class TaiKhoanNganHangResult
    {
        public long Id { get; set; }
        public long? IdNhanVIen { get; set; }
        public string HoTenNhanVien { get; set; } //(SO0001)
        public long? IdDoiTac { get; set; }
        public string HoTenDoiTac { get; set; } //(SO0001)
        public string STK { get; set; }
        public string Ten { get; set; }
        public string DiaChiNH { get; set; }
        public string GhiChu { get; set; }
        public Statuses Status { get; set; }
        public string Slug { get; set; }
    }

    public class TaiKhoanNganHangNhanVienResult
    {
        public long Id { get; set; }
        public string HoTenNhanVien { get; set; } //(SO0001)
        public long? IdNhanVien { get; set; }
        public long? IdNhanVienTrue { get; set; }
        public string STK { get; set; }
        public string Ten { get; set; }
        public string DiaChiNH { get; set; }
        public string GhiChu { get; set; }
        public Statuses Status { get; set; }
        public string Slug { get; set; }
    }


    public class TaiKhoanNganHangDoiTacResult
    {
        public long Id { get; set; }
        public string TenCongTyDoiTac { get; set; } //(SO0001)
        public long? IdDoiTac { get; set; }
        public long? IdDoiTacTrue { get; set; }
        public string STK { get; set; }
        public string Ten { get; set; }
        public string DiaChiNH { get; set; }
        public string GhiChu { get; set; }
        public Statuses Status { get; set; }
        public string Slug { get; set; }
    }

}
