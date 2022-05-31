using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class PhieuGiaoHangRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdDonHang { get; set; }
    }


    public class PhieuGiaoHangResult
    {
        public long Id { get; set; }
        public long IdDonHang { get; set; }
        public string MaDonHang { get; set; }
        public string SoPhieuGH { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? NgayGiao { get; set; }
        public Statuses Status { get; set; }
        public string GhiChu { get; set; }
        public string Slug { get; set; }
    }
}
