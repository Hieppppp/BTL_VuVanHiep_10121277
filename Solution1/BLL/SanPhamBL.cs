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
        public SanPhamModel GetByID(int id)
        {
            return _sanPhamDA.GetByID(id);
        }
        public void InsertSanPham(SanPhamModel sanPham)
        {
            _sanPhamDA.InsertSanPham(sanPham);
        }

        public void  UpdateSanPham(SanPhamModel sanPham)
        {
            _sanPhamDA.UpdateSanPham(sanPham);
        }

        public void DeleteSanPham(int id)
        {
            _sanPhamDA.DeleteSanPham(id);
        }
        public List<SanPhamModel> GetAllSanPhams()
        {
            return _sanPhamDA.GetAllSanPhams();
        }
        public List<SanPhamModel> SearchSanPhams(string keyword, int pageIndex, int pageSize, out long total)
        {
            return _sanPhamDA.SearchSanPhams(keyword, pageIndex, pageSize, out total);
        }

    }
}
