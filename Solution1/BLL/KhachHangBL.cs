﻿using DataModel;
using DAL;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class KhachHangBL:IKhachHangBL
    {
        private  IKhachHangDA _khachHangDA;

        public KhachHangBL(IKhachHangDA khachhang)
        {
            this._khachHangDA = khachhang;
        }

       
        public void InsertKhachHang(string tenKhachHang, bool gioiTinh, string diaChi, string sdt, string email)
        {
            _khachHangDA.InsertKhachHang(tenKhachHang, gioiTinh, diaChi, sdt, email);
        }

        public KhachHangModel getById(int id)
        {
            return _khachHangDA.GetByID(id);
        }
        public void upDateKhachHang(int id, string tenkh, bool gioitinh, string diachi, string sdt, string email)
        {
            _khachHangDA.upDateKhachHang(id, tenkh, gioitinh, diachi, sdt, email);
        }

        public void deleteKhachHang(int id)
        {
            _khachHangDA.deleteKhachHang(id);
        }



        public List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string ten_khach, string dia_chi)
        {
            return _khachHangDA.Search(pageIndex, pageSize, out total, ten_khach, dia_chi);
        }


    }
}