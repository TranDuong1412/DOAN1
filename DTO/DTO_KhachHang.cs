using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KhachHang
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string SĐTKH { get; set; }
        public string DiaChiKH { get; set; }

        public DTO_KhachHang(int ma, string ten, string sdt, string dc)
        {
            this.MaKH = ma;
            this.TenKH = ten;
            this.SĐTKH = sdt;
            this.DiaChiKH = dc;
        }
    }
}
