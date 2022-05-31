using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Data.Tables
{
    public class DoiTac
    {
        [Key]
        [Column(Order = 1)]
        public long Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TongCongNo { get; set; }


        [Required]
        public string TenCongTy { get; set; }
        [Required]
        public string  MaDoiTac { get; set; } //Để dẽ quản lý và tìm kiếm
        public string SanPhamDichVu { get; set; }
        [Required]
        public string MaSoThue { get; set; }
        [Required]
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        [Required]
        public string Slug { get; set; }


        public Statuses Status { get; set; }



    }
}
