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
    [Route("DanhBa")]


    public class DanhBaController : ControllerBase
    {
        private readonly IDanhBaService _donHangService;
        public DanhBaController(IDanhBaService DonHangService)
        {

            _donHangService = DonHangService;
        }

        [HttpGet]
        [Route("detail/doi-tac")]
        public async Task<DanhBaResult> GetChiTietDoiTacAsync(long id)
        {
            return await _donHangService.GetDoiTacAsync(id);
        }



        [HttpGet]
        [Route("detail/nhan-vien")]
        public async Task<DanhBaResult> GetChiTietNhanVienAsync(long id)
        {
            return await _donHangService.GetNhanVienAsync(id);
        }
        [HttpPost]
        [Route("delete-doi-tac")]
        public async Task DeleteDoiTacAsync(long productid)
        {
            await _donHangService.DeleteDoiTacAsync(productid);
        }

        [HttpPost]
        [Route("delete-nhan-vien")]
        public async Task DeleteNhanVienAsync(long productid)
        {
            await _donHangService.DeleteNhanVienAsync(productid);
        }



        [HttpGet]
        [Route("all/NhanVien")]
        public async Task<PagedResult<DanhBaResult>> GetDanhbaNhanVienAsync([FromQuery] DanhBaRequest request)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllDanhBaNhanVienPaging(request);
        }
        [HttpGet]
        [Route("all/DoiTac")]
        public async Task<PagedResult<DanhBaResult>> GetDanhbaDoiTacAsync([FromQuery] DanhBaRequest request)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllDanhBaDoiTacPaging(request);
        }

    }
}
