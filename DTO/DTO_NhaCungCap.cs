using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_NhaCungCap
    {
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string SĐTNCC { get; set; }
        public string DiaChiNCC { get; set; }

        public DTO_NhaCungCap(int ma, string ten, string sdt, string dc)
        {
            this.MaNCC = ma;
            this.TenNCC = ten;
            this.SĐTNCC = sdt;
            this.DiaChiNCC = dc;
        }
    }
}
