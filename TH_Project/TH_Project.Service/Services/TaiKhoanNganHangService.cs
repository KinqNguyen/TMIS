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
    public class TaiKhoanNganHangService : ITaiKhoanNganHangService
    {
        private readonly TH_DbConotext _context;
        public TaiKhoanNganHangService(TH_DbConotext context)
        {
            _context = context;
        }

        //public async Task<TaiKhoanNganHang> FetchAsync(long id)
        //{
        //    return await _context.TaiKhoanNganHangs.FindAsync(id);
        //}

        public async Task DeleteAsync(long productid)
        {
            var product = await FetchAsync(productid);

            product.Status = Statuses.Deleted;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<TaiKhoanNganHang> FetchAsync(long id)
        {
            return await _context.TaiKhoanNganHangs.AsNoTracking().Where(x => x.Status != Statuses.Deleted && x.Id == id).FirstOrDefaultAsync();
        }



        public async Task<TaiKhoanNganHangNhanVienResult> GetTKNHNhanVienAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sản phẩm");
            }
            string TenNhanVien = null;

            if (item.IdNhanVIen != null)
            {
                string ho = _context.NhanViens.Where(e => e.Id == item.IdNhanVIen).First().Ho;
                string ten = _context.NhanViens.Where(e => e.Id == item.IdNhanVIen).First().Ten;
                TenNhanVien = ho + " " +ten;
            }

            return new TaiKhoanNganHangNhanVienResult
            {
                Id = item.Id,
                IdNhanVien = item.IdNhanVIen,
                HoTenNhanVien = TenNhanVien,
                Ten = item.Ten,
                STK = item.STK,
                DiaChiNH = item.DiaChiNH,
                Slug = item.Slug,
                GhiChu = item.GhiChu,
                Status = item.Status
            };
        }



        public async Task<TaiKhoanNganHangDoiTacResult> GetTKNHDoiTacAsync(long productid)
        {
            var item = await this.FetchAsync(productid);
            if (item == null)
            {
                throw new InvalidOperationException($"Không tìm thấy sản phẩm");
            }
            string TenCongTy = null;

            if (item.IdDoiTac != null)
            {
                TenCongTy = _context.DoiTacs.Where(e => e.Id == item.IdDoiTac).First().TenCongTy;
            }

            return new TaiKhoanNganHangDoiTacResult
            {
                Id = item.Id,
                IdDoiTac = item.IdDoiTac,
                TenCongTyDoiTac = TenCongTy,
                Ten = item.Ten,
                STK = item.STK,
                DiaChiNH = item.DiaChiNH,
                Slug = item.Slug,
                GhiChu = item.GhiChu,
                Status = item.Status
            };
        }



        public async Task<PagedResult<TaiKhoanNganHangNhanVienResult>> getAllTaiKhoan_NhanVienPaging(TaiKhoanNganHangNhanVienRequest request)
        {

            var Result = new PagedResult<TaiKhoanNganHangNhanVienResult>() { };
            var ResultItem = new List<TaiKhoanNganHangNhanVienResult>();

            var TaiKhoanQerrys = _context.TaiKhoanNganHangs.Where(e => e.Status == 0 && e.IdNhanVIen != null);


            var RawTaiKhoan = await TaiKhoanQerrys.Select(e => new
            {
                Id = e.Id,
                IdNhanVIen = e.IdNhanVIen,
                Ten = e.Ten,
                STK = e.STK,
                DiaChiNH = e.DiaChiNH,
                Slug = e.Slug,
                GhiChu = e.GhiChu,
                Status = e.Status

    }).ToListAsync();



            // Filter

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
            if (request.IdNhanVien != null)
            {
                RawTaiKhoan = RawTaiKhoan
                    .Where(e => e.IdNhanVIen == request.IdNhanVien)
                    .ToList();
            }

            var donhangs = RawTaiKhoan
              .OrderBy(e => e.Id)
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToList();

            var totalRecords = RawTaiKhoan.Count();
            var rawTotalPages = ((double)totalRecords / (double)request.PageSize);
            var totalPages = (int)Math.Round(rawTotalPages);
            if (rawTotalPages > totalPages)
            {
                totalPages += 1;
            }

            var NhanVienQerrynews = _context.NhanViens.Where(e => e.Status == 0);


            long? NhanVienId = null;
            string HoTenNhanVien = null;


            foreach (var item in donhangs)
            {
                foreach (var i in NhanVienQerrynews)
                {
                    if (item.IdNhanVIen == null)
                    {
                        NhanVienId = null;
                        HoTenNhanVien = null;
                    }
                    if (i.Id == item.IdNhanVIen)
                    {
                        NhanVienId = i.Id;
                        var ho = i.Ho;
                        var ten = i.Ten;
                        HoTenNhanVien = ho + " " +ten;
                    }

                }
                var resultItem = new TaiKhoanNganHangNhanVienResult
                {
                    Id = item.Id,
                    IdNhanVien = item.IdNhanVIen,
                    IdNhanVienTrue = NhanVienId,
                    HoTenNhanVien = HoTenNhanVien,         
                    Ten = item.Ten,
                    STK = item.STK,
                    DiaChiNH = item.DiaChiNH,
                    Slug = item.Slug,
                    GhiChu = item.GhiChu,
                    Status = item.Status
                };
                ResultItem.Add(resultItem);
            }
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                ResultItem = ResultItem.Where(e =>
                e.HoTenNhanVien.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.Ten.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.STK.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.DiaChiNH.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();

            }

            return new PagedResult<TaiKhoanNganHangNhanVienResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }
        public async Task<PagedResult<TaiKhoanNganHangDoiTacResult>> getAllTaiKhoan_DoiTacPaging(TaiKhoanNganHangDoiTacRequest request)
        {

            var Result = new PagedResult<TaiKhoanNganHangDoiTacResult>() { };
            var ResultItem = new List<TaiKhoanNganHangDoiTacResult>();

            var TaiKhoanQerrys = _context.TaiKhoanNganHangs.Where(e => e.Status == 0 && e.IdDoiTac != null);


            var RawTaiKhoan = await TaiKhoanQerrys.Select(e => new
            {
                Id = e.Id,
                IdDoiTac = e.IdDoiTac,
                Ten = e.Ten,
                STK = e.STK,
                DiaChiNH = e.DiaChiNH,
                Slug = e.Slug,
                GhiChu = e.GhiChu,
                Status = e.Status

            }).ToListAsync();



            // Filter

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
            if (request.IdDoiTac != null)
            {
                RawTaiKhoan = RawTaiKhoan
                    .Where(e => e.IdDoiTac == request.IdDoiTac)
                    .ToList();
            }

            var donhangs = RawTaiKhoan
              .OrderBy(e => e.Id)
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToList();

            var totalRecords = RawTaiKhoan.Count();
            var rawTotalPages = ((double)totalRecords / (double)request.PageSize);
            var totalPages = (int)Math.Round(rawTotalPages);
            if (rawTotalPages > totalPages)
            {
                totalPages += 1;
            }

            var DoiTacQerrynews = _context.DoiTacs.Where(e => e.Status == 0);


            long? DoiTacId = null;
            string TenCongTyDoiTac = null;


            foreach (var item in donhangs)
            {
                foreach (var i in DoiTacQerrynews)
                {
                    if (item.IdDoiTac == null)
                    {
                        DoiTacId = null;
                        TenCongTyDoiTac = null;
                    }
                    if (i.Id == item.IdDoiTac)
                    {
                        DoiTacId = i.Id;
                        TenCongTyDoiTac = i.TenCongTy;
                    }

                }
                var resultItem = new TaiKhoanNganHangDoiTacResult
                {
                    Id = item.Id,
                    IdDoiTac = item.IdDoiTac,
                    IdDoiTacTrue = DoiTacId,
                    TenCongTyDoiTac = TenCongTyDoiTac,
                    Ten = item.Ten,
                    STK = item.STK,
                    DiaChiNH = item.DiaChiNH,
                    Slug = item.Slug,
                    GhiChu = item.GhiChu,
                    Status = item.Status
                };
                ResultItem.Add(resultItem);
            }
            if (!string.IsNullOrEmpty(request.Query) || !string.IsNullOrWhiteSpace(request.Query))
            {
                ResultItem = ResultItem.Where(e =>
                e.TenCongTyDoiTac.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.Ten.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.STK.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode()) ||
                    e.DiaChiNH.ToLowerInvariant().Trim().NonUnicode().Contains(request.Query.ToLowerInvariant().Trim().NonUnicode())).ToList();

            }

            return new PagedResult<TaiKhoanNganHangDoiTacResult>
            {
                Items = ResultItem,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,

            };
        }
        public async Task<PagedResult<HoaDonResult>> getAllTaiKhoanNganHangPaging(HoaDonRequest request)
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



    }
}
