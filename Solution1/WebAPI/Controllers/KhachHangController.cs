using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using BLL;
using BLL.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private  IKhachHangBL _khachHangBL;

        public KhachHangController(IKhachHangBL khachHangBL)
        {
            _khachHangBL = khachHangBL;
        }
        
        [Route("create-khachhang")]
        [HttpPost]
        public IActionResult CreateKhachHang([FromBody] KhachHangModel model)
        {
            try
            {
                _khachHangBL.InsertKhachHang(model.TenKh, model.GioiTinh, model.DiaChi, model.Sdt, model.Email);
                return Ok("Khách hàng đã được thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi: {ex.Message}");
            }
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public KhachHangModel getById(int id)
        {
            return _khachHangBL.getById(id);
        }
        [Route("update-khachhang")]
        [HttpPost]
        public KhachHangModel UpdateKhachHang([FromBody] KhachHangModel model)
        {
            _khachHangBL.upDateKhachHang(model.Id, model.TenKh, model.GioiTinh, model.DiaChi, model.Sdt, model.Email);
            return model;
        }
        //[Route("delete-khachhang")]
        //[HttpPost]
        //public KhachHangModel DeleteKhachHang([FromBody] KhachHangModel model)
        //{
        //    _khachHangBL.deleteKhachHang(model.Id);
        //    return model;
        //}


    }
}
