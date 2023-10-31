using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IKhachHangDA
    {

        void InsertKhachHang(string tenKhachHang, bool gioiTinh, string diaChi, string sdt, string email);
        KhachHangModel GetByID(int id);
        void upDateKhachHang(int id, string tenkh, bool gioitinh, string diachi, string sdt, string email);
        void deleteKhachHang(int id);

        List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string ten_khach, string dia_chi);
    }
}
