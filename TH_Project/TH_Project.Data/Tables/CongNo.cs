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
    public class CongNo
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey("HoaDon")]
        public long IdHoaDon { get; set; }

        public string MaCongNo { get; set; } //(SO0001)


        public string TenCongTrinh { get; set; }

        [Required]
        public string Slug { get; set; }


        [Required]
        public string NoiDung { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GiaTri { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? NgayBatDau { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? NgayToiHan { get; set; } //Ngày tới hạn là ngày cho đối tác nợ
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? NgayKetThuc { get; set; } // Ngày kết thúc là ngày mà cả 2 bên giao nhận và trả tiền thành công

        [DefaultValue(OrderStatuses.Default)]
        public OrderStatuses TrangThai { get; set; }
        public string GhiChu { get; set; }
        [DefaultValue(Statuses.Default)]
        public Statuses Status { get; set; }

        public virtual HoaDon HoaDon { get; set; }

    }
}
