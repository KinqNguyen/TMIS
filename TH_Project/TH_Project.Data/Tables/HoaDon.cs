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
    public class HoaDon
    {
        [Key]
        public long Id{get; set;}

        public long? IdDonHang{get; set;}

        public long? IdDatHang{get; set;}

        public string SoHD{get; set;}
        [Required]
        public string Slug { get; set; }

        public DateTime Ngay{get; set;}



        [Required]
        [Range(0, int.MaxValue)]
        public int GiaTri { get; set; }

        public string GhiChu{get; set;}

        public BillTypes LoaiHoaDon { get; set;}

        public Statuses Status{ get; set; }
        public BillStatuses BillStatus{ get; set;}


    }
}
