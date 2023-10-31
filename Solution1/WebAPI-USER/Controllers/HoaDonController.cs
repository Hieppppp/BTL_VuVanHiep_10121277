using BLL.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI_USER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private IHoaDonBL _hoaDonBL;
        public HoaDonController(IHoaDonBL hoaDonBL)
        {
            _hoaDonBL = hoaDonBL;
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetDatabyID(int id)
        {
            try
            {
                HoaDonModel hoaDonModel = _hoaDonBL.GetDatabyID(id);

                if (hoaDonModel != null)
                {
                    return Ok(hoaDonModel);
                }
                else
                {
                    return NotFound("Không tìm thấy hóa đơn");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }
        }
        [Route("create-hoadon")]
        [HttpPost]
        public IActionResult CreateHoaDon([FromBody] HoaDonModel model)
        {
            _hoaDonBL.CreateHoaDon(model);
            return Ok(model);
        }
        //Update hóa đơn
        [Route("update-hoadon")]
        [HttpPost]
        public IActionResult UpdateHoaDon([FromBody] HoaDonModel model)
        {
            try
            {
                // Lấy các thông tin cần thiết từ model và gọi phương thức của BL
                _hoaDonBL.UpdateHoaDon(model.MaHoaDon, model.TenKH, model.Diachi, model.TrangThai, JsonConvert.SerializeObject(model.list_json_chitiethoadon));
                return Ok("Cập nhật hóa đơn thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }
        }
        //Xóa khách hàng theo mã id
        [Route("delete-hoadon/{id}")]
        [HttpDelete]
        public IActionResult DeleteHoaDon(int id)
        {
            try
            {
                _hoaDonBL.deleteHoaDon(id);
                return Ok($"Hóa đơn với mã {id} đã được xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
        [Route("timkiem-hoadon/{id}")]
        [HttpPost]
        public IActionResult TimKiemHoaDon(int id)
        {
            try
            {
                _hoaDonBL.deleteHoaDon(id);
                return Ok($"Hóa đơn với mã {id} đã được xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string ten_khach = "";
                if (formData.Keys.Contains("ten_khach") && !string.IsNullOrEmpty(Convert.ToString(formData["ten_khach"]))) { ten_khach = Convert.ToString(formData["ten_khach"]); }
                DateTime? fr_NgayTao = null;
                if (formData.Keys.Contains("fr_NgayTao") && formData["fr_NgayTao"] != null && formData["fr_NgayTao"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["fr_NgayTao"].ToString());
                    fr_NgayTao = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                }
                DateTime? to_NgayTao = null;
                if (formData.Keys.Contains("to_NgayTao") && formData["to_NgayTao"] != null && formData["to_NgayTao"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["to_NgayTao"].ToString());
                    to_NgayTao = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
                }
                long total = 0;
                var data = _hoaDonBL.ThongKeKhach(page, pageSize, out total, ten_khach, fr_NgayTao, to_NgayTao);
                return Ok(
                    new
                    {
                        TotalItems = total,
                        Data = data,
                        Page = page,
                        PageSize = pageSize
                    }
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
