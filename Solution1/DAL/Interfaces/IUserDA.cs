﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserDA
    {
        UserModel Login(string taikhoan, string matkhau);
        UserModel TimKiemTaiKhoanByMa(int maTaiKhoan);
        List<UserModel> GetAllTaiKhoans();
    }
}
