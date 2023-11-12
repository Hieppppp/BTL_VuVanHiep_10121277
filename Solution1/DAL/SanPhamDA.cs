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
        //Tìm kiếm sản phẩm theo mã ID
        public SanPhamModel GetByID(int id)
        {
            SanPhamModel sanPham = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetSanPhamByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MaSanPham", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Kiểm tra xem có dữ liệu hay không
                        {
                            // Đọc dữ liệu từ SqlDataReader và gán vào đối tượng KhachHangModel
                            sanPham = new SanPhamModel
                            {
                                MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                MaChuyenMuc = reader.IsDBNull(reader.GetOrdinal("MaChuyenMuc")) ? null : reader.GetInt32(reader.GetOrdinal("MaChuyenMuc")),
                                TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                AnhDaiDien = reader.GetString(reader.GetOrdinal("AnhDaiDien")),
                                Gia = reader.GetDecimal(reader.GetOrdinal("Gia")),
                                GiaGiam = reader.IsDBNull(reader.GetOrdinal("GiaGiam")) ? null : reader.GetDecimal(reader.GetOrdinal("GiaGiam")),
                                SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")),
                                LuotXem = reader.GetInt32(reader.GetOrdinal("LuotXem")),
                                DacBiet = reader.GetBoolean(reader.GetOrdinal("DacBiet"))
                            };
                        }
                    }
                }
            }
            return sanPham;
        }

        //Tạo thêm Sản Phẩm
        public void InsertSanPham(SanPhamModel sanPham)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_InsertSanPham", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MaChuyenMuc", sanPham.MaChuyenMuc);
                    command.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                    command.Parameters.AddWithValue("@AnhDaiDien", sanPham.AnhDaiDien);
                    command.Parameters.AddWithValue("@Gia", sanPham.Gia);
                    command.Parameters.AddWithValue("@GiaGiam", sanPham.GiaGiam);
                    command.Parameters.AddWithValue("@SoLuong", sanPham.SoLuong);
                    command.Parameters.AddWithValue("@TrangThai", sanPham.TrangThai);
                    command.Parameters.AddWithValue("@LuotXem", sanPham.LuotXem);
                    command.Parameters.AddWithValue("@DacBiet", sanPham.DacBiet);

                    command.ExecuteNonQuery();
                }
            }
        }
        //Update Sản Phẩm
        public void UpdateSanPham(SanPhamModel sanPham)
        {
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand cmd=new SqlCommand("sp_UpdateSanPham", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaSanPham", sanPham.MaSanPham);
                    cmd.Parameters.AddWithValue("@MaChuyenMuc", sanPham.MaChuyenMuc);
                    cmd.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                    cmd.Parameters.AddWithValue("@AnhDaiDien", sanPham.AnhDaiDien);
                    cmd.Parameters.AddWithValue("@Gia", sanPham.Gia);
                    cmd.Parameters.AddWithValue("@GiaGiam", sanPham.GiaGiam);
                    cmd.Parameters.AddWithValue("@SoLuong", sanPham.SoLuong);
                    cmd.Parameters.AddWithValue("@TrangThai", sanPham.TrangThai);
                    cmd.Parameters.AddWithValue("@LuotXem", sanPham.LuotXem);
                    cmd.Parameters.AddWithValue("@DacBiet", sanPham.DacBiet);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Xóa Sản Phẩm theo Mã
        public void DeleteSanPham(int id)
        {
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand cmd=new SqlCommand("sp_DeleteSanPham", connection))
                {
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaSanPham",id);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        //Lấy toàn bộ danh sách Sản Phẩm
        public List<SanPhamModel> GetAllSanPhams()
        {
            List<SanPhamModel> sanPhams = new List<SanPhamModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetAllSanPhams", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPhamModel user = new SanPhamModel
                            {
                                MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                MaChuyenMuc = reader.IsDBNull(reader.GetOrdinal("MaChuyenMuc")) ? null : reader.GetInt32(reader.GetOrdinal("MaChuyenMuc")),
                                TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                AnhDaiDien = reader.GetString(reader.GetOrdinal("AnhDaiDien")),
                                Gia = reader.GetDecimal(reader.GetOrdinal("Gia")),
                                GiaGiam = reader.IsDBNull(reader.GetOrdinal("GiaGiam")) ? null : reader.GetDecimal(reader.GetOrdinal("GiaGiam")),
                                SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")),
                                LuotXem = reader.GetInt32(reader.GetOrdinal("LuotXem")),
                                DacBiet = reader.GetBoolean(reader.GetOrdinal("DacBiet"))
                            };

                            sanPhams.Add(user);
                        }
                    }
                }
            }

            return sanPhams;
        }
        //Tìm kiếm sản phẩm 
        public List<SanPhamModel> SearchSanPhams(string keyword, int pageIndex, int pageSize, out long total)
        {
            List<SanPhamModel> sanPhams = new List<SanPhamModel>();
            total = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SearchSanPhams", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Keyword", keyword);
                    command.Parameters.AddWithValue("@PageIndex", pageIndex);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    var totalParam = command.Parameters.Add("@Total", SqlDbType.BigInt);
                    totalParam.Direction = ParameterDirection.Output;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPhamModel sanPham = new SanPhamModel
                            {
                                MaSanPham = reader.GetInt32(reader.GetOrdinal("MaSanPham")),
                                MaChuyenMuc = reader.IsDBNull(reader.GetOrdinal("MaChuyenMuc")) ? null : reader.GetInt32(reader.GetOrdinal("MaChuyenMuc")),
                                TenSanPham = reader.GetString(reader.GetOrdinal("TenSanPham")),
                                AnhDaiDien = reader.GetString(reader.GetOrdinal("AnhDaiDien")),
                                Gia = reader.GetDecimal(reader.GetOrdinal("Gia")),
                                GiaGiam = reader.IsDBNull(reader.GetOrdinal("GiaGiam")) ? null : reader.GetDecimal(reader.GetOrdinal("GiaGiam")),
                                SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                                TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")),
                                LuotXem = reader.GetInt32(reader.GetOrdinal("LuotXem")),
                                DacBiet = reader.GetBoolean(reader.GetOrdinal("DacBiet"))
                            };
                            sanPhams.Add(sanPham);
                        }
                    }

                    total = (long)totalParam.Value;
                }
            }

            return sanPhams;
        }


    }

}
