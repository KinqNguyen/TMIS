using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
    public interface IQuyTienMatService
    {

        Task<PagedResult<QuyTienMatResult>> getAllQuyTienMatPaging(QuyTienMatRequest request);
        Task<QuyTienMatResult> GetAsync(long productid);
        Task DeleteAsync(long productid);
    }
}
