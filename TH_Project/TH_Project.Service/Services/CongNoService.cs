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
    public class CongNoService
        : ICongNoService
    {
        private readonly IHoaDonService _hoadonService;
        private readonly TH_DbConotext _context;
        public CongNoService(TH_DbConotext context, IHoaDonService hoadonService)
        {
            _context = context;
            this._hoadonService = hoadonService;
        }

        public async Task<CongNo> FetchAsync(long id)
        {
            return await _context.CongNos.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);
            product.Status = Statuses.Deleted;
            product.MaCongNo = $"Deleted {productid}";
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<CongNoResult>> getAllCongNoPaging(CongNoRequest request, string getNotification)
        {

            var Result = new PagedResult<CongNoResult>() { };
            var ResultItem = new List<CongNoResult>();

            var DonhangQerry = _context.CongNos.AsNoTracking().Include(e => e.HoaDon).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                idHoaDOn = e.IdHoaDon,
                MaCongNo = e.MaCongNo,
                TenCongTrinh = e.TenCongTrinh,
                IdDonHang = e.HoaDon.IdDonHang,
                IdDatHang = e.HoaDon.IdDatHang,
                NoiDung = e.NoiDung,
                GiaTri = e.GiaTri,
                NgayBatDau = e.NgayBatDau,
                MaHoaDon = e.HoaDon.SoHD,
                NgayToiHan = e.NgayToiHan,
                NgayKetThuc = e.NgayKetThuc,
                TrangThai = e.TrangThai,
                GhiChu = e.GhiChu,
                Status = e.Status

            }).ToListAsync();

            // Filter

            if (request.LoaiCongNo == Data.Enums.IOStatuses.Nhap)
            {
                RawDonhang = RawDonhang.Where(e =>
                 e.IdDatHang != null)
                    .ToList();
            }
            else {
                RawDonhang = RawDonhang.Where(e =>
                 e.IdDonHang != null)
                    .ToList();
            }


            //test danh sách lấy thông báo 
            if (!string.IsNullOrEmpty(getNotification) || !string.IsNullOrWhiteSpace(getNotification))
            {
                //DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
                DateTime noww = DateTime.Now;
                DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
                RawDonhang = RawDonhang.Where(e =>
                    e.NgayToiHan < oldDate)
                    .ToList();
            }


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
                    e.TenCongTrinh.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.MaCongNo.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.MaHoaDon.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
                    .ToList();
            }
            // Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            if (request.IdHoaDon != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.idHoaDOn == request.IdHoaDon)
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
                var resultItem = new CongNoResult
                {
                    Id = item.Id,
                    IdHoaDon = item.idHoaDOn,
                    MaCongNo = item.MaCongNo,
                    TenCongTrinh = item.TenCongTrinh,
                    NoiDung = item.NoiDung,
                    GiaTri = item.GiaTri,
                    NgayBatDau = item.NgayBatDau,
                    MaHoaDon = item.MaHoaDon,
                    NgayToiHan = item.NgayToiHan,
                    NgayKetThuc = item.NgayKetThuc,
                    TrangThai = item.TrangThai,
                    GhiChu = item.GhiChu,

                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<CongNoResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }
        public async Task<long> CreateAsync(CongNoCreate args)
        {
            {
                // check hóa đơn
                {
                    var checkCate = await _hoadonService.GetAsync(args.IdHoaDon);

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy hóa đơn ");
                    }
                }
            }

            var checkProd = await _context.CongNos.Where(x => x.MaCongNo == args.MaCongNo).FirstOrDefaultAsync();
            if (checkProd != null)
            {
                throw new InvalidOperationException($"Đã tồn tại công nợ có mã là :  {args.MaCongNo}");
            }

            var product = new CongNo
            {
                MaCongNo = args.MaCongNo,
                TenCongTrinh = args.TenCongTrinh,
                Slug = args.Slug,
                NoiDung = args.NoiDung,
                GiaTri = args.GiaTri,
                NgayBatDau = args.NgayBatDau,
                NgayToiHan = args.NgayToiHan,
                NgayKetThuc = args.NgayKetThuc,
                TrangThai = args.TrangThai,
                GhiChu = args.GhiChu,
                IdHoaDon = args.IdHoaDon,
                Status = Statuses.Default,
            };

            _context.CongNos.Add(product);

            await _context.SaveChangesAsync();


            return product.Id;
        }
        public async Task<CongNoResult> GetAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sản phẩm");
            }
            return new CongNoResult
            {
                Id = item.Id,
                IdHoaDonFake = item.IdHoaDon,
                IdHoaDon = _context.HoaDons.Where(e => e.Id == item.IdHoaDon).First().Id,
                MaHoaDon = _context.HoaDons.Where(e => e.Id == item.IdHoaDon).First().SoHD,
                MaCongNo = item.MaCongNo,
                TenCongTrinh = item.TenCongTrinh,
                NoiDung = item.NoiDung,
                GiaTri = item.GiaTri,
                NgayBatDau = item.NgayBatDau,
                NgayToiHan = item.NgayToiHan,
                NgayKetThuc = item.NgayKetThuc,
                TrangThai = item.TrangThai,
                GhiChu = item.GhiChu,
            };
        }



        //sửa sản phẩm
        public async Task EditAsync(long id, CongNoEdit args)
        {
            var product = await FetchAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Sản phẩm không tồn tại hoặc đã bị xóa");
            }
            if (args.IdHoaDon.HasValue)
            {
                // check loại sản phẩm
                {
                    var checkCate = _context.HoaDons
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdHoaDon && x.Status == Statuses.Default)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy loại sản phẩm");
                    }
                }
            }


            //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
            //{ product.ImagePath = args.ImagePath; }

            //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
            //{ product.VideoPath = args.VideoPath; }

            // update query for string
            if (!string.IsNullOrEmpty(args.MaCongNo) || !string.IsNullOrWhiteSpace(args.MaCongNo))
            {
                var checkProd = await _context.CongNos.Where(x => x.MaCongNo == args.MaCongNo && x.Id != id).FirstOrDefaultAsync();
                if (checkProd != null)
                {
                    throw new InvalidOperationException($"Đã tồn tại sản phẩm {args.MaCongNo}");
                }
                product.MaCongNo = args.MaCongNo;
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
            if (args.GiaTri != null ) { product.GiaTri = args.GiaTri; }
            if (args.NgayToiHan.HasValue) { product.NgayToiHan = args.NgayToiHan.Value; }
            if (args.NgayKetThuc.HasValue) { product.NgayKetThuc = args.NgayKetThuc.Value; }
            if (args.TrangThai != null ) { product.TrangThai = args.TrangThai; }

            // update kể cả khi request truyền null 
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //await versionService.NewVersion(ObjectVersions.Product, product.Id);
        }

    } }

    

