using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Interfaces
{
    public interface IKhachHangBL
    {

        void InsertKhachHang(string tenKhachHang, bool gioiTinh, string diaChi, string sdt, string email);
        KhachHangModel getById(int id);
        void upDateKhachHang(int id, string tenkh, bool gioitinh, string diachi, string sdt, string email);

        void deleteKhachHang(int id);

        List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string ten_khach, string dia_chi);


    }
}
