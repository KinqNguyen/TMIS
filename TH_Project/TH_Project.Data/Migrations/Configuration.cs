namespace TH_Project.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TH_Project.Data.TH_DbConotext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TH_Project.Data.TH_DbConotext context)
        {
            //        context.NhanViens.AddOrUpdate(new Tables.NhanVien
            //        {
            //            Id = 1,
            //            Ten = "Root",
            //            LoginName = "root",
            //            Password = "123#Qwerty",
            //            BietHieu = "Chú",
            //            DienThoai = "111222333444",
            //            Status = Enums.Statuses.Default
            //        });

            //        context.SaveChanges();
            ////Seed bang Doi Tac
            //        List<Tables.LoaiDoiTac> LoaiDoiTac = new List<Tables.LoaiDoiTac>();
            //        LoaiDoiTac.Add(new Tables.LoaiDoiTac
            //        {
            //            Id = 1,
            //            TenLoaiDoiTac = "Khách Hàng"
            //        });
            //        LoaiDoiTac.Add(new Tables.LoaiDoiTac
            //        {
            //            Id = 2,
            //            TenLoaiDoiTac = "Nhà cung cấp"
            //        });

            //        //Seed bang Hoa Don
            List<Tables.HoaDon> HoaDon = new List<Tables.HoaDon>();
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 1,
            //            IdDonHang = 1,
            //            SoHD="HD1",
            //            GiaTri =1000000,
            //            LoaiHoaDon=Enums.BillTypes.Ban,
            //            BillStatus = Enums.BillStatuses.Default,
            //            Status =Enums.Statuses.Default
            //        });
            //HoaDon.Add(new Tables.HoaDon
            //{
            //    Id = 2,
            //    IdDonHang = 1,
            //    IdDatHang= 0,
            //    SoHD = "HD1",
            //    Ngay = DateTime.Now,
            //    GiaTri = 4000000,
            //    LoaiHoaDon = Enums.BillTypes.Ban,
            //    BillStatus = Enums.BillStatuses.Done,
            //    Status = Enums.Statuses.Default
            
            //});
            
            //foreach (var Ho in HoaDon)
            //{
            //    var configdb = context.HoaDons.Find(Ho.Id);
            //    if (configdb == null)
            //    {
            //        context.HoaDons.AddOrUpdate(Ho);
            //    }
            //}
            //     HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 5,
            //            IdDonHang = 2,
            //            SoHD = "HD2",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 4000000,
            //            LoaiHoaDon = Enums.BillTypes.Ban,
            //            BillStatus = Enums.BillStatuses.Done,
            //            Status = Enums.Statuses.Default
            //        });
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 6,
            //            IdDonHang = 3,
            //            SoHD = "HD3",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 4000000,
            //            LoaiHoaDon = Enums.BillTypes.Ban,
            //            BillStatus = Enums.BillStatuses.Done,
            //            Status = Enums.Statuses.Default
            //        });

            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 3,
            //            IdDatHang = 1,
            //            SoHD = "DH1",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 1000000,
            //            LoaiHoaDon = Enums.BillTypes.Mua,
            //            Status = Enums.Statuses.Default,
            //            BillStatus = Enums.BillStatuses.Done,
            //        });
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 4,
            //            IdDatHang = 1,
            //            SoHD = "DH1",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 1000000,
            //            LoaiHoaDon = Enums.BillTypes.Mua,
            //            Status = Enums.Statuses.Default
            //            ,
            //            BillStatus = Enums.BillStatuses.Default,
            //        });
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 7,
            //            IdDatHang = 2,
            //            SoHD = "DH7",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 1000000,
            //            LoaiHoaDon = Enums.BillTypes.Mua,
            //            BillStatus = Enums.BillStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 8,
            //            IdDatHang = 3,
            //            SoHD = "DH3",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 1000000,
            //            LoaiHoaDon = Enums.BillTypes.Mua,
            //            BillStatus = Enums.BillStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 9,
            //            IdDatHang = 4,
            //            SoHD = "DH4",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 1000000,
            //            LoaiHoaDon = Enums.BillTypes.Mua,
            //            BillStatus = Enums.BillStatuses.Done,
            //            Status = Enums.Statuses.Default
            //        });
            //        HoaDon.Add(new Tables.HoaDon
            //        {
            //            Id = 10,
            //            IdDatHang = 5,
            //            SoHD = "DH5",
            //            Ngay = new DateTime(2022, 2, 15, 13, 45, 0),
            //            GiaTri = 1000000,
            //            LoaiHoaDon = Enums.BillTypes.Mua,
            //            BillStatus = Enums.BillStatuses.Done,
            //            Status = Enums.Statuses.Default
            //        });

            ////Seed bang Cong No
            //        List<Tables.CongNo> CongNo = new List<Tables.CongNo>();
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 1,
            //            IdHoaDon = 1,
            //            MaCongNo = "CN1",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 16, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 2,
            //            IdHoaDon = 2,
            //            MaCongNo = "CN2",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 18, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 4000000,
            //            Status = Enums.Statuses.Default
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 5,
            //            IdHoaDon = 5,
            //            MaCongNo = "CN5",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 23, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 4000000,
            //            Status = Enums.Statuses.Default
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 6,
            //            IdHoaDon = 6,
            //            MaCongNo = "CN6",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 28, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 4000000,
            //            Status = Enums.Statuses.Default
            //        });

            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 3,
            //            IdHoaDon = 3,
            //            MaCongNo = "CN3",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 3, 16, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default,
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 4,
            //            IdHoaDon = 4,
            //            MaCongNo = "CN4",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 4, 16, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default,
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 7,
            //            IdHoaDon = 7,
            //            MaCongNo = "CN7",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 20, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 8,
            //            IdHoaDon = 8,
            //            MaCongNo = "CN8",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 25, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 9,
            //            IdHoaDon = 9,
            //            MaCongNo = "CN9",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 16, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default
            //        });
            //        CongNo.Add(new Tables.CongNo
            //        {
            //            Id = 10,
            //            IdHoaDon = 10,
            //            MaCongNo = "CN10",
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            NgayToiHan = new DateTime(2022, 2, 26, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            GiaTri = 1000000,
            //            Status = Enums.Statuses.Default
            //        });


            //        //Seed bang Doi Tac

            //        List<Tables.DoiTac> DoiTac = new List<Tables.DoiTac>();

            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 1,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_1",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "o",
            //            Status = Enums.Statuses.Default
            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 2,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_2",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 3,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_3",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 4,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_4",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 5,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_5",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 6,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_6",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 7,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_7",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 8,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_8",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 9,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_9",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 10,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_10",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 1,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_1",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 4,
            //            IdLoaiDoiTac = 2,
            //            TenCongTy = "TH_4",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 7,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_7",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });
            //        DoiTac.Add(new Tables.DoiTac
            //        {
            //            Id = 8,
            //            IdLoaiDoiTac = 1,
            //            TenCongTy = "TH_8",
            //            SanPhamDichVu = "Ngan Hang",
            //            MaSoThue = "0000ff",
            //            DienThoai = "111333444",
            //            Status = Enums.Statuses.Default

            //        });

            ////Seed bang Danh Ba Doi Tac

            //        List<Tables.DanhBaDoiTac> DanhBaDoiTac = new List<Tables.DanhBaDoiTac>();

            //        DanhBaDoiTac.Add(new Tables.DanhBaDoiTac
            //        {
            //            Id = 1,
            //            IdDoiTac = 1,
            //            XungHo = "Anh",
            //            Ho = "Nguyễn",
            //            Ten = "Kiệt",
            //            DiDong = "0364832310",
            //            Status=Enums.Statuses.Default

            //        });

            //        DanhBaDoiTac.Add(new Tables.DanhBaDoiTac
            //        {
            //            Id = 2,
            //            IdDoiTac = 1,
            //            XungHo = "Anh",
            //            Ho = "Nguyễn",
            //            Ten = "Kiệt",
            //            DiDong = "0964832310",
            //            Status = Enums.Statuses.Default

            //        });

            //        DanhBaDoiTac.Add(new Tables.DanhBaDoiTac
            //        {
            //            Id = 3,
            //            IdDoiTac = 1,
            //            XungHo = "Anh",
            //            Ho = "Nguyễn",
            //            Ten = "Kiệt",
            //            DiDong = "0764832310",
            //            Status = Enums.Statuses.Default

            //        });

            //        DanhBaDoiTac.Add(new Tables.DanhBaDoiTac
            //        {
            //            Id = 3,
            //            IdDoiTac = 6,
            //            XungHo = "Anh",
            //            Ho = "Nguyễn",
            //            Ten = "Khanh 2",
            //            DiDong = "113113113",
            //            Status = Enums.Statuses.Default

            //        });

            //        DanhBaDoiTac.Add(new Tables.DanhBaDoiTac
            //        {
            //            Id = 3,
            //            IdDoiTac = 6,
            //            XungHo = "Anh",
            //            Ho = "Nguyễn",
            //            Ten = "Khanh 2",
            //            DiDong = "114114114",
            //            Status = Enums.Statuses.Default

            //        });

            ////Seed bang Vi Tri Nhan Vien

            //        List<Tables.ViTriNhanVien> ViTriNhanVien = new List<Tables.ViTriNhanVien>();

            //        ViTriNhanVien.Add(new Tables.ViTriNhanVien
            //        {
            //            Id = 1,
            //            TenViTri="admin"
            //        });
            //        ViTriNhanVien.Add(new Tables.ViTriNhanVien
            //        {
            //            Id = 2,
            //            TenViTri = "Giám đốc"
            //        });
            //        ViTriNhanVien.Add(new Tables.ViTriNhanVien
            //        {
            //            Id = 3,
            //            TenViTri = "Kế Toán"
            //        });
            //        ViTriNhanVien.Add(new Tables.ViTriNhanVien
            //        {
            //            Id = 4,
            //            TenViTri = "Thợ kỹ thuật"
            //        });
            //        ViTriNhanVien.Add(new Tables.ViTriNhanVien
            //        {
            //            Id = 5,
            //            TenViTri = "Thợ vẽ"
            //        });


            ////Seed bang Nhan Vien

            //        List<Tables.NhanVien> NhanVien = new List<Tables.NhanVien>();

            //        NhanVien.Add(new Tables.NhanVien
            //        {
            //            Id = 1,
            //            IdViTri = 2,
            //            Ho = "Viet",
            //            Ten = "Viet",
            //            BietHieu="Chú",
            //            LoginName="viet123",
            //            Password="123",
            //            DienThoai = "111222333444",
            //            Status = Enums.Statuses.Default
            //        });
            //        NhanVien.Add(new Tables.NhanVien
            //        {
            //            Id = 1,
            //            IdViTri = 3,
            //            Ho = "Thao",
            //            Ten = "Thảo",
            //            BietHieu = "A",
            //            LoginName = "thao123",
            //            Password = "123",
            //            DienThoai = "111222333444",
            //            Status = Enums.Statuses.Default
            //        });
            //        NhanVien.Add(new Tables.NhanVien
            //        {
            //            Id = 1,
            //            IdViTri = 4,
            //            Ho = "Thợ",
            //            Ten = "KT",
            //            BietHieu = "A",
            //            LoginName = "KTA123",
            //            Password = "123",
            //            DienThoai = "111222333444",
            //            Status = Enums.Statuses.Default
            //        });
            //        NhanVien.Add(new Tables.NhanVien
            //        {
            //            Id = 1,
            //            IdViTri = 4,
            //            Ho = "Thợ",
            //            Ten = "KT",
            //            BietHieu = "B",
            //            LoginName = "KTB123",
            //            Password = "123",
            //            DienThoai = "111222333444",
            //            Status = Enums.Statuses.Default
            //        });
            //        NhanVien.Add(new Tables.NhanVien
            //        {
            //            Id = 1,
            //            IdViTri = 5,
            //            Ho = "Thợ",
            //            Ten = "Thiết Kế",
            //            LoginName = "thietke123",
            //            Password = "123",
            //            DienThoai = "111222333444",
            //            Status = Enums.Statuses.Default
            //        });



            ////Seed bang Danh Ba Nhan Vien

            //        List<Tables.DanhBaNhanVien> DanhBaNhanVien = new List<Tables.DanhBaNhanVien>();

            //        DanhBaNhanVien.Add(new Tables.DanhBaNhanVien
            //        {
            //            Id = 1,
            //            IdNhanVien = 1,
            //            DiDong = "0364832310",
            //            Status = Enums.Statuses.Default
            //        });

            //        DanhBaNhanVien.Add(new Tables.DanhBaNhanVien
            //        {
            //            Id = 2,
            //            IdNhanVien = 2,
            //            DiDong = "115115115",
            //            Status = Enums.Statuses.Default
            //        });

            //        DanhBaNhanVien.Add(new Tables.DanhBaNhanVien
            //        {
            //            Id = 3,
            //            IdNhanVien = 2,
            //            DiDong = "116116116",
            //            Status = Enums.Statuses.Default
            //        });
            //        DanhBaNhanVien.Add(new Tables.DanhBaNhanVien
            //        {
            //            Id = 4,
            //            IdNhanVien = 2,
            //            DiDong = "117117117",
            //            Status = Enums.Statuses.Default
            //        });


            ////Seed bang Don Hang

            //List<Tables.DonHang> DonHang = new List<Tables.DonHang>();
            //DonHang.Add(new Tables.DonHang
            //{
            //    Id = 1,
            //    IdDoiTac = 1,
            //    IdNhanVien = 1,
            //    MaDonHang = "sdsda",
            //    TenCongTrinh = "hhiihi",
            //    GiaTri = 1,
            //    NgayBatDau = DateTime.Now,
            //    TrangThai = Enums.OrderStatuses.Default,
            //    Status = Enums.Statuses.Default
            //});

            //foreach (var Ho in DonHang)
            //{
            //    var configdb = context.DonHangs.Find(Ho.Id);
            //    if (configdb == null)
            //    {
            //        context.DonHangs.AddOrUpdate(Ho);
            //    }
            //}
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 2,
            //            IdDoiTac = 1,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda2",
            //            TenCongTrinh = "hhiihi2",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 2, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 3,
            //            IdDoiTac=1,
            //            IdNhanVien = 1,
            //            MaDonHang="sdsda3",
            //            TenCongTrinh="hhiihi3",
            //            GiaTri=1,
            //            NgayBatDau= new DateTime(2020, 3, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 4,
            //            IdDoiTac = 1,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda4",
            //            TenCongTrinh = "hhiihi4",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 4, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id =5,
            //            IdDoiTac = 1,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda5",
            //            TenCongTrinh = "hhiihi5",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 5, 15, 13, 45, 0)                ,
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 6,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda6",
            //            TenCongTrinh = "hhiihi6",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 6, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 7,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda7",
            //            TenCongTrinh = "hhiihi7",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 7, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 8,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda8",
            //            TenCongTrinh = "hhiihi8",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 8, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 9,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda9",
            //            TenCongTrinh = "hhiihi9",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 9, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 10,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda10",
            //            TenCongTrinh = "hhiihi10",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 10, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 11,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda11",
            //            TenCongTrinh = "hhiihi11",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 11, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 12,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda12",
            //            TenCongTrinh = "hhiihi12",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 12, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 13,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda12",
            //            TenCongTrinh = "hhiihi12",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 1, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 14,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda14",
            //            TenCongTrinh = "hhiihi14",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 2, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 15,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda15",
            //            TenCongTrinh = "hhiihi15",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 3, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 16,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda16",
            //            TenCongTrinh = "hhiihi16",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 4, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 17,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda17",
            //            TenCongTrinh = "hhiihi17",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 5, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 18,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda18",
            //            TenCongTrinh = "hhiihi18",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 6, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 19,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda19",
            //            TenCongTrinh = "hhiihi19",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 7, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 20,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda20",
            //            TenCongTrinh = "hhiihi20",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 8, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 21,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda21",
            //            TenCongTrinh = "hhiihi21",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 9, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 22,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda22",
            //            TenCongTrinh = "hhiihi22",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 10, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 23,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "Công trình mẫu",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 11, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 24,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "Mãu để tìm kiếm",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 12, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 25,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda25",
            //            TenCongTrinh = "hhiihi25",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 1, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 26,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda26",
            //            TenCongTrinh = "hhiihi26",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 27,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda27",
            //            TenCongTrinh = "hhiihi27",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 3, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 28,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda28",
            //            TenCongTrinh = "hhiihi28",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 4, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 29,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda29",
            //            TenCongTrinh = "hhiihi29",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 5, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 30,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda30",
            //            TenCongTrinh = "hhiihi30",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 6, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 31,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda31",
            //            TenCongTrinh = "hhiihi31",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 7, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 32,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 8, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 33,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 9, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 34,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 10, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 35,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 11, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DonHang.Add(new Tables.DonHang
            //        {
            //            Id = 36,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 12, 15, 13, 45, 0)                ,TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });

            //           //Seed bang Dat Hang

            //List<Tables.DatHang> DatHang = new List<Tables.DatHang>();
            //DatHang.Add(new Tables.DatHang
            //{
            //    Id = 1,
            //    IdDoiTac = 1,
            //    IdNhanVien = 1,
            //    MaDonHang = "sdsda",
            //    TenCongTrinh = "hhiihi",
            //    GiaTri = 1,
            //    NgayBatDau = DateTime.Now,
            //    TrangThai = Enums.OrderStatuses.Default,
            //    Status = Enums.Statuses.Default
            //});
            //foreach (var Ho in DatHang)
            //{
            //    var configdb = context.DatHangs.Find(Ho.Id);
            //    if (configdb == null)
            //    {
            //        context.DatHangs.AddOrUpdate(Ho);
            //    }
            //}
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 2,
            //            IdDoiTac = 1,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda2",
            //            TenCongTrinh = "hhiihi2",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 2, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 3,
            //            IdDoiTac=1,
            //            IdNhanVien = 1,
            //            MaDonHang="sdsda3",
            //            TenCongTrinh="hhiihi3",
            //            GiaTri=1,
            //            NgayBatDau= new DateTime(2020, 3, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 4,
            //            IdDoiTac = 1,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda4",
            //            TenCongTrinh = "hhiihi4",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 4, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id =5,
            //            IdDoiTac = 1,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda5",
            //            TenCongTrinh = "hhiihi5",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 5, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 6,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda6",
            //            TenCongTrinh = "hhiihi6",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 6, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 7,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda7",
            //            TenCongTrinh = "hhiihi7",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 7, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 8,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda8",
            //            TenCongTrinh = "hhiihi8",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 8, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 9,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda9",
            //            TenCongTrinh = "hhiihi9",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 9, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 10,
            //            IdDoiTac = 2,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda10",
            //            TenCongTrinh = "hhiihi10",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 10, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 11,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda11",
            //            TenCongTrinh = "hhiihi11",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 11, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 12,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda12",
            //            TenCongTrinh = "hhiihi12",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2020, 12, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 13,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda12",
            //            TenCongTrinh = "hhiihi12",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 1, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 14,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda14",
            //            TenCongTrinh = "hhiihi14",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 2, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 15,
            //            IdDoiTac = 3,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda15",
            //            TenCongTrinh = "hhiihi15",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 3, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 16,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda16",
            //            TenCongTrinh = "hhiihi16",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 4, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 17,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda17",
            //            TenCongTrinh = "hhiihi17",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 5, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 18,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda18",
            //            TenCongTrinh = "hhiihi18",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 6, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 19,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda19",
            //            TenCongTrinh = "hhiihi19",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 7, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 20,
            //            IdDoiTac = 4,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda20",
            //            TenCongTrinh = "hhiihi20",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 8, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 21,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda21",
            //            TenCongTrinh = "hhiihi21",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 9, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 22,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda22",
            //            TenCongTrinh = "hhiihi22",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 10, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 23,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "Công trình mẫu",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 11, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 24,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "Mãu để tìm kiếm",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2021, 12, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 25,
            //            IdDoiTac = 5,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda25",
            //            TenCongTrinh = "hhiihi25",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 1, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 26,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda26",
            //            TenCongTrinh = "hhiihi26",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 2, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 27,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda27",
            //            TenCongTrinh = "hhiihi27",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 3, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 28,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda28",
            //            TenCongTrinh = "hhiihi28",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 4, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 29,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda29",
            //            TenCongTrinh = "hhiihi29",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 5, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 30,
            //            IdDoiTac = 6,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda30",
            //            TenCongTrinh = "hhiihi30",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 6, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 31,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda31",
            //            TenCongTrinh = "hhiihi31",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 7, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 32,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 8, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 33,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 9, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 34,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 10, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 35,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 11, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });
            //        DatHang.Add(new Tables.DatHang
            //        {
            //            Id = 36,
            //            IdDoiTac = 7,
            //            IdNhanVien = 1,
            //            MaDonHang = "sdsda",
            //            TenCongTrinh = "hhiihi",
            //            GiaTri = 1,
            //            NgayBatDau = new DateTime(2022, 12, 15, 13, 45, 0),
            //            TrangThai = Enums.OrderStatuses.Default,
            //            Status = Enums.Statuses.Default
            //        });



        }
    }
}


































































































































































































