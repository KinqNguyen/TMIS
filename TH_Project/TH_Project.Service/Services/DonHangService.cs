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
using TH_Project.Service.Interface;

namespace TH_Project.Service.Services
{
    public class DonHangService : IDonHangService
    {
        private readonly TH_DbConotext _context;
        public DonHangService(TH_DbConotext context) {
            _context = context;
        }

        public async Task<DonHang> FetchAsync(long id)
        {
            return await _context.DonHangs.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            product.TenCongTrinh = $" {productid} Số di động đã bị xóa";
            product.GhiChu = $" {productid} Số di động đã bị xóa";
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<DonHangResult> GetDonHangAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy Đơn đặt hàng");
            }
            return new DonHangResult
            {
                Id = item.Id,
                IdDoiTac = item.IdDoiTac,
                IdNhanVien = item.IdNhanVien,
                MaDonHang = item.MaDonHang,
                TenCongTrinh = item.TenCongTrinh,
                NoiDung = item.NoiDung,
                GiaTri = item.GiaTri,
                NgayBatDau = item.NgayBatDau,
                NgayGiaoHang = item.NgayGiaoHang,
                GhiChu = item.GhiChu,
                TrangThai = item.TrangThai,
                Status = item.Status,

            };
        }


        public async Task<PagedResult<DonHangResult>> getAllDonHangPaging(DonHangRequest request, string getNotification) {

            var Result = new PagedResult<DonHangResult>() { };
            var ResultItem = new List<DonHangResult>();

            var DonhangQerry = _context.DonHangs.AsNoTracking().Include(e => e.IdNhanVien).Include(e=>e.IdDoiTac).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                MaDonHang = e.MaDonHang,
                TenCongTrinh = e.TenCongTrinh,
                NoiDung = e.NoiDung,
                GiaTri = e.GiaTri,
                NgayGiaoHang = e.NgayGiaoHang,
                NgayBatDau= e.NgayBatDau,
                GhiChu = e.GhiChu,
                IdDoiTac = e.IdDoiTac,
                IdNhanVien = e.IdNhanVien,
                TenDoiTac = e.DoiTac.TenCongTy,
                TenNhanVien = e.NhanVien.Ten,
                TrangThaiDonHang = e.TrangThai,
                Status = e.Status
               
            }).ToListAsync();



            // Filter

            //test danh sách lấy thông báo theo thời gian
            if (!string.IsNullOrEmpty(getNotification) || !string.IsNullOrWhiteSpace(getNotification))
            {
                DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
                DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
                RawDonhang = RawDonhang.Where(e =>
                    e.NgayBatDau < oldDate)
                    .ToList();
            }

            //test danh sách lấy từ ngày đến ngày 
            if (request.FromDate!=null && request.ToDate != null)
            {
                RawDonhang = RawDonhang.Where(e =>
                 e.NgayBatDau >= request.FromDate &&  e.NgayBatDau <= request.ToDate)
                    .ToList();
            }



            // Filter theo mã đơn hàng tên nhân viên và tên đối tác 
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                RawDonhang = RawDonhang.Where(e =>
                    e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.TenNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.TenDoiTac.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.TenCongTrinh.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
                    .ToList();
            }
            // Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            if (request.IdDoiTac != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdDoiTac == request.IdDoiTac)
                    .ToList(); 
            }


            if (request.IdNhanVien != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdNhanVien == request.IdNhanVien)
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
                var resultItem = new DonHangResult
                {
                    Id  = item.Id,
                    IdDoiTac  = item.IdDoiTac,
                    IdNhanVien  = item.IdNhanVien,
                    MaDonHang   = item.MaDonHang,
                    TenCongTrinh  = item.TenCongTrinh,
                    NoiDung  = item.NoiDung,
                    GiaTri  = item.GiaTri,
                    NgayBatDau  = item.NgayBatDau,
                    NgayGiaoHang  = item.NgayGiaoHang,
                    GhiChu  = item.GhiChu,
                    TrangThai  = item.TrangThaiDonHang,
                    Status  = item.Status,

                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<DonHangResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
               
            };
        }


        //sửa sản phẩm
        public async Task EditAsync(long id, DonHangEdit args)
        {
            var product = await FetchAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Đơn đặt hàng không tồn tại hoặc đã bị xóa");
            }
            if (args.IdNhanVien.HasValue)
            {
                // check bảng cha
                {
                    var checkCate = _context.NhanViens
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdNhanVien && x.Status == Statuses.Default)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy nhân viên");
                    }
                }
            }
            if (args.IdDoiTac.HasValue)
            {
                // check bảng cha
                {
                    var checkCate = _context.DoiTacs
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdDoiTac && x.Status == Statuses.Default)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy dối tác này");
                    }
                }
            }


            //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
            //{ product.ImagePath = args.ImagePath; }

            //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
            //{ product.VideoPath = args.VideoPath; }

            // update query for string
            if (!string.IsNullOrEmpty(args.MaDonHang) || !string.IsNullOrWhiteSpace(args.MaDonHang))
            {
                var checkProd = await _context.DonHangs.Where(x => x.MaDonHang == args.MaDonHang && x.Id != id).FirstOrDefaultAsync();
                if (checkProd != null)
                {
                    throw new InvalidOperationException($"Đã tồn tại sản phẩm {args.MaDonHang}");
                }
                product.MaDonHang = args.MaDonHang;
            }

            if (!string.IsNullOrEmpty(args.TenCongTrinh) || !string.IsNullOrWhiteSpace(args.TenCongTrinh))
            { product.TenCongTrinh = args.TenCongTrinh; }
            if (!string.IsNullOrEmpty(args.NoiDung) || !string.IsNullOrWhiteSpace(args.NoiDung))
            { product.NoiDung = args.NoiDung; }///
            if (!string.IsNullOrEmpty(args.Slug) || !string.IsNullOrWhiteSpace(args.Slug))
            { product.Slug = args.Slug; }
            if (!string.IsNullOrEmpty(args.GhiChu) || !string.IsNullOrWhiteSpace(args.GhiChu))
            { product.GhiChu = args.GhiChu; }

            //update query for int 
            if (args.GiaTri != null) { product.GiaTri = args.GiaTri; }
            if (args.NgayBatDau.HasValue) { product.NgayBatDau = args.NgayBatDau.Value; }
            if (args.NgayGiaoHang.HasValue) { product.NgayGiaoHang = args.NgayGiaoHang.Value; }
            if (args.TrangThai != null) { product.TrangThai = args.TrangThai; }

            // update kể cả khi request truyền null 
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //await versionService.NewVersion(ObjectVersions.Product, product.Id);



        }




    }
}
