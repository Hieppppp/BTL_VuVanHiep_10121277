using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHoaDonBL
    {
        HoaDonModel GetDatabyID(int maHoaDon);
        void CreateHoaDon(HoaDonModel model);
        void UpdateHoaDon(int maHoaDon, string tenKH, string diachi, bool trangThai, string list_json_chitiethoadon);
       
    }
}
