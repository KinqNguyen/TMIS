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
    [Route("TaiKhoanNganHang")]


    public class TaiKhoanNganHangController : ControllerBase
    {
        private readonly ITaiKhoanNganHangService _donHangService;
        public TaiKhoanNganHangController(ITaiKhoanNganHangService DonHangService)
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
        [Route("detail-nhan-vien")]
        public async Task<TaiKhoanNganHangNhanVienResult> GetChiTietTKNHNhanVienAsync(long id)
        {
            return await _donHangService.GetTKNHNhanVienAsync(id);
        }
        [HttpGet]
        [Route("detail-doi-tac")]
        public async Task<TaiKhoanNganHangDoiTacResult> GetChiTietTKNHDoiTacAsync(long id)
        {
            return await _donHangService.GetTKNHDoiTacAsync(id);
        }

        [HttpGet]
        [Route("all/tai-khoan-ngan-hang-doi-tac")]
        public async Task<PagedResult<TaiKhoanNganHangDoiTacResult>> GetTaiKhoanDoiTacAsync([FromQuery] TaiKhoanNganHangDoiTacRequest request)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllTaiKhoan_DoiTacPaging(request);
        }


        [HttpGet]
        [Route("all/tai-khoan-ngan-hang-nhan-vien")]
        public async Task<PagedResult<TaiKhoanNganHangNhanVienResult>> GetTaiKhoanNhanVienAsync([FromQuery] TaiKhoanNganHangNhanVienRequest request)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllTaiKhoan_NhanVienPaging(request);
        }

    }
}
