using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TH_Project.Data;
using TH_Project.Service.Interface;
using TH_Project.Service.Services;

namespace Stump.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped((_) => new TH_DbConotext(Configuration["Database:ConnectionString"]));
            //services.AddScoped((_) => new CmsDbContext(Configuration["Database:CmsConnectionString"]));

            services.AddTransient<IDonHangService, DonHangService>();
            services.AddTransient<IDatHangService, DatHangService>();
            services.AddTransient<ICongNoService, CongNoService>();
            services.AddTransient<IDanhBaService, DanhBaService>();
            services.AddTransient<IDoiTacService, DoiTacService>();
            services.AddTransient<IHoaDonService, HoaDonService>();
            services.AddTransient<INhanVienService, NhanVienService>();
            services.AddTransient<IPhieuGiaoHangService, PhieuGiaoHangService>();
            services.AddTransient<IQuyTienMatService, QuyTienMatService>();
            services.AddTransient<ITaiKhoanNganHangService, TaiKhoanNganHangService>();



            //services.AddScoped<DeviceTypeService>();
            //services.AddScoped<ClientService>();
            //services.AddScoped<ConfigurationService>();
            //services.AddScoped<VNPayService>();
            //services.AddScoped<MomoService>();
            //services.AddScoped<PaymentService>();
            //services.AddScoped<EvatProfileService>();
            //services.AddScoped<ViettelSmsProfileService>();
            //services.AddScoped<EmailProfileService>();


            ////services.AddScoped<EC.Api.Data.Services.ProductService>();

            ////services.AddScoped<EC.Api.Data.Services.CustomerService>();
            ////services.AddScoped<EC.Api.Data.Services.PromotionService>();

            //services.AddScoped<Stump.Api.Data.Services.CategoryService>();
            //services.AddScoped<TokenService>();
            //services.AddScoped<ConfigurationService>();
            //services.AddScoped<EmployeeService>();
            //services.AddScoped<WarehouseService>();
            //services.AddScoped<CustomerService>();
            //services.AddScoped<ProductService>();
            //services.AddScoped<PromotionService>();
            //services.AddScoped<BannerService>();
            //services.AddScoped<OrderService>();
            //services.AddScoped<ReportService>();
            //services.AddScoped<BannerTypeService>();

            ////services.AddScoped<EC.Api.Data.Services.OrderService>();

            //services.AddTransient<VnpayController>();
            //services.AddTransient<MomoController>();

            //#region cms
            //services.AddScoped<IContactService, ContactService>();
            //services.AddScoped<IRecruitService, RecruitService>();
            //services.AddScoped<IPostService, PostService>();
            //services.AddScoped<ICategoryService, Stump.Api.Data.Cms.Services.CategoryService>();
            //#endregion

        }


    }
}