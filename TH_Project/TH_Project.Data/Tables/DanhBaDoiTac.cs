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
    public class DanhBaDoiTac
    {


    [Key]
    [Column(Order = 0)]
    public long Id { get; set; }


        [ForeignKey("DoiTac")]
        public long IdDoiTac  {get;set;} 

     public string XungHo {get;set;}
    [Required]
     public string Ho {get;set;}
    [Required]
    public string Ten {get;set;}
     public string ChucVu {get;set;}
     [Required]
     public string DiDong {get;set;}
     public string Mail {get;set;}
     public string GhiChu {get;set;}
        [Required]
        public string Slug { get; set; }
        [Required]
    public Statuses Status {get;set;}

    public virtual DoiTac DoiTac { get; set; }

    }
}
