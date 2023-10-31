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
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL
{
    public class UserDA : IUserDA
    {
        private string connectionString;

        public UserDA(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connect");
        }

        public UserModel Login(string taikhoan, string matkhau)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_login", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@taikhoan", taikhoan));
                        command.Parameters.Add(new SqlParameter("@matkhau", matkhau));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var dt = new UserModel();

                            while (reader.Read())
                            {
                                // Đọc dữ liệu từ SqlDataReader và cài đặt thuộc tính của UserModel
                                dt.MaTaiKhoan = reader.GetInt32(reader.GetOrdinal("MaTaiKhoan"));
                                dt.LoaiTaiKhoan = reader.GetInt32(reader.GetOrdinal("LoaiTaiKhoan"));
                                dt.TenTaiKhoan = reader.GetString(reader.GetOrdinal("TenTaiKhoan"));
                                dt.MatKhau = reader.GetString(reader.GetOrdinal("MatKhau"));
                                dt.Email = reader.GetString(reader.GetOrdinal("Email"));
                            }

                            return dt;
                        }
                    }
                }

                return null; // Trả về null nếu không tìm thấy tài khoản
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserModel TimKiemTaiKhoanByMa(int maTaiKhoan)
        {
            UserModel user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_timkiemtaikhoanByMa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                MaTaiKhoan = reader.GetInt32(reader.GetOrdinal("MaTaiKhoan")),
                                LoaiTaiKhoan = reader.IsDBNull(reader.GetOrdinal("LoaiTaiKhoan")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("LoaiTaiKhoan")),
                                TenTaiKhoan = reader.GetString(reader.GetOrdinal("TenTaiKhoan")),
                                MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                                Email = reader.GetString(reader.GetOrdinal("Email"))
                            };
                        }
                    }
                }
            }

            return user;
        }
        public List<UserModel> GetAllTaiKhoans()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_getall_taikhoans", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                MaTaiKhoan = reader.GetInt32(reader.GetOrdinal("MaTaiKhoan")),
                                LoaiTaiKhoan = reader.IsDBNull(reader.GetOrdinal("LoaiTaiKhoan")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("LoaiTaiKhoan")),
                                TenTaiKhoan = reader.GetString(reader.GetOrdinal("TenTaiKhoan")),
                                MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

    }

}
