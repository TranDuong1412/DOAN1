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
    public class DAL_HoaDonXuat:DBConnec
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dskh ra ngoài màn hình
        public DataTable getHoaDonXuat()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from HoaDonXuat", conn);
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
        public int kiemtramatrung(int maHDX)
        {
            int i = 0;
            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM HoaDonXuat WHERE MaHDX = @MaHDX";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHDX", maHDX);
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

        public bool themHDX(DTO_HoaDonXuat HDX)
        {
            string ngay = string.Format("{0}/{1}/{2}", HDX.NgayBan.Year, HDX.NgayBan.Month, HDX.NgayBan.Day);
            string sql = "Insert into HoaDonXuat values(N'" + HDX.MaHDX + "',N'" + HDX.MaNV + "',N'" + HDX.MaKH + "',N'" + ngay + "')";

            thucthisql(sql);
            return true;
        }
        public bool suaHDX(DTO_HoaDonXuat HDX)
        {
            string ngay = string.Format("{0}/{1}/{2}", HDX.NgayBan.Year, HDX.NgayBan.Month, HDX.NgayBan.Day);
            string sql = "Update HoaDonXuat set MaNV = N'" + HDX.MaNV + "', MaKH = '" + HDX.MaKH + "',NgayBan = '" + ngay + "' where MaHDX = '" + HDX.MaHDX + "'";

            thucthisql(sql);
            return true;
        }
        public bool xoaHDX(int maHDX)
        {
            {
                string sql = "Delete from HoaDonXuat where MaHDX = '" + maHDX + "'";

                thucthisql(sql);
                return true;
            }
        }



        //
        public DataTable TimKiemSanPhamTheoMa(string maHDX)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM HoaDonXuat WHERE MaHDX = @MaHDX", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaHDX", maHDX);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable TimKiemSanPhamTheoTen(string maNV)
        {
            conn.Open(); da = new SqlDataAdapter("SELECT * FROM HoaDonXuat WHERE MaNV LIKE '%' + @MaNV + '%'", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaNV", maNV);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        //
        public bool KiemTraTonTaiHoaDonXuat(int maHoaDonXuat)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HoaDonXuat WHERE MaHDX = @MaHoaDonXuat", conn);
            cmd.Parameters.AddWithValue("@MaHoaDonXuat", maHoaDonXuat);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }
    }
}
