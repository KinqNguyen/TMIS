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
    public class QuyTienMatService : IQuyTienMatService
    {
        private readonly TH_DbConotext _context;
        public QuyTienMatService(TH_DbConotext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        //public async Task<QuyTienMat> FetchAsync(long id)
        //{
        //    return await _context.QuyTienMats.FindAsync(id);
        //}

        public async Task<QuyTienMat> FetchAsync(long id)
        {
            return await _context.QuyTienMats.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<QuyTienMatResult> GetAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sản phẩm");
            }
            string MaHoaDon = null;

            if (item.IdHoaDon != null)
            {
                MaHoaDon = _context.HoaDons.Where(e => e.Id == item.IdHoaDon).First().SoHD;
            }

            return new QuyTienMatResult
            {
                Id = item.Id,
                IdHoaDon = item.IdHoaDon,
                IdNhanVien = item.NhanVien.Id,
                IdNhanVienTrue = item.IdNhanVien,
                TenCongTy = _context.HoaDons.Where(e => e.Id == item.IdHoaDon).First().SoHD,
                HoTenNhanVien = item.NhanVien.Ten + " " + item.NhanVien.Ho,
                SoHoaDon = MaHoaDon,
                Ngay = item.Ngay,
                Slug = item.Slug,
                NoiDung = item.NoiDung,
                IOStatuses = item.IOStatuses,
                Status = item.Status,
            };
        }



        //public async Task<QuyTienMatResult> GetDonHangAsync(long productid)
        //{
        //    var item = await this.FetchAsync(productid);
        //    if (item == null)
        //    {
        //        throw new InvalidOperationException($"Không tìm thấy Đơn đặt hàng");
        //    }
        //    return new QuyTienMatResult
        //    {
        //        Id = item.Id,
        //        IdHoaDon = item.IdHoaDon,
        //        IdHoaDonTrue = HoaDonId,
        //        IdNhanVien = item.IdNhanVien,
        //        IdNhanVienTrue = NhanVienId,
        //        HoTenNhanVien = TenNhanVien,
        //        SoHoaDon = SoHoaDon,
        //        Ngay = item.Ngay,
        //        Slug = item.Slug,
        //        NoiDung = item.NoiDung,
        //        IOStatuses = item.IOStatuses,
        //        Status = item.Status,

        //    };
        //}


        public async Task EditAsync(long id, QuyTienMatEdit args)
        {
            var product = await FetchAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"QTM này không tồn tại trong CSDL hoặc đã bị xóa");
            }
            if (args.IdNhanVien.HasValue)
            {
                // check bảng cha
                {
                    var checkCate = _context.NhanViens
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdNhanVien && x.Status != Statuses.Deleted)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy nhân viên");
                    }
                }
            }
            if (args.IdHoaDon.HasValue)
            {
                // check bảng cha
                {
                    var checkCate = _context.HoaDons
                        .AsNoTracking()
                        .Where(x => x.Id == args.IdHoaDon && x.Status != Statuses.Deleted)
                        .FirstOrDefault();

                    if (checkCate == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy hóa đơn");
                    }
                }
            }


            //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
            //{ product.ImagePath = args.ImagePath; }

            //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
            //{ product.VideoPath = args.VideoPath; }

            // update query for string


            if (!string.IsNullOrEmpty(args.Slug) || !string.IsNullOrWhiteSpace(args.Slug))
            { product.Slug = args.Slug; }
            if (!string.IsNullOrEmpty(args.NoiDung) || !string.IsNullOrWhiteSpace(args.NoiDung))
            { product.NoiDung = args.NoiDung; }



            //update query for int 
            if (args.IdHoaDon != null) { product.IdHoaDon = args.IdHoaDon.Value; }
            if (args.IdNhanVien != null) { product.IdNhanVien = args.IdNhanVien.Value; }
            if (args.IOStatuses != null) { product.IOStatuses = args.IOStatuses; }



            // update kể cả khi request truyền null 
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //await versionService.NewVersion(ObjectVersions.Product, product.Id);



        }





        public async Task<PagedResult<QuyTienMatResult>> getAllQuyTienMatPaging(QuyTienMatRequest request)
        {

            var Result = new PagedResult<QuyTienMatResult>() { };
            var ResultItem = new List<QuyTienMatResult>();

            var QuyTienMatQerrys = _context.QuyTienMats.Where(e => e.Status == 0);



            var RawQuyTienMat = await QuyTienMatQerrys.Select(e => new
            {
                Id = e.Id,
                IdHoaDon = e.IdHoaDon,
                IdNhanVien = e.IdNhanVien,
                Ngay = e.Ngay,
                Slug = e.Slug,
                NoiDung = e.NoiDung,
                IOStatuses = e.IOStatuses,
                Status = e.Status,
             }).ToListAsync();



            // Filter
            if (request.FromDate != null && request.ToDate != null)
            {
                RawQuyTienMat = RawQuyTienMat.Where(e =>
                 e.Ngay >= request.FromDate && e.Ngay <= request.ToDate)
                    .ToList();
            }
            #region Lọc kiểu cũ
            ////test danh sách lấy thông báo theo thời gian
            //if (!string.IsNullOrEmpty(dayss) || !string.IsNullOrWhiteSpace(dayss))
            //{
            //    DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
            //    DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
            //    RawDonhang = RawDonhang.Where(e =>
            //        e.Ngay < oldDate)
            //        .ToList();
            //}

            // Filter theo mã đơn hàng tên nhân viên và tên đối tác 
            //if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            //{
            //    RawDonhang = RawDonhang.Where(e =>
            //        e.SoHD.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();
            //    //||
            //    //e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
            //    //e.MaDatHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))

            //}

            #endregion

            // Lọc theo thể loại -- chính xác hơn ở đây là đang lọc theo id của đối tác hoặc id nhân viên
            if (request.IdHoaDon != null)
            {
                RawQuyTienMat = RawQuyTienMat
                    .Where(e => e.IdHoaDon == request.IdHoaDon)
                    .ToList();
            }
            if (request.IdNhanVien != null)
            {
                RawQuyTienMat = RawQuyTienMat
                    .Where(e => e.IdNhanVien == request.IdNhanVien)
                    .ToList();
            }


            RawQuyTienMat = RawQuyTienMat
                    .Where(e => e.IOStatuses == request.IOStatuses)
                    .ToList();


            var donhangs = RawQuyTienMat
              .OrderBy(e => e.Id)
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToList();

            var totalRecords = RawQuyTienMat.Count();
            var rawTotalPages = ((double)totalRecords / (double)request.PageSize);
            var totalPages = (int)Math.Round(rawTotalPages);
            if (rawTotalPages > totalPages)
            {
                totalPages += 1;
            }

            var HoaDonQerrynews = _context.HoaDons.Where(e => e.Status == 0);
            var NhanVienQerrynews = _context.NhanViens.Where(e => e.Status == 0);

            long? HoaDonId = null;
            string SoHoaDon = null;
            long? NhanVienId = null;
            string TenNhanVien = null;

            foreach (var item in donhangs)
            {
                foreach (var i in HoaDonQerrynews)
                {
                    if (item.IdHoaDon == null)
                    {
                        HoaDonId = null;
                        SoHoaDon = "Không có thuộc hóa đơn";
                    }
                    if (i.Id == item.IdHoaDon)
                    {
                        HoaDonId = i.Id;
                        SoHoaDon = i.SoHD;
                    }

                }
                foreach (var i in NhanVienQerrynews)
                {
                    if (i.Id == item.IdNhanVien)
                    {
                        NhanVienId = i.Id;
                        var ho = i.Ho;
                        var ten = i.Ten;
                        TenNhanVien = ho + " " + ten;
                    }

                }

                var resultItem = new QuyTienMatResult
                {


                    Id = item.Id,
                    IdHoaDon = item.IdHoaDon,
                    IdHoaDonTrue = HoaDonId,
                    IdNhanVien = item.IdNhanVien,
                    IdNhanVienTrue = NhanVienId,
                    HoTenNhanVien = TenNhanVien,
                    SoHoaDon = SoHoaDon,
                    Ngay = item.Ngay,
                    Slug = item.Slug,
                    NoiDung = item.NoiDung,
                    IOStatuses = item.IOStatuses,
                    Status = item.Status,

                };
                ResultItem.Add(resultItem);
            }
            var resultItems = ResultItem.Where(e => e.Status == 0).ToList();


            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {

                resultItems = ResultItem.Where(e => e.Status == 0).ToList();

                resultItems = ResultItem.Where(e =>
                    e.Slug.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                e.SoHoaDon.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                e.HoTenNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();
                //||
                //e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                //e.MaDatHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
            }
            return new PagedResult<QuyTienMatResult>
            {
                Items = resultItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }


    }
}
