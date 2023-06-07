using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChiTietHDN
    {
        public int MaCTHDN { get; set; }
        public int MaHDN { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal TongTien { get; set; }

        public DTO_ChiTietHDN(int maCT, int maHDN, string masp, int sl, decimal gia, decimal tt)
        {
            this.MaCTHDN = maCT;
            this.MaHDN = maHDN;
            this.MaSP = masp;
            this.SoLuong = sl;
            this.Gia = gia;
            this.TongTien = tt;
        }
    }
}
