using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class HoaDonRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdDatHang { get; set; }
        public long? IdDonHang { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }


    }

    public class HoaDon_DonHangRequest : PageFilter
    {
        public string Keyword { get; set; }
        public Statuses Status { get; set; }
        public long? IdDonHang { get; set; }

    }

    public class HoaDon_DatHangRequest : PageFilter
    {
        public string Keyword
        {
            get; set;
        }
        public Statuses Status { get; set; }
        public long? IdDatHang { get; set; }

    }


    public class HoaDonResult
    {
        public long Id { get; set; }
        public long? IdDonHang { get; set; }
        public long? IdDonHangTrue { get; set; }
        public long? IdDatHangTrue { get; set; }
        public long? IdDatHang { get; set; }
        public string MaDonHang { get; set; } //(SO0001)
        public string MaDatHang { get; set; }
        public string SoHD { get; set; }
        public int GiaTri { get; set; }
        public DateTime? Ngay { get; set; }
        public string GhiChu { get; set; }
        public BillTypes LoaiHoaDon
        {
            get; set;
        }
        public Statuses Status { get; set; }

        public BillStatuses BillStatus
        {
            get; set;
        }

    }

    public class HoaDonEdit
    {
        public long? IdDonHang { get; set; }
        public long? IdDatHang { get; set; }
        public string SoHD { get; set; }
        public string Slug { get; set; }
        public int GiaTri { get; set; }
        public DateTime Ngay { get; set; }
        public string GhiChu { get; set; }
        public BillStatuses BillStatus
        {
            get; set;
        }

    }


    public class HoaDon_DonHangResult
    {
        public long Id { get; set; }
        public long? IdDonHang { get; set; }
        public long? IdDonHangTrue { get; set; }
        public string MaDonHang { get; set; } //(SO0001)
        public string SoHD { get; set; }
        public int GiaTri { get; set; }
        public DateTime? Ngay { get; set; }
        public string GhiChu { get; set; }
        public BillTypes LoaiHoaDon
        {
            get; set;
        }
        public Statuses Status { get; set; }

        public BillStatuses BillStatus
        {
            get; set;
        }
    }

   public class HoaDon_DatHangResult
    {
        public long Id { get; set; }
        public long? IdDatHangTrue { get; set; }
        public long? IdDatHang { get; set; }
        public string MaDatHang { get; set; }
        public string SoHD { get; set; }
        public int GiaTri { get; set; }
        public DateTime? Ngay { get; set; }
        public string GhiChu { get; set; }
        public BillTypes LoaiHoaDon
        {
            get; set;
        }
        public Statuses Status { get; set; }

        public BillStatuses BillStatus
        {
            get; set;
        }

    }

}
