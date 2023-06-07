using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HoaDonXuat
    {
        public int MaHDX { get; set; }
        public int MaNV { get; set; }
        public int MaKH { get; set; }
        public DateTime NgayBan { get; set; }

        public DTO_HoaDonXuat(int maHDX, int maNV, int maKH, DateTime nb)
        {
            this.MaHDX = maHDX;
            this.MaNV = maNV;
            this.MaKH = maKH;
            this.NgayBan = nb;
        }
    }
}
