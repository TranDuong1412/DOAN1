using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HoaDonNhap
    {
        public int MaHDN { get; set; }
        public int MaNV { get; set; }
        public int MaNCC { get; set; }
        public DateTime NgayNhap { get; set; }

        public DTO_HoaDonNhap(int maHDN, int maNV, int maNCC, DateTime nn)
        {
            this.MaHDN = maHDN;
            this.MaNV = maNV;
            this.MaNCC = maNCC;
            this.NgayNhap = nn;
        }
    }
}
