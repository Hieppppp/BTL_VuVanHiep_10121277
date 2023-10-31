using BLL;
using BLL.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private ISanPhamBL _sanPhamBL;
        public SanPhamController(ISanPhamBL sanPhamBL)
        {
            _sanPhamBL = sanPhamBL;
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetSanPham(int id)
        {
            try
            {
                SanPhamModel sanPhamModel = _sanPhamBL.GetSanPhamById(id);

                if (sanPhamModel != null)
                {
                    return Ok(sanPhamModel);
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
    }
}
