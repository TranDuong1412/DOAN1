using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChiTietHDX
    {
        public int MaCTHDX { get; set; }
        public int MaHDX { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal TongTien { get; set; }

        public DTO_ChiTietHDX(int maCT, int maHDX, string masp, int sl, decimal gia, decimal tt)
        {
            this.MaCTHDX = maCT;
            this.MaHDX = maHDX;
            this.MaSP = masp;
            this.SoLuong = sl;
            this.Gia = gia;
            this.TongTien = tt;
        }

    }
}
