using BLL;
using BLL.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers
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
    }
}
