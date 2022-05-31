using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;
using TH_Project.Service.Interface;
using TH_Project.Service.Services;

namespace TH_Project.BackendApi.Controllers
{
    [ApiController]
    [Route("QuyTienMat")]


    public class QuyTienMatController : ControllerBase
    {
        private readonly IQuyTienMatService _donHangService;
        public QuyTienMatController(IQuyTienMatService DonHangService)
        {

            _donHangService = DonHangService;
        }

        [HttpGet]
        [Route("detail")]
        public async Task<QuyTienMatResult> GetChiTietQuyTienMatAsync(long id)
        {
            return await _donHangService.GetAsync(id);
        }

        [HttpPost]
        [Route("delete")]
        public async Task DeleteProductAsync(long productid)
        {
            await _donHangService.DeleteAsync(productid);
        }

        [HttpGet]
        [Route("all")]
        public async Task<PagedResult<QuyTienMatResult>> GetQuyTienMatAsync([FromQuery] QuyTienMatRequest request)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllQuyTienMatPaging(request);
        }

    }
}
