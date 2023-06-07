using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_SanPham
    {


        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public float Gia { get; set; }
        public float SoLuong { get; set; }
        public string XuatXu { get; set; }

        public DTO_SanPham(int ma, string ten, float gia, float sl, string xx)
        {
            this.MaSP = ma;
            this.TenSP = ten;
            this.Gia = gia;
            this.SoLuong = sl;
            this.XuatXu = xx;
        }
    }   
}
