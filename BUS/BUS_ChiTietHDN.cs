using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace BUS
{
    public class BUS_ChiTietHDN
    {
        DAL_ChiTietHDN daCTHDN = new DAL_ChiTietHDN();
        DAL_SanPham dasp = new DAL_SanPham();
        public DataTable getCTHDN()
        {
            return daCTHDN.getCTHDN();
        }
        public int kiemtramatrung(int maCTHDN)
        {
            return daCTHDN.kiemtramatrung(maCTHDN);
        }
        public int themCTHDN(DTO_ChiTietHDN CTHDN)
        {
            //return daCTHDN.themCTHDN(CTHDN);
            int result = daCTHDN.themCTHDN(CTHDN);

            if (result == 1)
            {
                dasp.ThemSoLuongSanPham(CTHDN.MaSP, CTHDN.SoLuong);
            }

            return result;
        }
        public int suaCTHDN(DTO_ChiTietHDN CTHDN)
        {
            return daCTHDN.suaCTHDN(CTHDN);
        }
        public bool xoaCTHDN(int maCTHDN)
        {
            return daCTHDN.xoaCTHDN(maCTHDN);
        }
        //public bool KiemTraTonTaiHoaDonNhap(string maHoaDonNhap)
        //{
        //    return daCTHDN.KiemTraTonTaiHoaDonNhap(maHoaDonNhap);
        //}
        public decimal TinhTongTien(string maHDN)
        {
            return daCTHDN.TinhTongTien(maHDN);
        }
    }
}
