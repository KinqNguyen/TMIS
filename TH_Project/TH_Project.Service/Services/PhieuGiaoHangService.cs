using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TH_Project.Data;
using TH_Project.Data.Tables;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;
using SscKiosk.Kitchen.Api.Data.Utils;
using TH_Project.Data.Enums;

namespace TH_Project.Service.Services
{
    public class PhieuGiaoHangService : IPhieuGiaoHangService
    {
        private readonly TH_DbConotext _context;
        public PhieuGiaoHangService(TH_DbConotext context)
        {
            _context = context;
        }

        //public async Task<PhieuGiaoHang> FetchAsync(long id)
        //{
        //    return await _context.PhieuGiaoHangs.FindAsync(id);
        //}

        public async Task<PhieuGiaoHang> FetchAsync(long id)
        {
            return await _context.PhieuGiaoHangs.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



        public async Task<PhieuGiaoHangResult> GetPhieuGiaoHangAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy Đơn đặt hàng");
            }
            return new PhieuGiaoHangResult
            {
                Id = item.Id,
                IdDonHang = item.IdDonHang,
                MaDonHang = item.DonHang.MaDonHang,
                SoPhieuGH = item.SoPhieuGH,
                NgayLap = item.NgayLap,
                NgayGiao = item.NgayGiao,
                Slug = item.Slug,
                GhiChu = item.GhiChu,
                Status = item.Status

            };
        }


        public async Task<PagedResult<PhieuGiaoHangResult>> getAllPhieuGiaoHangPaging(PhieuGiaoHangRequest request)
        {
            var ResultItem = new List<PhieuGiaoHangResult>();

            var DonhangQerry = _context.PhieuGiaoHangs.AsNoTracking().Include(e => e.DonHang).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                IdDonHang = e.IdDonHang,
                MaDonHang = e.DonHang.MaDonHang,
                SoPhieuGH = e.SoPhieuGH,
                NgayLap = e.NgayLap,
                NgayGiao = e.NgayGiao,
                Slug = e.Slug,
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
                    e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.SoPhieuGH.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.Slug.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
                    .ToList();
            }
            // Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            if (request.IdDonHang != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdDonHang == request.IdDonHang)
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
                var resultItem = new PhieuGiaoHangResult
                {

                    Id = item.Id,
                    IdDonHang = item.IdDonHang,
                    MaDonHang = item.MaDonHang,
                    SoPhieuGH = item.SoPhieuGH,
                    NgayLap = item.NgayLap,
                    NgayGiao = item.NgayGiao,
                    Slug = item.Slug,
                    GhiChu = item.GhiChu,
                    Status = item.Status
                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<PhieuGiaoHangResult>
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
