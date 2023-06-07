using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_NhanVien : DBConnec
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dsnv ra ngoài màn hình
        public DataTable getNhanVien()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from NhanVien", conn);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        void thucthisql(string sql)
        {
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public int kiemtramatrung(int ma)
        {
            int i = 0;
            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNV", ma);
                i = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                // Xử lý lỗi kết nối
            }
            finally
            {
                conn.Close();
            }
            return i;
        }

        public bool themNV(DTO_NhanVien NV)
        {
            string ngay = string.Format("{0}/{1}/{2}", NV.NgaySinh.Year, NV.NgaySinh.Month, NV.NgaySinh.Day);
            string sql = "Insert into NhanVien values('" + NV.MaNV + "',N'" + NV.TenNV + "','" + NV.SĐTNV + "',N'" + NV.MatKhau + "',N'" + NV.ChucVu + "','" + ngay + "',N'" + NV.GioiTinh + "')";

            thucthisql(sql);
            return true;
        }
        public bool suaNV(DTO_NhanVien NV)
        {
            string ngay = string.Format("{0}/{1}/{2}", NV.NgaySinh.Year, NV.NgaySinh.Month, NV.NgaySinh.Day);
            string sql = "Update NhanVien set TenNV = N'" + NV.TenNV + "', SĐTNV = N'" + NV.SĐTNV + "',MatKhau = N'" + NV.MatKhau + "', ChucVu = N'" + NV.ChucVu + "',NgaySinh = '" + ngay + "',GioiTinh = N'" + NV.GioiTinh + "' where MaNV = '" + NV.MaNV + "'";

            thucthisql(sql);
            return true;
        }
        public bool xoaNV(int ma)
        {
            {
                string sql = "Delete from NhanVien where MaNV = '" + ma + "'";

                thucthisql(sql);
                return true;
            }
        }



        //
        public DataTable TimKiemSanPhamTheoMa(int ma)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM NhanVien WHERE MaNV = @MaNV", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaNV", ma);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable TimKiemSanPhamTheoTen(string ten)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM NhanVien WHERE TenNV LIKE '%' + @TenNV + '%'", conn);
            da.SelectCommand.Parameters.AddWithValue("@TenNV", ten);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;

        }



        //
        public bool KiemTraTonTaiMaNhanVien(int maNhanVien)
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNhanVien";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count > 0;
        }
    }

}

