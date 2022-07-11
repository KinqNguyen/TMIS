using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class DoiTacRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
        public long? IdDoiTac { get; set; }


    }
    public class DoiTacResult
    {
        public long Id { get; set; }
        public int TongCongNo { get; set; }
        public string TenCongTy { get; set; } //(SO0001)
        public string MaDoiTac { get; set; }
        public string SanPhamDichVu { get; set; }
        public string MaSoThue { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public Statuses Status { get; set; }
    }


    public class DoiTacEdit
    {
        public string TenCongTy { get; set; }
        public string MaDoiTac { get; set; } //Để dẽ quản lý và tìm kiếm
        public string SanPhamDichVu { get; set; }
        public string MaSoThue { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public string Slug { get; set; }
    }


}
