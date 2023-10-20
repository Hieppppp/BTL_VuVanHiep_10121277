using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using DAL.Interfaces;
using DataModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


public class HoaDonDA:IHoaDonDA
{
    private string connectionString;

    public HoaDonDA(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("connect");
    }
    //Tìm kiếm hóa đơn theo id
    public HoaDonModel GetDatabyID(int maHoaDon)
    {
        HoaDonModel hoaDonModel = new HoaDonModel();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("sp_hoadon_get_by_id", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MaHoaDon", SqlDbType.Int) { Value = maHoaDon });

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        hoaDonModel.MaHoaDon = (int)reader["MaHoaDon"];
                        hoaDonModel.TenKH = reader["TenKH"].ToString();
                        hoaDonModel.Diachi = reader["Diachi"].ToString();
                        hoaDonModel.TrangThai = (bool)reader["TrangThai"];

                        // Xử lý chuỗi JSON cho list_json_chitiethoadon
                        string jsonResult = reader["list_json_chitiethoadon"].ToString();
                        hoaDonModel.list_json_chitiethoadon = JsonConvert.DeserializeObject<List<ChiTietHoaDonModel>>(jsonResult);
                    }
                }
            }
        }

        return hoaDonModel;
    }

    //Thêm hóa đơn
    public void CreateHoaDon(HoaDonModel model)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_hoadon_create", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TenKH", SqlDbType.NVarChar, 50) { Value = model.TenKH });
                    cmd.Parameters.Add(new SqlParameter("@Diachi", SqlDbType.NVarChar, 250) { Value = model.Diachi });
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.Bit) { Value = model.TrangThai });

                    if (model.list_json_chitiethoadon != null)
                    {
                        SqlParameter parameter = cmd.Parameters.Add("@list_json_chitiethoadon", SqlDbType.NVarChar, -1);
                        parameter.Value = JsonConvert.SerializeObject(model.list_json_chitiethoadon);
                    }
                    else
                    {
                        cmd.Parameters.Add("@list_json_chitiethoadon", SqlDbType.NVarChar, -1).Value = DBNull.Value;
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
    //Update hóa đơn
    public void UpdateHoaDon(int maHoaDon, string tenKH, string diachi, bool trangThai, string list_json_chitiethoadon)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("sp_hoa_don_update", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MaHoaDon", SqlDbType.Int) { Value = maHoaDon });
                cmd.Parameters.Add(new SqlParameter("@TenKH", SqlDbType.NVarChar, 50) { Value = tenKH });
                cmd.Parameters.Add(new SqlParameter("@Diachi", SqlDbType.NVarChar, 250) { Value = diachi });
                cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.Bit) { Value = trangThai });

                if (!string.IsNullOrEmpty(list_json_chitiethoadon))
                {
                    cmd.Parameters.Add(new SqlParameter("@list_json_chitiethoadon", SqlDbType.NVarChar, -1) { Value = list_json_chitiethoadon });
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@list_json_chitiethoadon", SqlDbType.NVarChar, -1) { Value = DBNull.Value });
                }

                cmd.ExecuteNonQuery();
            }
        }
    }
}
