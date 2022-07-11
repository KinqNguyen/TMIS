using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class DatHangRequest : PageFilter
    {
        public string Keyword { get; set; }


        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
        public long? IdDoiTac { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }


    }
    public class DatHangResult {
        public long Id { get; set; }
        public long IdDoiTac { get; set; }
        public long IdNhanVien { get; set; }
        public string MaDonHang { get; set; } //(SO0001)
        public string TenCongTrinh { get; set; }
        public string NoiDung { get; set; }
        public int GiaTri { get; set; }
        public string Slug { get; set; }

        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayGiaoHang { get; set; }
        public string GhiChu { get; set; }
        public OrderStatuses TrangThai { get; set; }
        public Statuses Status { get; set; }
    }


    public class DatHangCreate
    {
        public long? IdDoiTac { get; set; }
        public long? IdNhanVien { get; set; }
        public string TenCongTrinh { get; set; }
        public string NoiDung { get; set; }
        public int? GiaTri { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayToiHan { get; set; }
        
        public string Slug { get; set; }

        public DateTime? NgayGiaoHang { get; set; }
        public string GhiChu { get; set; }
        public OrderStatuses? TrangThai { get; set; }

    }

    public class DatHangEdit
    {
        public long? IdDoiTac { get; set; }
        public long? IdNhanVien { get; set; }
        public string MaDonHang { get; set; } //(SO0001)

        public string TenCongTrinh { get; set; }
        public string NoiDung { get; set; }
        public int? GiaTri { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public string Slug { get; set; }

        public DateTime? NgayGiaoHang { get; set; }
        public string GhiChu { get; set; }
        public OrderStatuses? TrangThai { get; set; }
    }
}
