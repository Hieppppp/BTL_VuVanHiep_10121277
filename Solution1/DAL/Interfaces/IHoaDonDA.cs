using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IHoaDonDA
    {
        HoaDonModel GetDatabyID(int maHoaDon);
        void CreateHoaDon(HoaDonModel model);
        void UpdateHoaDon(int maHoaDon, string tenKH, string diachi, bool trangThai, string list_json_chitiethoadon);
        void deleteHoaDon(int id);
        public List<ThongKeKhachModels> ThongKeKhach(int pageIndex, int pageSize, out long total, string ten_khach, DateTime? fr_NgayTao, DateTime? to_NgayTao)
        {
            return ThongKeKhach(pageIndex, pageSize, out total, ten_khach, fr_NgayTao, to_NgayTao);
        }
    }
}
