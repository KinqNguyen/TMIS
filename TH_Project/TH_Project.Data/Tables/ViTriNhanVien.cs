using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH_Project.Data.Tables
{
    public class ViTriNhanVien
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string TenViTri { get; set; }

        [Required]
        public string Slug { get; set; }
    }
}
