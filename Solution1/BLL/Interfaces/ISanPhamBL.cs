﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISanPhamBL
    {
        SanPhamModel GetByID(int id);
        void InsertSanPham(SanPhamModel sanPham);
        void UpdateSanPham(SanPhamModel sanPham);
        void DeleteSanPham(int id);
        List<SanPhamModel> GetAllSanPhams();
        List<SanPhamModel> SearchSanPhams(string keyword, int pageIndex, int pageSize, out long total);
    }
}
