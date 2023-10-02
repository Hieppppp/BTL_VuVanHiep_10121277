using DAL.Interfaces;
using DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity;
using WebAPI.Models;

namespace DAL
{
    public class KhachHangDA:IKhachHangDA
    {
        private readonly LINHKIENContext _context;

        public KhachHangDA(LINHKIENContext dbcontext)
        {
           _context = dbcontext;
        }
        //Thêm khách hàng
        public void InsertKhachHang(string tenKhachHang, bool gioiTinh, string diaChi, string sdt, string email)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                new SqlParameter("@TenKH", tenKhachHang),
                new SqlParameter("@GioiTinh", gioiTinh),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@Email", email)
                };

                _context.Database.ExecuteSqlRaw("EXEC sp_InsertKhachHang @TenKH, @GioiTinh, @DiaChi, @SDT, @Email", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Tìm kiếm khách hàng theo ID
        public KhachHangModel getByID(int id)
        {
            try
            {
                // Sử dụng FromSqlRaw để gọi procedure và truyền tham số @Id
                var result = _context.KhachHangs.FirstOrDefault(x=>x.Id==id);

                if (result == null)
                {
                    return null;
                }

                // Tạo một đối tượng KhachHangModel và gán giá trị từ result
                var khmodel = new KhachHangModel
                {
                    Id = result.Id,
                    TenKh = result.TenKh,
                    GioiTinh = result.GioiTinh,
                    DiaChi = result.DiaChi,
                    Sdt = result.Sdt,
                    Email = result.Email
                };

                return khmodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Sủa khách hàng
        public void upDateKhachHang(int id,string tenkh,bool gioitinh,string diachi,string sdt,string email)
        {
            try
            {
                var dt = new 
                {
                    Id = id,
                    TenKH = tenkh,
                    GioiTinh = gioitinh,
                    DiaChi = diachi,
                    SDT = sdt,
                    Email = email
                };
                _context.Database.ExecuteSqlRaw("sp_khachhang_update", dt);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Xóa Khách Hàng
        public void deleteKhachHang(int id)
        {
            try
            {
                var dt = new
                {
                    Id=id,

                };
                _context.Database.ExecuteSqlRaw("sp_khachhang_delete", dt);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }






    }
}