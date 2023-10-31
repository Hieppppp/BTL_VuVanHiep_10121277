using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace BLL
{
    public class SanPhamBL:ISanPhamBL
    {
        private ISanPhamDA _sanPhamDA;

        public SanPhamBL(ISanPhamDA sanPhamDA)
        {
            _sanPhamDA = sanPhamDA;
        }
        public SanPhamModel GetSanPhamById(int maSanPham)
        {
            return _sanPhamDA.GetSanPhamById(maSanPham);
        }
    }
}
