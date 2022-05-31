using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class CongNoRequest : PageFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IOStatuses? LoaiCongNo { get; set; }
        public Statuses Status { get; set; }
        public long? IdHoaDon { get; set; }
            }

    public class CongNoResult
    {
        public long Id { get; set; }
        public long IdHoaDonFake { get; set; }
        public long IdHoaDon { get; set; }
        public string MaCongNo { get; set; } //(SO0001)
        public string TenCongTrinh { get; set; }
        public string NoiDung { get; set; }
        public string MaHoaDon { get; set; }
        public int GiaTri { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayToiHan { get; set; } //Ngày tới hạn là ngày cho đối tác nợ
        public DateTime? NgayKetThuc { get; set; } // Ngày kết thúc là ngày mà cả 2 bên giao nhận và trả tiền thành công
        public OrderStatuses TrangThai { get; set; }
        public string GhiChu { get; set; }
        public Statuses Status { get; set; }    }


    public class CongNoEdit
    {
        public long? IdHoaDon { get; set; }
        public string MaCongNo { get; set; } //(SO0001)
        public string TenCongTrinh { get; set; }
        public string NoiDung { get; set; }
        public int GiaTri { get; set; }
        public string Slug { get; set; }
        public DateTime? NgayToiHan { get; set; } //Ngày tới hạn là ngày cho đối tác nợ
        public DateTime? NgayKetThuc { get; set; } // Ngày kết thúc là ngày mà cả 2 bên giao nhận và trả tiền thành công
        public OrderStatuses TrangThai { get; set; }
        public string GhiChu { get; set; }
  }

    public class CongNoCreate
    {
        public long IdHoaDon { get; set; }
        public string MaCongNo { get; set; } //(SO0001)
        public string TenCongTrinh { get; set; }
        public string NoiDung { get; set; }
        public int GiaTri { get; set; }
        public string Slug { get; set; }

        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayToiHan { get; set; } //Ngày tới hạn là ngày cho đối tác nợ
        public DateTime? NgayKetThuc { get; set; } // Ngày kết thúc là ngày mà cả 2 bên giao nhận và trả tiền thành công
        public OrderStatuses TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string MaHoaDon { get; set; }
    }
}
