using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel
{
   public class NhanVienRequest : PageFilter
    {
        public string Keyword { get; set; }

        public Statuses Status { get; set; }

        public long? IdNhanVien { get; set; }
        public long? IdDoiTac { get; set; }


    }
    public class NhanVienResult
    {

        public long Id { get; set; }
        public long IdViTri { get; set; }
        public long RoleId { get; set; }

        public string Ho { get; set; }
        public string Ten { get; set; }
        public string BietHieu { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime? NgayNghiViec { get; set; }

        public string LoginName { get; set; }
        public string ViTriNhanVien { get; set; }
        public string HoTen { get; set; }
        public string Password { get; set; }
        public Statuses Status { get; set; }

    }

    public class NhanVienEdit
    {
        public long? IdViTri { get; set; }
        public long RoleId { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string BietHieu { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Slug { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayNghiViec { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

    }

}
