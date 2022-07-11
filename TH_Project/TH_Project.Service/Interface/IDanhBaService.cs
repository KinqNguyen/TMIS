using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
    public interface IDanhBaService
    {
        Task<PagedResult<DanhBaResult>> getAllDanhBaDoiTacPaging(DanhBaRequest request);
        Task<PagedResult<DanhBaResult>> getAllDanhBaNhanVienPaging(DanhBaRequest request);
        Task<DanhBaResult> GetDoiTacAsync(long productid);
        Task<DanhBaResult> GetNhanVienAsync(long productid);
        Task DeleteDoiTacAsync(long productid);
        Task DeleteNhanVienAsync(long productid);

        Task EditNhanVienAsync(long id, DanhBaNhanVienEdit args);
        Task EditDoiTacAsync(long id, DanhBaDoiTacEdit args);

        Task<long> CreateNhanVienAsync(DanhBaNhanVienCreate args);
        Task<long> CreateDoiTacAsync(DanhBaDoiTacCreate args);
    }
}
