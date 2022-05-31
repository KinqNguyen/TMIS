using SscKiosk.Kitchen.Api.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Data;
using TH_Project.Data.Enums;
using TH_Project.Data.Tables;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
   public class DanhBaService : IDanhBaService
    {
        private readonly TH_DbConotext _context;
        public DanhBaService(TH_DbConotext context)
        {
            _context = context;
        }

        public async Task<DanhBaDoiTac> FetchDoiTacAsync(long id)
        {
            return await _context.DanhBaDoiTacs.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DanhBaNhanVien> FetchNhanVienAsync(long id)
        {
            return await _context.DanhBaNhanViens.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }



        public async Task DeleteDoiTacAsync(long productid)
        {
            var product = await FetchDoiTacAsync(productid);

            product.Status = Statuses.Deleted;
            product.XungHo = $" {productid} Số di động đã bị xóa";
            product.Ten = $" {productid} Số di động đã bị xóa";
            product.ChucVu = $" {productid} Số di động đã bị xóa";
            product.DiDong = $" {productid} Số di động đã bị xóa";
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteNhanVienAsync(long productid)
        {
            var product = await FetchNhanVienAsync(productid);

            product.Status = Statuses.Deleted;
            product.GhiChu = $" {productid} Số di động đã bị xóa";
            product.DiDong = $" {productid} Số di động đã bị xóa";
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



        public async Task<DanhBaResult> GetDoiTacAsync(long productid)
        {
            var item = await this.FetchDoiTacAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy số di động này");
            }
            return new DanhBaResult
            {
                Id = item.Id,
                IdDoiTac = item.IdDoiTac,
                TenCongTyDoiTac = item.DoiTac.TenCongTy,
                XungHo = item.XungHo,
                Ho = item.Ho,
                Ten = item.Ten,
                ChucVu = item.ChucVu,
                DiDong = item.DiDong,
                Mail = item.Mail,
                GhiChu = item.GhiChu,
                Status = item.Status
            };
        }
        public async Task<DanhBaResult> GetNhanVienAsync(long productid)
        {
            var item = await this.FetchNhanVienAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sản phẩm");
            }
            return new DanhBaResult
            {
                Id = item.Id,
                IdNhanVien = item.IdNhanVien,
                ViTriNhanVien = item.NhanVien.BietHieu,
                TenNhanVien = item.NhanVien.Ho + " " + item.NhanVien.Ten,
                DiDong = item.DiDong,
                Mail = item.Mail,
                GhiChu = item.GhiChu,
                Status = item.Status
            };
        }








        //public async Task EditNhanVienAsync(long id, CongNoEdit args)
        //{
        //    var product = await FetchNhanVienAsync(id);
        //    if (product == null)
        //    {
        //        throw new InvalidOperationException($"Sản phẩm không tồn tại hoặc đã bị xóa");
        //    }
        //    if (args.IdHoaDon.HasValue)
        //    {
        //        // check loại sản phẩm
        //        {
        //            var checkCate = _context.HoaDons
        //                .AsNoTracking()
        //                .Where(x => x.Id == args.IdHoaDon && x.Status == Statuses.Default)
        //                .FirstOrDefault();

        //            if (checkCate == null)
        //            {
        //                throw new InvalidOperationException($"Không tìm thấy loại sản phẩm");
        //            }
        //        }
        //    }


        //    //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
        //    //{ product.ImagePath = args.ImagePath; }

        //    //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
        //    //{ product.VideoPath = args.VideoPath; }

        //    // update query for string
        //    if (!string.IsNullOrEmpty(args.MaCongNo) || !string.IsNullOrWhiteSpace(args.MaCongNo))
        //    {
        //        var checkProd = await _context.CongNos.Where(x => x.MaCongNo == args.MaCongNo && x.Id != id).FirstOrDefaultAsync();
        //        if (checkProd != null)
        //        {
        //            throw new InvalidOperationException($"Đã tồn tại sản phẩm {args.MaCongNo}");
        //        }
        //        product.MaCongNo = args.MaCongNo;
        //    }


        //    if (!string.IsNullOrEmpty(args.TenCongTrinh) || !string.IsNullOrWhiteSpace(args.TenCongTrinh))
        //    { product.TenCongTrinh = args.TenCongTrinh; }
        //    if (!string.IsNullOrEmpty(args.NoiDung) || !string.IsNullOrWhiteSpace(args.NoiDung))
        //    { product.NoiDung = args.NoiDung; }///
        //    if (!string.IsNullOrEmpty(args.Slug) || !string.IsNullOrWhiteSpace(args.Slug))
        //    { product.Slug = args.Slug; }
        //    if (!string.IsNullOrEmpty(args.GhiChu) || !string.IsNullOrWhiteSpace(args.GhiChu))
        //    { product.GhiChu = args.GhiChu; }

        //    //update query for int 
        //    if (args.GiaTri != null) { product.GiaTri = args.GiaTri; }
        //    if (args.NgayToiHan.HasValue) { product.NgayToiHan = args.NgayToiHan.Value; }
        //    if (args.NgayKetThuc.HasValue) { product.NgayKetThuc = args.NgayKetThuc.Value; }
        //    if (args.TrangThai != null) { product.TrangThai = args.TrangThai; }

        //    // update kể cả khi request truyền null 
        //    _context.Entry(product).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    //await versionService.NewVersion(ObjectVersions.Product, product.Id);
        //}



        public async Task<PagedResult<DanhBaResult>> getAllDanhBaDoiTacPaging(DanhBaRequest request)
        {

            var Result = new PagedResult<CongNoResult>() { };
            var ResultItem = new List<DanhBaResult>();

            var DonhangQerry = _context.DanhBaDoiTacs.AsNoTracking().Include(e => e.IdDoiTac).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                IdDoiTac = e.IdDoiTac,
                TenCongTyDoiTac = e.DoiTac.TenCongTy,
                XungHo = e.XungHo,
                Ho = e.Ho,
                Ten = e.Ten,
                ChucVu = e.ChucVu,
                DiDong = e.DiDong,
                Mail = e.Mail,
                GhiChu = e.GhiChu,
                Status = e.Status

    }).ToListAsync();

            // Filter

            //test danh sách lấy thông báo 
            //if (!string.IsNullOrEmpty(dayss) || !string.IsNullOrWhiteSpace(dayss))
            //{
            //    //DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
            //    DateTime noww = DateTime.Now;
            //    DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
            //    RawDonhang = RawDonhang.Where(e =>
            //        e.NgayToiHan < oldDate)
            //        .ToList();
            //}



            // Search or Filter theo mã đơn hàng tên nhân viên và tên đối tác 
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                RawDonhang = RawDonhang.Where(e =>
                    e.Ten.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.Ho.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.DiDong.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())||
                    e.TenCongTyDoiTac.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
                    .ToList();
            }
            // Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            if (request.IdDoiTac != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdDoiTac == request.IdDoiTac)
                    .ToList();
            }

            var donhangs = RawDonhang
              .OrderBy(e => e.Id)
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToList();

            var totalRecords = RawDonhang.Count();
            var rawTotalPages = ((double)totalRecords / (double)request.PageSize);
            var totalPages = (int)Math.Round(rawTotalPages);
            if (rawTotalPages > totalPages)
            {
                totalPages += 1;
            }
            foreach (var item in donhangs)
            {
                var resultItem = new DanhBaResult
                {

                    Id = item.Id,
                    IdDoiTac = item.IdDoiTac,
                    TenCongTyDoiTac = item.TenCongTyDoiTac,
                    XungHo = item.XungHo,
                    Ho = item.Ho,
                    Ten = item.Ten,
                    ChucVu = item.ChucVu,
                    DiDong = item.DiDong,
                    Mail = item.Mail,
                    GhiChu = item.GhiChu,
                    Status = item.Status
                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<DanhBaResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }
        public async Task<PagedResult<DanhBaResult>> getAllDanhBaNhanVienPaging(DanhBaRequest request)
        {

            var Result = new PagedResult<CongNoResult>() { };
            var ResultItem = new List<DanhBaResult>();

            var DonhangQerry = _context.DanhBaNhanViens.AsNoTracking().Include(e => e.IdNhanVien).Include(e => e.NhanVien.IdViTri).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                IdNhanVien = e.IdNhanVien,
                TenNhanVien = e.NhanVien.Ten,
                ViTriNhanVien = e.NhanVien.ViTriNhanVien.TenViTri,
                DiDong = e.DiDong,
                Mail = e.Mail,
                GhiChu = e.GhiChu,
                Status = e.Status

            }).ToListAsync();

            // Filter

            //test danh sách lấy thông báo 
            //if (!string.IsNullOrEmpty(dayss) || !string.IsNullOrWhiteSpace(dayss))
            //{
            //    //DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
            //    DateTime noww = DateTime.Now;
            //    DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
            //    RawDonhang = RawDonhang.Where(e =>
            //        e.NgayToiHan < oldDate)
            //        .ToList();
            //}



            // Search or Filter theo mã đơn hàng tên nhân viên và tên đối tác 
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                RawDonhang = RawDonhang.Where(e =>
                    e.TenNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.ViTriNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.DiDong.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
                    .ToList();
            }
            // Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            if (request.IdDoiTac != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdNhanVien == request.IdDoiTac)
                    .ToList();
            }

            var donhangs = RawDonhang
              .OrderBy(e => e.Id)
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToList();

            var totalRecords = RawDonhang.Count();
            var rawTotalPages = ((double)totalRecords / (double)request.PageSize);
            var totalPages = (int)Math.Round(rawTotalPages);
            if (rawTotalPages > totalPages)
            {
                totalPages += 1;
            }
            foreach (var item in donhangs)
            {
                var resultItem = new DanhBaResult
                {

                    Id = item.Id,
                    IdNhanVien = item.IdNhanVien,
                    ViTriNhanVien = item.ViTriNhanVien,
                    TenNhanVien = item.TenNhanVien,
                    DiDong = item.DiDong,
                    Mail = item.Mail,
                    GhiChu = item.GhiChu,
                    Status = item.Status
                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<DanhBaResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }



    }
}
