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
    public class HoaDonService : IHoaDonService
    {
        private readonly TH_DbConotext _context;
        public HoaDonService(TH_DbConotext context)
        {
            _context = context;
        }

        public async Task<HoaDon> FetchAsync(long id)
        {
            return await _context.HoaDons.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }
        //public async Task<HoaDon> FetchAsync(long id)
        //{
        //    return await _context.HoaDons.FindAsync(id);
        //}

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            product.GhiChu = $" {productid} Số di động đã bị xóa";
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<HoaDonResult> GetAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sản phẩm");
            }
            string MaDonHang = null;
            string MaDatHang = null;
            long? idDonHang = null;
            long? idDatHang = null;

            if (item.IdDonHang != null)
            {
                MaDonHang = _context.DonHangs.Where(e => e.Id == item.IdDonHang).First().MaDonHang;
                idDonHang = _context.DonHangs.Where(e => e.Id == item.IdDonHang).First().Id;
            }
            if (item.IdDatHang != null)
            {
                idDatHang = _context.DatHangs.Where(e => e.Id == item.IdDatHang).First().Id;
                MaDatHang = _context.DatHangs.Where(e => e.Id == item.IdDatHang).First().MaDonHang;
            }
            return new HoaDonResult
            {
                Id = item.Id,
                IdDonHang = item.IdDonHang,
                MaDonHang = MaDonHang,
                MaDatHang = MaDatHang,
                IdDatHang = item.IdDatHang,
                IdDonHangTrue = idDonHang,
                IdDatHangTrue = idDatHang,
                SoHD = item.SoHD,
                Ngay = item.Ngay,
                GiaTri = item.GiaTri,
                LoaiHoaDon = item.LoaiHoaDon,
                BillStatus = item.BillStatus,
                GhiChu = item.GhiChu,
                Status = item.Status
            };
        }

        public async Task<List<HoaDonResult>> GetAsync()
        {
            var result = new List<HoaDonResult>();


            var list = await _context.HoaDons.Where(e => e.Status == 0)
                .ToListAsync();


            foreach (var item in list.ToList())
            {
                result.Add(new HoaDonResult
                {
                    Id = item.Id,
                    IdDonHang = item.IdDonHang,
                    IdDatHang = item.IdDatHang,
                    SoHD = item.SoHD,
                    Ngay = item.Ngay,
                    GiaTri = item.GiaTri,
                    LoaiHoaDon = item.LoaiHoaDon,
                    GhiChu = item.GhiChu,
                    Status = item.Status
                });
            }

            return result;
        }

        public async Task<PagedResult<HoaDonResult>>  getAllHoaDon_DatHangPaging(HoaDonRequest request, string getNotification)
        {

            var Result = new PagedResult<HoaDonResult>() { };
            var ResultItem = new List<HoaDonResult>();

            var HoaDonQerrys = _context.HoaDons.Where(e => e.Status == 0 && e.IdDatHang != null);


            var RawDonhang = await HoaDonQerrys.Select(e => new
            {
                Id = e.Id,
                IdDatHang = e.IdDatHang,
                SoHD = e.SoHD,
                Ngay = e.Ngay,
                GiaTri = e.GiaTri,
                LoaiHoaDon = e.LoaiHoaDon,
                TrangThaiHoaDon = e.BillStatus,
                GhiChu = e.GhiChu,
                Status = e.Status
    }).ToListAsync();



            // Filter
            //test danh sách lấy thông báo theo thời gian
            if (!string.IsNullOrEmpty(getNotification) || !string.IsNullOrWhiteSpace(getNotification))
            {
                DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
                DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
                RawDonhang = RawDonhang.Where(e =>
                    e.Ngay < oldDate)
                    .ToList();
            }
            //test danh sách lấy từ ngày đến ngày 
            if (request.FromDate != null && request.ToDate != null)
            {
                RawDonhang = RawDonhang.Where(e =>
                 e.Ngay >= request.FromDate && e.Ngay <= request.ToDate)
                    .ToList();
            }

            #region Filter Lọc kiểu trước khi ra Result
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
            if (request.IdDatHang != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdDatHang == request.IdDatHang)
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

            var DonHangQerrynews = _context.DonHangs.Where(e => e.Status == 0);
            var DatHangQerrynews = _context.DatHangs.Where(e => e.Status == 0);

            long? DatHangId = null;
            string MaDatHang = null;
            foreach (var item in donhangs)
            {
                foreach (var i in DatHangQerrynews)
                {
                    if (item.IdDatHang == null)
                    {
                        DatHangId = null;
                        MaDatHang = null;
                    }
                    if (i.Id == item.IdDatHang)
                    {
                        DatHangId = i.Id;
                        MaDatHang = i.MaDonHang;
                    }

                }
                var resultItem = new HoaDonResult
                    {
                        Id = item.Id,
                        IdDatHang = DatHangId,
                        IdDatHangTrue = item.IdDatHang,
                        MaDatHang = MaDatHang,
                        SoHD = item.SoHD,
                        Ngay = item.Ngay,
                        GiaTri = item.GiaTri,
                        LoaiHoaDon = item.LoaiHoaDon,
                        BillStatus = item.TrangThaiHoaDon,
                        GhiChu = item.GhiChu,
                        Status = item.Status
                     };
                ResultItem.Add(resultItem);
            }
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                ResultItem = ResultItem.Where(e =>
                e.SoHD.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.MaDatHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();

            }

            return new PagedResult<HoaDonResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }

        public async Task<PagedResult<HoaDonResult>> getAllHoaDon_DonHangPaging(HoaDonRequest request, string getNotification)
        {

            var Result = new PagedResult<HoaDonResult>() { };
            var ResultItem = new List<HoaDonResult>();

            var HoaDonQerrys = _context.HoaDons.Where(e => e.Status == 0 && e.IdDonHang != null);



            var RawDonhang = await HoaDonQerrys.Select(e => new
            {
                Id = e.Id,
                IdDonHang = e.IdDonHang,
                IdDatHang = e.IdDatHang,
                //MaDonHang = e.DonHang.MaDonHang,
                //MaDatHang = e.DatHang.MaDonHang,
                SoHD = e.SoHD,
                Ngay = e.Ngay,
                GiaTri = e.GiaTri,
                LoaiHoaDon = e.LoaiHoaDon,
                TrangThaiHoaDon = e.BillStatus,
                GhiChu = e.GhiChu,
                Status = e.Status
            }).ToListAsync();



            // Filter
            //test danh sách lấy thông báo theo thời gian
            if (!string.IsNullOrEmpty(getNotification) || !string.IsNullOrWhiteSpace(getNotification))
            {
                DateTime noww = new DateTime(2020, 01, 30, 0, 0, 0, 0);
                DateTime oldDate = noww.Subtract(new TimeSpan(7, 0, 0, 0, 0));
                RawDonhang = RawDonhang.Where(e =>
                    e.Ngay < oldDate)
                    .ToList();
            }
            //test danh sách lấy từ ngày đến ngày 
            if (request.FromDate != null && request.ToDate != null)
            {
                RawDonhang = RawDonhang.Where(e =>
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

            var DonHangQerrynews = _context.DonHangs.Where(e => e.Status == 0);

            long? DonHangId = null;
            string MaDonHang = null;

            foreach (var item in donhangs)
            {
                foreach (var i in DonHangQerrynews)
                {
                    if (item.IdDonHang == null)
                    {
                        DonHangId = null;
                        MaDonHang = "Không thuộc đơn hàng";
                    }
                    if (i.Id == item.IdDonHang)
                    {
                        DonHangId = i.Id;
                        MaDonHang = i.MaDonHang;
                    }

                }

                var resultItem = new HoaDonResult
                {
                    Id = item.Id,
                    IdDonHang = item.IdDonHang,
                    IdDonHangTrue = DonHangId,
                    MaDonHang = MaDonHang,
                    SoHD = item.SoHD,
                    Ngay = item.Ngay,
                    GiaTri = item.GiaTri,
                    LoaiHoaDon = item.LoaiHoaDon,
                    BillStatus = item.TrangThaiHoaDon,
                    GhiChu = item.GhiChu,
                    Status = item.Status

                };
                ResultItem.Add(resultItem);
            }
            var resultItems = ResultItem.Where(e => e.Status == 0).ToList();


            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                resultItems = ResultItem.Where(e =>
                    e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())||
                    e.SoHD.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();
            }
            return new PagedResult<HoaDonResult>
            {
                Items = resultItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }
        public async Task<PagedResult<HoaDonResult>> getAllHoaDonPaging(HoaDonRequest request)
        {

            var Result = new PagedResult<HoaDonResult>() { };
            var ResultItem = new List<HoaDonResult>();

            var HoaDonQerrys = _context.HoaDons.Where(e => e.Status == 0);



            var RawDonhang = await HoaDonQerrys.Select(e => new
            {
                Id = e.Id,
                IdDonHang = e.IdDonHang,
                IdDatHang = e.IdDatHang,
                //MaDonHang = e.DonHang.MaDonHang,
                //MaDatHang = e.DatHang.MaDonHang,
                SoHD = e.SoHD,
                Ngay = e.Ngay,
                GiaTri = e.GiaTri,
                LoaiHoaDon = e.LoaiHoaDon,
                TrangThaiHoaDon = e.BillStatus,
                GhiChu = e.GhiChu,
                Status = e.Status
            }).ToListAsync();



            // Filter

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
            if (request.IdDatHang != null)
            {
                RawDonhang = RawDonhang
                    .Where(e => e.IdDatHang == request.IdDatHang)
                    .ToList();
            }

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

            var DonHangQerrynews = _context.DonHangs.Where(e => e.Status == 0);
            var DatHangQerrynews = _context.DatHangs.Where(e => e.Status == 0);

            long? DonHangId = null;
            string MaDonHang = null;
            long? DatHangId = null;
            string MaDatHang = null;

            foreach (var item in donhangs)
            {
                foreach (var i in DonHangQerrynews)
                {
                    if (item.IdDonHang == null)
                    {
                        DonHangId = null;
                        MaDonHang = "Không thuộc đơn hàng";
                    }
                    if (i.Id == item.IdDonHang)
                    {
                        DonHangId = i.Id;
                        MaDonHang = i.MaDonHang;
                    }

                }
                foreach (var i in DatHangQerrynews)
                {
                    if (item.IdDatHang == null)
                    {
                        DatHangId = null;
                        MaDatHang = "Không thuộc đặt hàng";
                    }
                    if (i.Id == item.IdDatHang)
                    {
                        DatHangId = i.Id;
                        MaDatHang = i.MaDonHang;
                    }

                }
                var resultItem = new HoaDonResult
                {


                    Id = item.Id,
                    IdDonHang = item.IdDonHang,
                    IdDonHangTrue = DonHangId,
                    MaDonHang = MaDonHang,
                    IdDatHang = DatHangId,
                    IdDatHangTrue = item.IdDatHang,
                    MaDatHang = MaDatHang,
                    SoHD = item.SoHD,
                    Ngay = item.Ngay,
                    GiaTri = item.GiaTri,
                    LoaiHoaDon = item.LoaiHoaDon,
                    BillStatus = item.TrangThaiHoaDon,
                    GhiChu = item.GhiChu,
                    Status = item.Status

                };
                ResultItem.Add(resultItem);
            }
            var resultItems = ResultItem.Where(e => e.Status == 0).ToList();


            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {

                resultItems = ResultItem.Where(e => e.Status == 0).ToList();

                resultItems = ResultItem.Where(e =>
                    e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                e.MaDatHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();
                //||
                //e.MaDonHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                //e.MaDatHang.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()))
            }
            return new PagedResult<HoaDonResult>
            {
                Items = resultItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }


        //sửa sản phẩm
        //public async Task EditAsync(long id, DonHangEdit args)
        //{
        //    var product = await FetchAsync(id);
        //    if (product == null)
        //    {
        //        throw new InvalidOperationException($"Đơn đặt hàng không tồn tại hoặc đã bị xóa");
        //    }
        //    if (args.IdNhanVien.HasValue)
        //    {
        //        // check bảng cha
        //        {
        //            var checkCate = _context.NhanViens
        //                .AsNoTracking()
        //                .Where(x => x.Id == args.IdNhanVien && x.Status == Statuses.Default)
        //                .FirstOrDefault();

        //            if (checkCate == null)
        //            {
        //                throw new InvalidOperationException($"Không tìm thấy nhân viên");
        //            }
        //        }
        //    }
        //    if (args.IdDoiTac.HasValue)
        //    {
        //        // check bảng cha
        //        {
        //            var checkCate = _context.DoiTacs
        //                .AsNoTracking()
        //                .Where(x => x.Id == args.IdDoiTac && x.Status == Statuses.Default)
        //                .FirstOrDefault();

        //            if (checkCate == null)
        //            {
        //                throw new InvalidOperationException($"Không tìm thấy dối tác này");
        //            }
        //        }
        //    }


        //    //if (!string.IsNullOrEmpty(args.ImagePath) || !string.IsNullOrWhiteSpace(args.ImagePath))
        //    //{ product.ImagePath = args.ImagePath; }

        //    //if (!string.IsNullOrEmpty(args.VideoPath) || !string.IsNullOrWhiteSpace(args.VideoPath))
        //    //{ product.VideoPath = args.VideoPath; }

        //    // update query for string
        //    if (!string.IsNullOrEmpty(args.MaDonHang) || !string.IsNullOrWhiteSpace(args.MaDonHang))
        //    {
        //        var checkProd = await _context.DonHangs.Where(x => x.MaDonHang == args.MaDonHang && x.Id != id).FirstOrDefaultAsync();
        //        if (checkProd != null)
        //        {
        //            throw new InvalidOperationException($"Đã tồn tại sản phẩm {args.MaDonHang}");
        //        }
        //        product.MaDonHang = args.MaDonHang;
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
        //    if (args.NgayBatDau.HasValue) { product.NgayBatDau = args.NgayBatDau.Value; }
        //    if (args.NgayGiaoHang.HasValue) { product.NgayGiaoHang = args.NgayGiaoHang.Value; }
        //    if (args.TrangThai != null) { product.TrangThai = args.TrangThai; }

        //    // update kể cả khi request truyền null 
        //    _context.Entry(product).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    //await versionService.NewVersion(ObjectVersions.Product, product.Id);



        //}




    }
}
