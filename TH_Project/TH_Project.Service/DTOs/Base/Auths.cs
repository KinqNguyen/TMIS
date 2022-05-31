using Stump.Api.Data.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace TH_Project.Service.DTOs.Base
{
    public class LoginParams
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class ChangeAccountParams
    {
        [Required]
        public string OldLoginName { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [StringLength(24, MinimumLength = 6)]
        public string NewLoginName { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }

    public class LoginResponse
    {
        internal DateTime DataTokenExpired { get; set; }
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long? WareHouseId { get; set; }
        public string Token { get; set; }
        public string TokenExpired { get => DataTokenExpired.ToFullDateString(); }
    }

    public class GetMeData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long RoleId { get; set; }
        public long? WareHouseId { get; set; }
    }
}
