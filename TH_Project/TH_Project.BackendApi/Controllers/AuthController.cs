using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TH_Project.Data;
using Stump.Api.Data.Services;
using TH_Project.Service.Services;
using TH_Project.Service.DTOs;
using TH_Project.Service.DTOs.Base;

namespace Stump.Api.Controllers.Admin
{
    [ApiController]
    [Route("auth")]
    public partial class AuthController : ControllerBase
    {
        private readonly TH_DbConotext context;
        private readonly TokenService tokenService;
        private readonly IConfiguration configuration;
        private readonly INhanVienService employeeService;

        public AuthController(TH_DbConotext dbContext, IConfiguration configuration, TokenService tokenService, INhanVienService employeeService)
        {
            this.context = dbContext;
            this.configuration = configuration;
            this.tokenService = tokenService;
            this.employeeService = employeeService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<AuthResult> LoginAsync(LoginParams args)
        {
            var result = await loginAsync(args); 

           // HttpContext.Session.SetString("JWToken", result.Token);

            return result;
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<object> RefreshTokenAsync(TokenRequest tokenRequest)
        {
            return await verifyToken(tokenRequest);
        }

        [Authorize]
        [HttpGet]
        [Route("get-me")]
        public async Task<GetMeData> GetMeAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var Uid = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var empl = await employeeService.GetNhanVienAsync(long.Parse(Uid));
            return new GetMeData
            {
                Id = empl.Id,
                Name = empl.Ten,
                RoleId = empl.RoleId,

            };
        }
    }
}