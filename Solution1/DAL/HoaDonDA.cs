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
    //Xóa Hóa đơn
    public void deleteHoaDon(int id)
    {
        try
        {
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_delete_hoadon", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MaHoaDon", id));
                    command.ExecuteNonQuery();
                }


            }

        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    //Thống kê hóa đơn
    public List<ThongKeKhachModels> Search(int pageIndex, int pageSize, out long total, string ten_khach, DateTime? fr_NgayTao, DateTime? to_NgayTao)
    {
        total = 0;
        List<ThongKeKhachModels> thongKeKhachModels = new List<ThongKeKhachModels>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("sp_thong_ke_khach", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@page_index", pageIndex);
                cmd.Parameters.AddWithValue("@page_size", pageSize);
                cmd.Parameters.AddWithValue("@ten_khach", ten_khach);
                cmd.Parameters.AddWithValue("@fr_NgayTao", fr_NgayTao);
                cmd.Parameters.AddWithValue("@to_NgayTao", to_NgayTao);

                SqlParameter totalParameter = new SqlParameter("@total", SqlDbType.BigInt);
                totalParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(totalParameter);

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    total = (long)totalParameter.Value;
                    thongKeKhachModels = ConvertDataTableToList(dt);
                }
            }
        }

        return thongKeKhachModels;
    }

    private List<ThongKeKhachModels> ConvertDataTableToList(DataTable dt)
    {
        List<ThongKeKhachModels> list = new List<ThongKeKhachModels>();

        foreach (DataRow row in dt.Rows)
        {
            ThongKeKhachModels item = new ThongKeKhachModels
            {
                MaSanPham = Convert.ToInt32(row["MaSanPham"]),
                TenSanPham = row["TenSanPham"].ToString(),
                SoLuong = Convert.ToInt32(row["SoLuong"]),
                TongGia = Convert.ToDecimal(row["TongGia"]),
                NgayTao = Convert.ToDateTime(row["NgayTao"]),
                TenKH = row["TenKH"].ToString(),
                Diachi = row["Diachi"].ToString()
            };
            list.Add(item);
        }

        return list;
    }


}
