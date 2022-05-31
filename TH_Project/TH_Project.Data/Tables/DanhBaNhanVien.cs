using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data.Enums;

namespace TH_Project.Data.Tables
{
    public class DanhBaNhanVien
    {
      [Key]
      [Column(Order = 0)]
      public long Id { get; set; }

      [ForeignKey("NhanVien")]
      public long  IdNhanVien {get;set;}
      [Required]
      public string DiDong {get;set;}

      public string  Mail {get;set;}
      public string GhiChu {get;set;}
        [Required]
        public string Slug { get; set; }
        [Required]
      [DefaultValue(Statuses.Default)]
      public Statuses  Status {get;set;}
      public virtual NhanVien NhanVien { get; set; }

    }
}
