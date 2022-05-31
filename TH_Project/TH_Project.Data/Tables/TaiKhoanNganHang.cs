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
    public class TaiKhoanNganHang
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }
        public long? IdDoiTac { get; set; }
        public long? IdNhanVIen { get; set; }

        [Required]
        public string STK { get; set; }
        [Required]
        public string Ten { get; set; }
        public string DiaChiNH { get; set; }
        public string GhiChu { get; set; }
        public Statuses Status { get; set; }
        [Required]
        public string Slug { get; set; }


    }
}
