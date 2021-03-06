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
    public class DatHangService : IDatHangService
    {
        private readonly TH_DbConotext _context;
        public DatHangService(TH_DbConotext context)
        {
            _context = context;
        }


        public async Task<PagedResult<DatHangResult>> getAllDatHangPaging(DatHangRequest request, string getNotification)
        {

            var Result = new PagedResult<DatHangResult>() { };
            var ResultItem = new List<DatHangResult>();

            var DonhangQerry = _context.DatHangs.AsNoTracking().Include(e => e.NhanVien).Include(e => e.DoiTac).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                MaDonHang = e.MaDonHang,
                TenCongTrinh = e.TenCongTrinh,
                NoiDung = e.NoiDung,
                GiaTri = e.GiaTri,
                NgayGiaoHang = e.NgayGiaoHang,
                NgayBatDau = e.NgayBatDau,
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
            if (request.FromDate != null && request.ToDate != null)
            {
                RawDonhang = RawDonhang.Where(e =>
                 e.NgayBatDau >= request.FromDate && e.NgayBatDau <= request.ToDate)
                    .ToList();
            }


            // Search or Filter theo mã đơn hàng tên nhân viên và tên đối tác 
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                RawDonhang = RawDonhang.Where(e =>
                    e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.TenNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.TenDoiTac.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
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
                var resultItem = new DatHangResult
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
                    TrangThai = item.TrangThaiDonHang,
                    Status = item.Status,

                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<DatHangResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }


        public async Task<DatHangResult> GetDatHangAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy Đơn đặt hàng");
            }
            return new DatHangResult
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

        public async Task<DatHang> FetchAsync(long id)
        {
            return await _context.DatHangs.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
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




        public async Task CreateDoiTacAsync(DatHangCreate args)
        {
            {
                // check hóa đơn
                {
                    var checkCate = _context.DoiTacs
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdDoiTac && x.Status == Statuses.Default)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy Doi Tac ");
                    }
                }
            }

            {
                // check hóa đơn
                {
                    var checkCate = _context.NhanViens
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdNhanVien && x.Status == Statuses.Default)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy Nhan Vien ");
                    }
                }
            }
            var prod = await  _context.DatHangs.OrderByDescending(x=> x.Id).AsNoTracking().Where(x => x.Status != Statuses.Deleted).FirstOrDefaultAsync();
            long bro = prod.Id + 1;

            var product = new DatHang
            {
                IdDoiTac = args.IdDoiTac.Value,
                IdNhanVien = args.IdNhanVien.Value,
                    MaDonHang = "ORD_0000_" + bro,
                TenCongTrinh = args.TenCongTrinh,
                NoiDung = args.NoiDung,
                GiaTri = args.GiaTri.Value,
                NgayBatDau = args.NgayBatDau,
                Slug = args.Slug,
                NgayGiaoHang = args.NgayGiaoHang,
                NgayToiHan = args.NgayToiHan,
                GhiChu = args.GhiChu,
                Status = Statuses.Default,
                TrangThai = args.TrangThai.Value,
            };

            _context.DatHangs.Add(product);

            await _context.SaveChangesAsync();

        }






        //sửa sản phẩm
        public async Task EditAsync(long id, DatHangEdit args)
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
                var checkProd = await _context.DatHangs.Where(x => x.MaDonHang == args.MaDonHang && x.Id != id).FirstOrDefaultAsync();
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
            if (args.GiaTri != null) { product.GiaTri = args.GiaTri.Value; }
            if (args.IdNhanVien != null) { product.IdNhanVien = args.IdNhanVien.Value; }
            if (args.IdDoiTac != null) { product.IdDoiTac = args.IdDoiTac.Value; }
            if (args.NgayBatDau.HasValue) { product.NgayBatDau = args.NgayBatDau.Value; }
            if (args.NgayGiaoHang.HasValue) { product.NgayGiaoHang = args.NgayGiaoHang.Value; }
            if (args.TrangThai != null) { product.TrangThai = args.TrangThai.Value; }

            // update kể cả khi request truyền null 
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //await versionService.NewVersion(ObjectVersions.Product, product.Id);



        }




    }
}
