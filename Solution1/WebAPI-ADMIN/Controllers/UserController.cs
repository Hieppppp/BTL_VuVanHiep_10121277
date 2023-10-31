using BLL.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBL _userBL;
        public UserController(IUserBL userBL)
        {
            this._userBL = userBL;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userBL.Login(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            }
            return Ok(new { taikhoan = user.TenTaiKhoan, email = user.Email, token = user.token });
        }
        [Route("get-by-id")]
        [HttpGet]
        public UserModel TimKiemTaiKhoanByMa(int maTaiKhoan)
        {
            UserModel user = _userBL.TimKiemTaiKhoanByMa(maTaiKhoan);
            return user;
        }
        [Route("get-all-users")]
        [HttpGet]
        public List<UserModel> GetAllTaiKhoans()
        {
            
            return _userBL.GetAllTaiKhoans();
        }
    }
}
