 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens; 

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Stump.Api.Utils;
using TH_Project.Service.DTOs;
using TH_Project.Data.Tables;
using TH_Project.Data.Enums;
using Stump.Api.Helper;
using TH_Project.Service.DTOs.Base;

namespace Stump.Api.Controllers.Admin
{
    public partial class AuthController : ControllerBase
    {
        private async Task<AuthResult> loginAsync(LoginParams args)
        {
            var employee = await this.context.NhanViens.Where(e => e.LoginName == args.LoginName && e.Status == Statuses.Default).FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new InvalidOperationException($"Sai thông tin đăng nhập");
            }

            if (employee.Password.ToLower() != args.Password.ToSha256().ToLower())
            {
                throw new InvalidOperationException($"Sai thông tin đăng nhập");
            }

            return await genarateTokenAsync(employee);
        }

        private async Task<object> verifyToken(TokenRequest tokenRequest)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var ipv4 = "";
            if (remoteIpAddress != null)
            {
                ipv4 = remoteIpAddress.MapToIPv4().ToString();
            }
            
            var data = await AuthHelper.VerifyToken(tokenRequest, tokenService, ipv4);

            if (data == null)
            {
                return null;
            }

            if (data.Success == false)
            {
                return data;
            }

            if (tokenRequest.IsMonitor)
            {
                return await genarateRefreshTokenAsync(data.TokenStoredId);
            }

            var employee = await this.context.NhanViens.Where(e => e.Id == data.Id).FirstOrDefaultAsync();

            return await genarateRefreshTokenAsync(data.TokenStoredId , employee);
        }

        private async Task<AuthResult> genarateTokenAsync(NhanVien employee = null)
        {
            string secretKey = configuration["JWT:Secret"];
            DateTime tokenExpires = DateTime.UtcNow.AddHours(8);
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var ipv4 = "";
            if (remoteIpAddress != null)
            {
                 ipv4 = remoteIpAddress.MapToIPv4().ToString();
            }

            if (employee == null)
            {
                tokenExpires = DateTime.UtcNow.AddHours(24);
            }

            // var token = AuthUtils.CreateToken(employee.Id, secretKey, tokenExpires);
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                };

            if (employee != null)
            {
                authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                };
            }

            var token = new JwtSecurityToken
            (expires: tokenExpires,
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            var jwtToken = jwtTokenHandler.WriteToken(token);


            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddHours(24),
                IsRevoked = false,
                Token = RandomString(25) + Guid.NewGuid(),
                IpAddress = ipv4
            };
            
            if (employee != null)
            {
                refreshToken = new RefreshToken()
                {
                    JwtId = token.Id,
                    UserId = employee.Id,
                    AddedDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddHours(24),
                    IsRevoked = false,
                    Token = RandomString(25) + Guid.NewGuid(),
                    IpAddress = ipv4
                };
            }

            await tokenService.AddTokenAsync(refreshToken);

            if (employee == null)
            {
                return new AuthResult
                {
                    Token = jwtToken,
                    DataTokenExpired = token.ValidTo,
                    Success = true
                };
            }

            return new AuthResult
            {
                Token = jwtToken,
                DataTokenExpired = token.ValidTo,
                Success = true
            };
        }
        
        private async Task<AuthResult> genarateRefreshTokenAsync(long tokenStored, NhanVien employee = null )
        {
            string secretKey = configuration["JWT:Secret"];
            DateTime tokenExpires = DateTime.UtcNow.AddHours(8);

            if (employee == null)
            {
                tokenExpires = DateTime.UtcNow.AddHours(24);
            }

            // var token = AuthUtils.CreateToken(employee.Id, secretKey, tokenExpires);
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                };

            if (employee != null)
            {
                authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                };
            }

            var token = new JwtSecurityToken
            (expires: tokenExpires,
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            var jwtToken = jwtTokenHandler.WriteToken(token);
            
            await tokenService.RefreshTokenAsync(tokenStored, token.Id);

            if (employee == null)
            {
                return new AuthResult
                {
                    Token = jwtToken,
                    DataTokenExpired = token.ValidTo,
                    Success = true
                };
            }

            return new AuthResult
            {
                Token = jwtToken,
                DataTokenExpired = token.ValidTo,
                Success = true
                
            };
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
