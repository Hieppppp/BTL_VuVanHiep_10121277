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

        public void deleteHoaDon(int id)
        {
            _hoaDonDA.deleteHoaDon(id);
        }
        public List<ThongKeKhachModels> ThongKeKhach(int pageIndex, int pageSize, out long total, string ten_khach, DateTime? fr_NgayTao, DateTime? to_NgayTao)
        {
            return _hoaDonDA.ThongKeKhach(pageIndex, pageSize, out total, ten_khach, fr_NgayTao, to_NgayTao);
        }




    }
}
