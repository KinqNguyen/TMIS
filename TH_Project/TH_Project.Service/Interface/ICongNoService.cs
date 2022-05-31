using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
    public interface ICongNoService
    {
        Task<long> CreateAsync(CongNoCreate args);
        Task<PagedResult<CongNoResult>> getAllCongNoPaging(CongNoRequest request, string getNotification);
        Task<CongNoResult> GetAsync(long productid);
        Task DeleteAsync(long productid);
        Task EditAsync(long id, CongNoEdit args);
    }
}
