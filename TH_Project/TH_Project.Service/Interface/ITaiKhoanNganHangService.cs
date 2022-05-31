using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
    public interface ITaiKhoanNganHangService
    {
        Task<PagedResult<TaiKhoanNganHangNhanVienResult>> getAllTaiKhoan_NhanVienPaging(TaiKhoanNganHangNhanVienRequest request);
        Task<PagedResult<TaiKhoanNganHangDoiTacResult>> getAllTaiKhoan_DoiTacPaging(TaiKhoanNganHangDoiTacRequest request);
        Task<TaiKhoanNganHangNhanVienResult> GetTKNHNhanVienAsync(long productid);
        Task DeleteAsync(long productid);
        Task<TaiKhoanNganHangDoiTacResult> GetTKNHDoiTacAsync(long productid);
    }
}
