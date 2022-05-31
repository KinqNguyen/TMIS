using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;
using TH_Project.Data.Tables;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class QuyTienMatRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
        public long? IdHoaDon { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IOStatuses IOStatuses { get; set; } //in out status


    }
    public class QuyTienMatResult
    {
        public long Id { get; set; }
        public long? IdHoaDon { get; set; }
        public long? IdHoaDonTrue { get; set; }
        public long? IdNhanVien { get; set; } //Nguoi thuc hien
        public long? IdNhanVienTrue { get; set; } //Nguoi thuc hien
        public string SoHoaDon { get; set; }
        public string TenCongTy { get; set; }
        public string HoTenNhanVien { get; set; }
        public DateTime? Ngay { get; set; }
        public string Slug { get; set; }
        public string NoiDung { get; set; }
        public IOStatuses IOStatuses { get; set; } //in out status
        public Statuses Status { get; set; }
    }
}
