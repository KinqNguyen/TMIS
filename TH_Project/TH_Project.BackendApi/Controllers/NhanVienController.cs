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
    [Route("NhanVien")]


    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _donHangService;
        public NhanVienController(INhanVienService DonHangService)
        {

            _donHangService = DonHangService;
        }

        [HttpPost]
        [Route("delete")]
        public async Task DeleteProductAsync(long productid)
        {
            await _donHangService.DeleteAsync(productid);
        }

        [HttpPost]
        [Route("edit")]
        [DisableRequestSizeLimit]
        public async Task EditProductAsync(long id, [FromForm] NhanVienEdit args)
        {
            await _donHangService.EditAsync(id, args);
        }



        [HttpGet]
        [Route("all")]
        public async Task<PagedResult<NhanVienResult>> getAllNhanVienPaging([FromQuery] NhanVienRequest request)
        {
            //{
            //    var identity = HttpContext.User.Identity as ClaimsIdentity;
            //    var userID = identity.Claims.Where(e => e.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //    await dbContext.CheckPermissionAsync(1, long.Parse(userID)); // 1 - xem
            //} 
            return await _donHangService.getAllNhanVienPaging(request);
        }


        [HttpGet]
        [Route("detail")]
        public async Task<NhanVienResult> GetChiTietCongNoAsync(long id)
        {
            return await _donHangService.GetNhanVienAsync(id);
        }


        [HttpPost]
        [Route("create")]
        public async Task<long> CreateProductAsync([FromForm] NhanVienEdit args)
        {

            return await _donHangService.CreateAsync(args);


            //string imagePath = args.image != null ? this.env.SaveFile(args.image) : null;
            //string videoPath = args.video != null ? this.env.SaveFile(args.video) : null;

            //await productService.CreateAsync(new CreateProductArgs
            //{
            //    Name = args.name,
            //    Description = args.description,
            //    CategoryID = args.categoryId,
            //    ImagePath = imagePath,
            //    VideoPath = videoPath,
            //    UnitPrice = args.unitPrice,
            //});
        }

    }
}
