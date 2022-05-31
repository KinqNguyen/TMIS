using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TH_Project.Data.Tables;

namespace TH_Project.Data
{
    public class TH_DbConotext : DbContext, IDisposable
    {
        public TH_DbConotext() : base($"Server=.;Database=TH_ProjectDatabase;Trusted_Connection=True;MultipleActiveResultSets=true") { }
        public TH_DbConotext(string ConnectionString) : base(ConnectionString) { }

        public DbSet<DanhBaDoiTac> DanhBaDoiTacs { get; set; }
        public DbSet<DanhBaNhanVien> DanhBaNhanViens { get; set; }
        public DbSet<DatHang> DatHangs { get; set; }
        public DbSet<DoiTac> DoiTacs { get; set; }
        public DbSet<DoiTac_va_LoaiDoiTac> DoiTac_va_LoaiDoiTacs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<LoaiDoiTac> LoaiDoiTacs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhieuGiaoHang> PhieuGiaoHangs { get; set; }
        public DbSet<QuyTienMat> QuyTienMats { get; set; }
        public DbSet<TaiKhoanNganHang> TaiKhoanNganHangs { get; set; }
        public DbSet<ViTriNhanVien> ViTriNhanViens { get; set; }
        public DbSet<CongNo> CongNos { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

    }
}
