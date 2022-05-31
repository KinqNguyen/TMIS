using Microsoft.IdentityModel.Tokens;
using Stump.Api.Data.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs;

namespace Stump.Api.Helper
{
    public static class AuthHelper
    {
        /// <summary>
        /// Tạo token
        /// </summary>
        /// <param name="userID">Mã người dùng</param>
        /// <param name="key">Khóa</param>
        /// <param name="expires">Thời gian hết hạn</param>
        /// <returns></returns>
        public static JwtSecurityToken CreateToken(long userID, string key, DateTime expires)
        {
            var authClaims = new List<Claim>
                {
                    new Claim("Id", userID.ToString()),
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

        public static async Task<AuthResult> VerifyToken(
            TokenRequest tokenRequest,
            TokenService tokenService,
            string ipV4
            )
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenReader = jwtTokenHandler.ReadJwtToken(tokenRequest.Token);
                //// This validation function will make sure that the token meets the validation parameters
                //// and its an actual jwt token not just a random string
                //var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, tokenValidationParams, out var validatedToken);

                //// Now we need to check if the token has a valid security algorithm
                //if (validatedToken is JwtSecurityToken jwtSecurityToken)
                //{
                //    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                //    if (result == false)
                //    {
                //        return null;
                //    }
                //}

                // Will get the time stamp in unix time
                //var utcExpiryDate = long.Parse(identity.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                //// we convert the expiry date from seconds to the date
                //var expDate = UnixTimeStampToDateTime(utcExpiryDate);

                //if (expDate > DateTime.UtcNow)
                //{
                //    return new AuthResult()
                //    {
                //        Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
                //        Success = false
                //    };
                //}

                // Check the token we got if its saved in the db
                // var storedRefreshToken = await tokenService.GetAsync(tokenRequest.RefreshToken);
                var storedRefreshToken = await tokenService.GetByJwtTokenAsync(tokenReader.Id);

                
                
                if (storedRefreshToken == null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "Token doesnt exist" },
                        Success = false
                    };
                }

                // Check the date of the saved token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "Token has expired, user needs to relogin" },
                        Success = false
                    };
                }
                
                // check if the refresh token has been used
                // if (storedRefreshToken.IsUsed)
                // {
                //     return new AuthResult()
                //     {
                //         Errors = new List<string>() { "token has been used" },
                //         Success = false
                //     };
                // }

                // Check if the token is revoked
                if (storedRefreshToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been revoked" },
                        Success = false
                    };
                }

                // we are getting here the jwt token id
                //var jti = identity.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                //// check the id that the recieved token has against the id saved in the db
                //if (storedRefreshToken.JwtId != jti)
                //{
                //    return new AuthResult()
                //    {
                //        Errors = new List<string>() { "the token doenst mateched the saved token" },
                //        Success = false
                //    };
                //}

                //Update IsUsed to true
                // storedRefreshToken.IsUsed = true;
                // await tokenService.EditAsync(storedRefreshToken);
                
                // Check ip v4
                if (storedRefreshToken.IpAddress != ipV4)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "Not match" },
                        Success = false
                    };
                }

                return new AuthResult()
                {
                    TokenStoredId = storedRefreshToken.Id,
                    Id = storedRefreshToken.UserId.Value,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}