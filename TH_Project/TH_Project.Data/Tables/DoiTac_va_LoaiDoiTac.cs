using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH_Project.Data.Tables
{
   public class DoiTac_va_LoaiDoiTac
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("LoaiDoiTac")]
        public long IdLoaiDoiTac { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("DoiTac")]
        public long IdDoiTac { get; set; }

        public virtual LoaiDoiTac LoaiDoiTac { get; set; }
        public virtual DoiTac DoiTac { get; set; }

    }
}
