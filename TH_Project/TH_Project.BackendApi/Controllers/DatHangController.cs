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
    [Route("DatHang")]


    public class DatHangController : ControllerBase
    {
        private readonly IDatHangService _donHangService;
        public DatHangController(IDatHangService DonHangService)
        {

            _donHangService = DonHangService;
        }

        [HttpPost]
        [Route("delete")]
        public async Task DeleteProductAsync(long productid)
        {
            await _donHangService.DeleteAsync(productid);
        }

        [HttpGet]
        [Route("all")]
        public async Task<PagedResult<DatHangResult>> GetProductAsync([FromQuery] DatHangRequest request, string getNotification)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllDatHangPaging(request, getNotification);
        }

    }
}
