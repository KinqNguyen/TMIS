using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Interface

{
    public interface IHoaDonService
    {
        Task<PagedResult<HoaDonResult>> getAllHoaDonPaging(HoaDonRequest request);
        Task<PagedResult<HoaDonResult>> getAllHoaDon_DatHangPaging(HoaDonRequest request, string getNotification);
        Task<PagedResult<HoaDonResult>> getAllHoaDon_DonHangPaging(HoaDonRequest request, string getNotification);
        Task<List<HoaDonResult>> GetAsync();
        Task<HoaDonResult> GetAsync(long productid);
        Task DeleteAsync(long productid);
    }
}
