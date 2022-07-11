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
    public class DoiTacService : IDoiTacService
    {
        private readonly TH_DbConotext _context;
        public DoiTacService(TH_DbConotext context)
        {
            _context = context;
        }

        public async Task<DoiTac> FetchAsync(long id)
        {
            return await _context.DoiTacs.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            product.TenCongTy = $" {productid} Số di động đã bị xóa";
            product.GhiChu = $" {productid} Số di động đã bị xóa";
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<DoiTacResult> GetDoitacAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy Đơn đặt hàng");
            }
            return new DoiTacResult
            {
                Id = item.Id,
                TongCongNo = item.TongCongNo,
                TenCongTy = item.TenCongTy,
                MaDoiTac = item.MaDoiTac,
                SanPhamDichVu = item.SanPhamDichVu,
                MaSoThue = item.MaSoThue,
                DienThoai = item.DienThoai,
                GhiChu = item.GhiChu,
                DiaChi = item.DiaChi,
                Status = item.Status

            };
        }

        public async Task<PagedResult<DoiTacResult>> getAllDoiTacPaging(DoiTacRequest request)
        {

            var Result = new PagedResult<DoiTacResult>() { };
            var ResultItem = new List<DoiTacResult>();

            var DonhangQerry = _context.DoiTacs.AsNoTracking().Where(e => e.Status == Data.Enums.Statuses.Default);

            var RawDonhang = await DonhangQerry.Select(e => new
            {
                Id = e.Id,
                TongCongNo = e.TongCongNo,
                TenCongTy = e.TenCongTy,
                MaDoiTac = e.MaDoiTac,
                SanPhamDichVu = e.SanPhamDichVu,
                MaSoThue = e.MaSoThue,
                DienThoai = e.DienThoai,
                GhiChu = e.GhiChu,
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
                    e.MaDoiTac.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.DienThoai.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.SanPhamDichVu.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.TenCongTy.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.MaSoThue.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
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
                var resultItem = new DoiTacResult
                {
                    Id = item.Id,
                    TongCongNo = item.TongCongNo,
                    TenCongTy = item.TenCongTy,
                    MaDoiTac = item.MaDoiTac,
                    SanPhamDichVu = item.SanPhamDichVu,
                    MaSoThue = item.MaSoThue,
                    DienThoai = item.DienThoai,
                    GhiChu = item.GhiChu,
                    DiaChi = item.DiaChi,
                    Status = item.Status

                };
                ResultItem.Add(resultItem);
            }

            return new PagedResult<DoiTacResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }


        public async Task CreateDoiTacAsync(DoiTacEdit args)
        {
            
            var prod = await _context.DatHangs.OrderByDescending(x => x.Id).AsNoTracking().Where(x => x.Status != Statuses.Deleted).FirstOrDefaultAsync();
            long bro = prod.Id + 1;

            var product = new DoiTac
            {
                TenCongTy = args.TenCongTy,
                MaDoiTac = args.MaDoiTac,
                SanPhamDichVu = args.SanPhamDichVu,
                MaSoThue = args.MaSoThue,
                DienThoai = args.DienThoai,
                DiaChi = args.DiaChi,
                Slug = args.Slug,

                GhiChu = args.GhiChu,
                Status = Statuses.Default,
            };

            _context.DoiTacs.Add(product);

            await _context.SaveChangesAsync();

        }

        //sửa sản phẩm
        public async Task EditAsync(long id, DoiTacEdit args)
        {
            var product = await FetchAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Đơn đặt hàng không tồn tại hoặc đã bị xóa");
            }
            //if (args.IdLoaiDoiTac.HasValue)
            //{
            //    // check bảng cha
            //    {
            //        var checkCate = _context.LoaiDoiTacs
            //            .AsNoTracking()
            //            .Where(x => x.Id == args.IdLoaiDoiTac && x.Status == Statuses.Default)
            //            .FirstOrDefault();

            //        if (checkCate == null)
            //        {
            //            throw new InvalidOperationException($"Không tìm thấy loại đối tác");
            //        }
            //        var prod_cate = _context.DoiTac_va_LoaiDoiTacs.AsNoTracking().Where(x => x.IdDoiTac == id && x.IdLoaiDoiTac == checkCate.Id).FirstOrDefault();
            //        if (prod_cate == null)
            //        {
            //            var DoiTac_va_LoaiDoiTac = new DoiTac_va_LoaiDoiTac
            //            {
            //                IdLoaiDoiTac = args.IdLoaiDoiTac.Value,
            //                IdDoiTac = id
            //            };
            //            _context.DoiTac_va_LoaiDoiTacs.Add(DoiTac_va_LoaiDoiTac);
            //            await _context.SaveChangesAsync();
            //        }
            //        prod_cate.IdLoaiDoiTac = args.IdLoaiDoiTac.Value;
            //        _context.Entry(checkCate).State = EntityState.Modified;
            //        await _context.SaveChangesAsync();
            //    }

            //}



            //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
            //{ product.ImagePath = args.ImagePath; }

            //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
            //{ product.VideoPath = args.VideoPath; }

            // update query for string
            if (!string.IsNullOrEmpty(args.MaDoiTac) || !string.IsNullOrWhiteSpace(args.MaDoiTac))
            {
                var checkProd = await _context.DoiTacs.Where(x => x.MaDoiTac == args.MaDoiTac && x.Id != id).FirstOrDefaultAsync();
                if (checkProd != null)
                {
                    throw new InvalidOperationException($"Đã tồn tại sản phẩm {args.MaDoiTac}");
                }
                product.MaDoiTac = args.MaDoiTac;
            }

            if (!string.IsNullOrEmpty(args.SanPhamDichVu) || !string.IsNullOrWhiteSpace(args.SanPhamDichVu))
            { product.SanPhamDichVu = args.SanPhamDichVu; }
            if (!string.IsNullOrEmpty(args.MaSoThue) || !string.IsNullOrWhiteSpace(args.MaSoThue))
            { product.MaSoThue = args.MaSoThue; }///
            if (!string.IsNullOrEmpty(args.Slug) || !string.IsNullOrWhiteSpace(args.Slug))
            { product.Slug = args.Slug; }
            if (!string.IsNullOrEmpty(args.GhiChu) || !string.IsNullOrWhiteSpace(args.GhiChu))
            { product.GhiChu = args.GhiChu; }
            if (!string.IsNullOrEmpty(args.DiaChi) || !string.IsNullOrWhiteSpace(args.DiaChi))
            { product.DiaChi = args.DiaChi; }


            //update query for int 

            // update kể cả khi request truyền null 
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //await versionService.NewVersion(ObjectVersions.Product, product.Id);



        }




    }
}
