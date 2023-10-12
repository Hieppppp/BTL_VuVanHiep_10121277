using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace DAL
{
    public class HoaDonDA
    {
        private LINHKIENContext _context;

        public HoaDonDA(LINHKIENContext context)
        {
            _context = context;
        }

        public HoaDonModel GetHoadonByID(int maHoaDon)
        {
            // Sử dụng Entity Framework để gọi thủ tục và truy vấn dữ liệu từ cơ sở dữ liệu
            var hoadon = _context.HoaDons.FromSqlRaw("EXEC sp_hoadon_get_by_id @MaHoaDon", new SqlParameter("@MaHoaDon", maHoaDon)).FirstOrDefault();

            return hoadon;
        }
    }
}
