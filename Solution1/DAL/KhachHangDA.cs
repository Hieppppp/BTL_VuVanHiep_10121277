using DAL.Interfaces;
using DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Entity;
using WebAPI.Models;

namespace DAL
{
    public class KhachHangDA : IKhachHangDA
    {
        
        public string connectionString;
        public KhachHangDA(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connect");
        }
        //Thêm khách hàng
        public void InsertKhachHang(string tenKhachHang, bool gioiTinh, string diaChi, string sdt, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_InsertKhachHang", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TenKH", tenKhachHang));
                    command.Parameters.Add(new SqlParameter("@GioiTinh", gioiTinh));
                    command.Parameters.Add(new SqlParameter("@DiaChi", diaChi));
                    command.Parameters.Add(new SqlParameter("@SDT", sdt));
                    command.Parameters.Add(new SqlParameter("@Email", email));

                    command.ExecuteNonQuery();
                }
            }
        }

        //Tìm kiếm khách hàng theo ID
        public KhachHangModel GetByID(int id)
        {
            KhachHangModel khachhang = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_khachhang_get_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Kiểm tra xem có dữ liệu hay không
                        {
                            // Đọc dữ liệu từ SqlDataReader và gán vào đối tượng KhachHangModel
                            khachhang = new KhachHangModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                TenKh = reader.GetString(reader.GetOrdinal("TenKh")),
                                GioiTinh = reader.GetBoolean(reader.GetOrdinal("GioiTinh")),
                                DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                                Sdt = reader.GetString(reader.GetOrdinal("Sdt")),
                                Email = reader.GetString(reader.GetOrdinal("Email"))
                            };
                        }
                    }
                }
            }
            return khachhang;
        }

        //Sủa khách hàng
        public void upDateKhachHang(int id, string tenkh, bool gioitinh, string diachi, string sdt, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_khachhang_update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", id));
                        command.Parameters.Add(new SqlParameter("@TenKH", tenkh));
                        command.Parameters.Add(new SqlParameter("@GioiTinh", gioitinh));
                        command.Parameters.Add(new SqlParameter("@DiaChi", diachi));
                        command.Parameters.Add(new SqlParameter("@SDT", sdt));
                        command.Parameters.Add(new SqlParameter("@Email", email));

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Xóa Khách Hàng
        public void deleteKhachHang(int id)
        {
            try
            {
                using (SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command=new SqlCommand("sp_khachhang_delete", connection))
                    {
                        command.CommandType=CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id",id));
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string ten_khach, string dia_chi)
        {
            total = 0;
            List<KhachHangModel> khachModels = new List<KhachHangModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_khach_search", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@page_index", pageIndex);
                    cmd.Parameters.AddWithValue("@page_size", pageSize);
                    cmd.Parameters.AddWithValue("@ten_khach", ten_khach);
                    cmd.Parameters.AddWithValue("@dia_chi", dia_chi);

                    SqlParameter totalParameter = new SqlParameter("@total", SqlDbType.BigInt);
                    totalParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(totalParameter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KhachHangModel khachModel = new KhachHangModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                TenKh = reader["TenKH"].ToString(),
                                // Thêm các thuộc tính khác ở đây
                            };
                            khachModels.Add(khachModel);
                        }
                    }

                    if (totalParameter.Value != DBNull.Value)
                    {
                        total = Convert.ToInt64(totalParameter.Value);
                    }
                }
            }

            return khachModels;
        }


    }
}


