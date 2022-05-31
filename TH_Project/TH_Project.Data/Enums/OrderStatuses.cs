using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH_Project.Data.Enums
{
   public enum OrderStatuses
    {
        Default = 0, //chờ thanh toán
        Waiting = 1, //Chờ xử lý
        Prepared = 2, //đã chuẩn bị đơn hàng
        Done = 3, // Đã thanh toán
        Cancel = -1 // hủy đơn
    }
}
