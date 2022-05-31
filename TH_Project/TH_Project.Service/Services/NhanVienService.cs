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
    public class NhanVienService : INhanVienService
    {
        private readonly TH_DbConotext _context;
        public NhanVienService(TH_DbConotext context)
        {
            _context = context;
        }

        public async Task<NhanVien> FetchAsync(long id)
        {
            return await _context.NhanViens.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }


        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<NhanVienResult> GetNhanVienAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy Đơn đặt hàng");
            }
            return new NhanVienResult
            {
                Id = item.Id,
                IdViTri = item.IdViTri,
                ViTriNhanVien = item.ViTriNhanVien.TenViTri,
                Ho = item.Ho,
                Ten = item.Ten,
                HoTen = item.Ho + " " + item.Ten,
                BietHieu = item.BietHieu,
                NgayBatDau = item.NgayBatDau,
                NgayNghiViec = item.NgayNghiViec,
                LoginName = item.LoginName,
                Password = item.Password,
                NgaySinh = item.NgaySinh,
                RoleId = item.RoleId,
                DienThoai = item.DienThoai,
                DiaChi = item.DiaChi,
                Status = item.Status

            };
        }



        public async Task<PagedResult<NhanVienResult>> getAllNhanVienPaging(NhanVienRequest request)
        {

            var Result = new PagedResult<NhanVienResult>() { };
            var ResultItem = new List<NhanVienResult>();

            var DonhangQerry = _context.NhanViens.AsNoTracking().Include(e => e.IdViTri).Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                IdViTri = e.IdViTri,
                ViTriNhanVien = e.ViTriNhanVien.TenViTri,
                Ho = e.Ho,
                Ten = e.Ten,
                HoTen = e.Ho + e.Ten,
                BietHieu = e.BietHieu,
                NgayBatDau = e.NgayBatDau,
                NgayNghiViec = e.NgayNghiViec,
                LoginName = e.LoginName,
                Password = e.Password,
                NgaySinh = e.NgaySinh,
                DienThoai = e.DienThoai,
                DiaChi = e.DiaChi,
                Status = e.Status
    }).ToListAsync();



            //// Filter

            ////test danh sách lấy thông báo theo thời gian
            //if (!string.IsNullOrEmpty(dayss) || !string.IsNullOrWhiteSpace(dayss))
            //{
            //    DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
            //    DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
            //    RawDonhang = RawDonhang.Where(e =>
            //        e.NgayBatDau < oldDate)
            //        .ToList();
            //}

            // Filter theo mã đơn hàng tên nhân viên và tên đối tác 
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                RawDonhang = RawDonhang.Where(e =>
                    e.HoTen.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.Ho.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.Ten.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.BietHieu.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.LoginName.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.ViTriNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
                    .ToList();
            }
            //// Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            //if (request.IdDoiTac != null)
            //{
            //    RawDonhang = RawDonhang
            //        .Where(e => e.IdDoiTac == request.IdDoiTac)
            //        .ToList();
            //}


            //if (request.IdNhanVien != null)
            //{
            //    RawDonhang = RawDonhang
            //        .Where(e => e.IdNhanVien == request.IdNhanVien)
            //        .ToList();
            //}



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
                var resultItem = new NhanVienResult
                {
                    Id = item.Id,
                    IdViTri = item.IdViTri,
                    ViTriNhanVien = item.ViTriNhanVien,
                    Ho = item.Ho,
                    Ten = item.Ten,
                    HoTen = item.HoTen,
                    BietHieu = item.BietHieu,
                    NgayBatDau = item.NgayBatDau,
                    NgayNghiViec = item.NgayNghiViec,
                    LoginName = item.LoginName,
                    Password = item.Password,
                    NgaySinh = item.NgaySinh,
                    DienThoai = item.DienThoai,
                    DiaChi = item.DiaChi,
                    Status = item.Status

                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<NhanVienResult>
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
