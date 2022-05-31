using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TH_Project.Service.DTOs
{
    public class TokenRequest
    { 
        public string Token { get; set; }
        public bool IsMonitor { get; set; } = false;
    }

    public class AuthResult
    {
        public long TokenStoredId { get; set; }
        public DateTime DataTokenExpired { get; set; }
        public long Id { get; set; }
        // public string Name { get; set; }
        // public long RoleID { get; set; }
        [ForeignKey("WareHouse")]
        public long? WareHouseID { get; set; }
        public string Token { get; set; }
        // public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string TokenExpired { get => DataTokenExpired.ToString("o"); }
    }
    
    public class LoginMonitorParams
    {
        [Required]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        // internal DateTime DataTokenExpired { get; set; }

        // public string TokenExpired { get => DataTokenExpired.ToString("o"); }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }

    /// <summary>
    /// Cho nhân viên đăng nhập
    /// </summary>
    public class ChangePasswordParams
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string NewPassword { get; set; }
    }


}
