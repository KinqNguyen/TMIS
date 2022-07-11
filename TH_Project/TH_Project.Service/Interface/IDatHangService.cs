﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH_Project.Service.DTOs.Base;
using TH_Project.Service.DTOs.Result_Request_DTOs_ViewModel;

namespace TH_Project.Service.Services
{
    public interface IDatHangService
    {
        Task<PagedResult<DatHangResult>> getAllDatHangPaging(DatHangRequest request, string getNotification);
        Task DeleteAsync(long productid);
        Task CreateDoiTacAsync(DatHangCreate args);
        Task EditAsync(long id, DatHangEdit args);
        Task<DatHangResult> GetDatHangAsync(long productid);

    }
}
