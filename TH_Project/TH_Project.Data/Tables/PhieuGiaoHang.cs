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
    public class PhieuGiaoHang
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey("DonHang")]
        public long  IdDonHang {get; set;}
       public string SoPhieuGH { get; set; }
        public DateTime? NgayLap { get; set; }

        public DateTime? NgayGiao {get; set;}
       public Statuses  Status {get; set;}
       public string  GhiChu {get; set;}
        [Required]
        public string Slug { get; set; }

        public virtual DonHang DonHang { get; set; }



    }
}
