using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Interface
{
    public interface IDonHangService
    {
        Task<PagedResult<DonHangResult>> getAllDonHangPaging(DonHangRequest request, string getNotification);
        Task DeleteAsync(long productid);
        Task EditAsync(long id, DonHangEdit args);
        Task CreateDoiTacAsync(DonHangEdit args);
        Task<DonHangResult> GetDonHangAsync(long productid);
    }
}
