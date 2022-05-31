using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Stump.Api.Utils
{
    public static class AuthUtils
    {
        /// <summary>
        /// Tạo token
        /// </summary>
        /// <param name="userID">Mã người dùng</param>
        /// <param name="roleID">Mã chức vụ</param>
        /// <param name="key">Khóa</param>
        /// <param name="expires">Thời gian hết hạn</param>
        /// <returns></returns>
        public static JwtSecurityToken CreateToken(string key, DateTime expires)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
                expires: expires,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
