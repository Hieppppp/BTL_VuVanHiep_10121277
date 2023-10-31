using DAL.Interfaces;
using DataModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SanPhamDA:ISanPhamDA
    {
        private string connectionString;
        public SanPhamDA(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connect");
        }
        //Tìm kiếm sản phẩm theo mã
        public SanPhamModel GetSanPhamById(int maSanPham)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_sanpham_get_by_id", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@MaSanPham", SqlDbType.Int) { Value = maSanPham });

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            SanPhamModel sanPham = new SanPhamModel
                            {
                                MaSanPham = (int)reader["MaSanPham"],
                                MaChuyenMuc = (int)reader["MaChuyenMuc"],
                                TenSanPham = reader["TenSanPham"].ToString(),
                                AnhDaiDien = reader["AnhDaiDien"].ToString(),
                                Gia = (decimal)reader["Gia"]
                            };

                            // Lấy dữ liệu sản phẩm liên quan
                            reader.NextResult();
                            if (reader.HasRows)
                            {
                                List<SanPhamModel> sanPhamLienQuan = new List<SanPhamModel>();
                                while (reader.Read())
                                {
                                    SanPhamModel sp = new SanPhamModel
                                    {
                                        MaSanPham = (int)reader["MaSanPham"],
                                        MaChuyenMuc = (int)reader["MaChuyenMuc"],
                                        TenSanPham = reader["TenSanPham"].ToString(),
                                        AnhDaiDien = reader["AnhDaiDien"].ToString(),
                                        Gia = (decimal)reader["Gia"]
                                    };
                                    sanPhamLienQuan.Add(sp);
                                }
                                sanPham.list_json_sanphamlienquan = sanPhamLienQuan;
                            }

                            return sanPham;
                        }
                    }
                }
            }
            return null;
        }
    }

}
