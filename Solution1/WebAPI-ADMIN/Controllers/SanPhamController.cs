using BLL;
using BLL.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI_ADMIN.Controllers
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
        //Tìm kiếm sản phẩm theo mã ID
        [Route("get-by-id-SanPham/{id}")]
        [HttpGet]
        public SanPhamModel getById(int id)
        {
            return _sanPhamBL.GetByID(id);
        }

        // Thêm mới sản phẩm
        [Route("create-SanPham")]
        [HttpPost]
        public IActionResult CreateSanPham([FromBody] SanPhamModel model)
        {
            try
            {
                _sanPhamBL.InsertSanPham(model);
                return Ok(new { message = "Sản phẩm đã được thêm thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi: {ex.Message}" });
            }
        }
        //Update Sản Phẩm
        [Route("update-SanPham")]
        [HttpPut]
        public IActionResult updateKhachHang([FromBody] SanPhamModel model)
        {
            try
            {
                _sanPhamBL.UpdateSanPham(model);
                return Ok("Đã update sản phẩm thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi:{ex.Message}");
            }
        }
        //Xóa Sản Phẩm
        [Route("delete-SanPham")]
        [HttpDelete]
        public IActionResult deleteSanPham(int id)
        {
            try
            {
                _sanPhamBL.DeleteSanPham(id);
                return Ok($"Sản Phẩm với mã sản phẩm {id} đã được xóa thành công");
            }
            catch(Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");

            }
        }
        //Lấy toàn bộ danh sách sản phẩm
        [Route("get-all-SanPham")]
        [HttpGet]
        public List<SanPhamModel> GetAllSanPhams()
        {

            return _sanPhamBL.GetAllSanPhams();
        }
        //Tìm kiếm sản phẩm
        [Route("Serch-SanPham")]
        [HttpGet]
        public ActionResult<IEnumerable<SanPhamModel>> SearchSanPham(string keyword, int pageIndex, int pageSize)
        {
            try
            {
                long total;
                var sanPhams=_sanPhamBL.SearchSanPhams(keyword, pageIndex, pageSize, out total);
                return Ok(new { data=sanPhams,totalTrang=total});

            }
            catch(Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
    }
    
}
