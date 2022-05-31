using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TH_Project.Data.Enums;

namespace TH_Project.Data.Tables
{
   public  class DonHang
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey("DoiTac")]
        public long IdDoiTac { get; set; }

        [ForeignKey("NhanVien")]
        public long IdNhanVien { get; set; }


        [Required]
        public string MaDonHang { get; set; } //(SO0001)

        [Required]
        public string TenCongTrinh { get; set; }
        public string NoiDung {get;set;}

        [Required]
        [Range(0, int.MaxValue)]
        public int  GiaTri {get;set;}
        
        [Required]
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayGiaoHang { get; set; }
        [Required]
        public DateTime? NgayToiHan { get; set; }
        [Required]
        public string Slug { get; set; }

        public string  GhiChu {get;set;}

        public OrderStatuses TrangThai { get; set; }
        public Statuses Status { get;set;}

        public virtual NhanVien NhanVien { get; set; }
        public virtual DoiTac DoiTac { get; set; }

    }

}
