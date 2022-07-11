using SscKiosk.Kitchen.Api.Data.Utils;
using Stump.Api.Utils;
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

        //sửa sản phẩm
        public async Task EditAsync(long id, NhanVienEdit args)
        {
            var product = await FetchAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Nhân viên này không tồn tại trong CSDL hoặc đã bị xóa");
            }
            if (args.IdViTri.HasValue)
            {
                // check bảng cha
                {
                    var checkCate = _context.ViTriNhanViens
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdViTri)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy nhân viên");
                    }
                }
            }


            //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
            //{ product.ImagePath = args.ImagePath; }

            //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
            //{ product.VideoPath = args.VideoPath; }

            // update query for string


            if (!string.IsNullOrEmpty(args.Ho) || !string.IsNullOrWhiteSpace(args.Ho))
            { product.Ho = args.Ho; }
            if (!string.IsNullOrEmpty(args.Ten) || !string.IsNullOrWhiteSpace(args.Ten))
            { product.Ten = args.Ten; }
            if (!string.IsNullOrEmpty(args.BietHieu) || !string.IsNullOrWhiteSpace(args.BietHieu))
            { product.BietHieu = args.BietHieu; }
            if (!string.IsNullOrEmpty(args.DiaChi) || !string.IsNullOrWhiteSpace(args.DiaChi))
            { product.DiaChi = args.DiaChi; }
            if (!string.IsNullOrEmpty(args.DienThoai) || !string.IsNullOrWhiteSpace(args.DienThoai))
            { product.DienThoai = args.DienThoai; }
            if (!string.IsNullOrEmpty(args.LoginName) || !string.IsNullOrWhiteSpace(args.LoginName))
            { product.LoginName = args.LoginName; }
            if (!string.IsNullOrEmpty(args.Password) || !string.IsNullOrWhiteSpace(args.Password))
            { product.Password = args.Password.ToSha256(); }


            //update query for int 
            if (args.IdViTri != null) { product.IdViTri = args.IdViTri.Value; }
            if (args.RoleId != null) { product.RoleId = args.RoleId; }
            if (args.NgaySinh != null) { product.NgaySinh = args.NgaySinh; }
            if (args.NgayBatDau != null) { product.NgayBatDau = args.NgayBatDau.Value; }
            if (args.NgayNghiViec != null) { product.NgayNghiViec = args.NgayNghiViec; }


            // update kể cả khi request truyền null 
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //await versionService.NewVersion(ObjectVersions.Product, product.Id);



        }

        public async Task<long> CreateAsync(NhanVienEdit args)
        {


            var checkProd = await _context.NhanViens.Where(x => x.LoginName == args.LoginName).FirstOrDefaultAsync();
            if (checkProd != null)
            {

                throw new InvalidOperationException($"Đã tồn tại tên đăng nhập có mã là :  {args.LoginName}");
            }

            var product = new NhanVien
            {
                IdViTri = args.IdViTri.Value,
                RoleId = args.RoleId,
                Ho = args.Ho,
                Ten = args.Ten,
                BietHieu = args.BietHieu,
                NgaySinh = args.NgaySinh.Value,
                DiaChi = args.DiaChi,
                DienThoai = args.DienThoai,
                Slug = args.Slug,
                NgayBatDau = args.NgayBatDau.Value,
                NgayNghiViec = args.NgayNghiViec.Value,
                LoginName = args.LoginName,
                Password = args.Password.ToSha256(),
                Status = Statuses.Default,
            };

            _context.NhanViens.Add(product);

            await _context.SaveChangesAsync();


            return product.Id;
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
