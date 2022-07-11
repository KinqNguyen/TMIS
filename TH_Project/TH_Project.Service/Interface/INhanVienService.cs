using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
    public interface INhanVienService
    {
        Task<PagedResult<NhanVienResult>> getAllNhanVienPaging(NhanVienRequest request);
        Task DeleteAsync(long productid);
        Task<NhanVienResult> GetNhanVienAsync(long productid);

        Task EditAsync(long id, NhanVienEdit args);
        Task<long> CreateAsync(NhanVienEdit args);
    }
}
