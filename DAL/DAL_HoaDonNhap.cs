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
    public class DAL_HoaDonNhap:DBConnec
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dskh ra ngoài màn hình
        public DataTable getHoaDonNhap()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from HoaDonNhap", conn);
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
        public int kiemtramatrung(int maHDN)
        {
            int i = 0;
            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHDN", maHDN);
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

        public bool themHDN(DTO_HoaDonNhap HDN)
        {
            string ngay = string.Format("{0}/{1}/{2}", HDN.NgayNhap.Year, HDN.NgayNhap.Month, HDN.NgayNhap.Day);
            string sql = "Insert into HoaDonNhap values(N'" + HDN.MaHDN + "',N'" + HDN.MaNV + "',N'" + HDN.MaNCC + "',N'" + ngay + "')";

            thucthisql(sql);
            return true;
        }
        public bool suaHDN(DTO_HoaDonNhap HDN)
        {
            string ngay = string.Format("{0}/{1}/{2}", HDN.NgayNhap.Year, HDN.NgayNhap.Month, HDN.NgayNhap.Day);
            string sql = "Update HoaDonNhap set MaNV = N'" + HDN.MaNV + "', MaNCC = '" + HDN.MaNCC + "',NgayNhap = '" + ngay + "' where MaHDN = '" + HDN.MaHDN + "'";

            thucthisql(sql);
            return true;
        }
        public bool xoaHDN(int maHDN)
        {
            {
                string sql = "Delete from HoaDonNhap where MaHDN = '" + maHDN + "'";

                thucthisql(sql);
                return true;
            }
        }



        //
        public DataTable TimKiemSanPhamTheoMa(string maHDN)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM HoaDonNhap WHERE MaHDN = @MaHDN", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaHDN", maHDN);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable TimKiemSanPhamTheoTen(string maNV)
        {
            conn.Open(); da = new SqlDataAdapter("SELECT * FROM HoaDonNhap WHERE MaNV LIKE '%' + @MaNV + '%'", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaNV", maNV);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        //
        public bool KiemTraTonTaiHoaDonNhap(int maHoaDonNhap)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HoaDonNhap WHERE MaHDN = @MaHoaDonNhap", conn);
            cmd.Parameters.AddWithValue("@MaHoaDonNhap", maHoaDonNhap);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }
        //
        public int ThemHoaDonNhap(DTO_HoaDonNhap HDN)
        {
            string query = $"INSERT INTO HoaDonNhap(MaHDN, MaNV,MaNCC, NgayNhap) VALUES({HDN.MaHDN}, {HDN.MaNV}, {HDN.MaNCC}, '{HDN.NgayNhap.ToString("yyyy-MM-dd")}')";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                // Lấy mã hóa đơn nhập vừa thêm và trả về
                query = "SELECT IDENT_CURRENT('HoaDonNhap')";
                cmd.CommandText = query;
                int maHDN = Convert.ToInt32(cmd.ExecuteScalar());

                return maHDN;
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ (Exception)
                // ...
            }
            finally
            {
                conn.Close();
            }

            return -1; // Trả về -1 nếu có lỗi
        }
    }
}
