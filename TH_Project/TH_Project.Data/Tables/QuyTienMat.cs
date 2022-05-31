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
    public class QuyTienMat
    {
        [Key]
        [Column(Order = 0)]
        public long Id {get;set;}

        public long? IdHoaDon { get; set; }

        [ForeignKey("NhanVien")]
        public long IdNhanVien { get; set; } //Nguoi thuc hien

        [Required]
        public DateTime? Ngay {get;set;}

        [Required]
        public string Slug { get; set; }
        public string   NoiDung {get;set;}

        public IOStatuses IOStatuses { get;set;} //in out status
        public Statuses  Status {get;set;}

        public virtual NhanVien NhanVien { get; set; }

    }
}
