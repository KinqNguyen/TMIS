using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH_Project.Data.Enums
{
   public enum BillTypes
    {
        Mua = 0, //Nhap
        Ban = 1, //Xuat
    }

    public enum BillStatuses
    {
        Default = 0,
        Done = 1,
        Waiting = 1,
    }
}
