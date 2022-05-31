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
    [Route("HoaDon")]


    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService _donHangService;
        public HoaDonController(IHoaDonService DonHangService)
        {

            _donHangService = DonHangService;
        }

        [HttpGet]
        [Route("detail")]
        public async Task<HoaDonResult> GetChiTietCongNoAsync(long id)
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
        [Route("all/Test")]
        public async Task<List<HoaDonResult>> GetTest(  )
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.GetAsync();
        }



        [HttpGet]
        [Route("all")]
        public async Task<PagedResult<HoaDonResult>> GetHoaDonAsync([FromQuery] HoaDonRequest request)
        {
            return await _donHangService.getAllHoaDonPaging(request);
        }
        [HttpGet]
        [Route("all/hoa-don-dat-hang")]
        public async Task<PagedResult<HoaDonResult>> GetHoaDonDatHangAsync([FromQuery] HoaDonRequest request,string getNotification)
        {
            return await _donHangService.getAllHoaDon_DatHangPaging(request, getNotification);
        }
        [HttpGet]
        [Route("all/hoa-don-don-hang")]
        public async Task<PagedResult<HoaDonResult>> GetHoaDonDonHangAsync([FromQuery] HoaDonRequest request, string getNotification)
        {
            return await _donHangService.getAllHoaDon_DonHangPaging(request, getNotification);
        }

    }
}
