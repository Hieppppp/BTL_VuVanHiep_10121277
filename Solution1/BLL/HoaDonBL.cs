using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HoaDonBL:IHoaDonBL
    {
        private IHoaDonDA _hoaDonDA;

        public HoaDonBL(IHoaDonDA hoaDonDA)
        {
            _hoaDonDA = hoaDonDA;
        }

        public HoaDonModel GetDatabyID(int maHoaDon)
        {
            return _hoaDonDA.GetDatabyID(maHoaDon);
        }
        public void CreateHoaDon(HoaDonModel model)
        {
            _hoaDonDA.CreateHoaDon(model);
        }

        public void UpdateHoaDon(int maHoaDon, string tenKH, string diachi, bool trangThai, string list_json_chitiethoadon)
        {
            _hoaDonDA.UpdateHoaDon(maHoaDon, tenKH, diachi, trangThai, list_json_chitiethoadon);
        }




    }
}
