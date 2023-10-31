using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserBL
    {
        UserModel Login(string taikhoan, string matkhau);
        UserModel TimKiemTaiKhoanByMa(int maTaiKhoan);
        List<UserModel> GetAllTaiKhoans();
    }
}
