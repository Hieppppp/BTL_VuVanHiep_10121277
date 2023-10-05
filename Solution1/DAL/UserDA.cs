using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;

namespace DAL
{
    public class UserDA:IUserDA
    {
        private readonly LINHKIENContext _context;

        public UserDA(LINHKIENContext context)
        {
            _context = context;
        }

        public UserModel Login(string taikhoan, string matkhau)
        {
            
            try
            {
                var dt = _context.TaiKhoans.FromSqlRaw("EXEC sp_login @taikhoan, @matkhau",
                    new SqlParameter("@taikhoan", taikhoan),
                    new SqlParameter("@matkhau", matkhau))
                    .AsEnumerable() // Chuyển sang phía máy khách
                    .SingleOrDefault();

                if (dt != null)
                {
                    // Tạo một đối tượng UserModel và sao chép dữ liệu từ đối tượng taiKhoans
                    UserModel user = new UserModel
                    {
                        MaTaiKhoan = dt.MaTaiKhoan,
                        LoaiTaiKhoan = dt.LoaiTaiKhoan,
                        TenTaiKhoan = dt.TenTaiKhoan,
                        MatKhau = dt.MatKhau,
                        Email = dt.Email
                    };

                    return user;
                }

                return null; // Trả về null nếu không tìm thấy tài khoản
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
