using BLL.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetHoaDonById(int id)
        {
            HoaDonModel hoadon = _hoaDonBL.GetHoadonByID(id);

            if (hoadon == null)
            {
                return NotFound();
            }

            return Ok(hoadon);
        }
    }
}
