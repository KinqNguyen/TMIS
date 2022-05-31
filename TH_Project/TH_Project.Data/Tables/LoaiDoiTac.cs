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
   public class LoaiDoiTac
    {
        [Key]
        public long Id { get; set; }
        public string TenLoaiDoiTac { get; set; } //(SO0001)
        [Required]
        public string Slug { get; set; }
        public Statuses Status
        {
            get; set;
        }

    }
}
