using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_NhanVien
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string SĐTNV { get; set; }
        public string MatKhau { get; set; }
        public string ChucVu { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        public DTO_NhanVien(int ma, string ten, string sdt, string mk, string cv, DateTime ngs, string gt)
        {
            this.MaNV = ma;
            this.TenNV = ten;
            this.SĐTNV = sdt;
            this.MatKhau = mk;
            this.ChucVu = cv;
            this.NgaySinh = ngs;
            this.GioiTinh = gt;
        }
    }
}
