﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Data.Tables
{
    public class NhanVien
    {
        [Key]
        [Column(Order = 1)]
        public long Id {get; set;}


        [ForeignKey("ViTriNhanVien")]
        public long IdViTri { get; set; }

       public string Ho {get; set;}
       public string Ten { get; set; }
       public string BietHieu {get; set;}

       public DateTime? NgaySinh {get; set;}
       public string DiaChi {get; set;}
       
       [Required]
       public string DienThoai { get; set; }
       
        public DateTime NgayBatDau {get; set;}

       public DateTime? NgayNghiViec {get; set;}
        public long RoleId { get; set; }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public Statuses Status { get; set;}
        [Required]
        public string Slug { get; set; }
        public virtual ViTriNhanVien ViTriNhanVien { get; set; }
    }
}
