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
    public class DAL_KhachHang:DBConnec
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dskh ra ngoài màn hình
        public DataTable getKhachHang()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from KhachHang", conn);
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
                string sql = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKH", ma);
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

        public bool themKH(DTO_KhachHang KH)
        {
            string sql = "Insert into KhachHang values(N'" + KH.MaKH + "',N'" + KH.TenKH + "',N'" + KH.SĐTKH + "',N'" + KH.DiaChiKH + "')";

            thucthisql(sql);
            return true;
        }
        public bool suaKH(DTO_KhachHang KH)
        {
            string sql = "Update KhachHang set TenKH = N'" + KH.TenKH + "', SĐTKH = '" + KH.SĐTKH + "',DiaChiKH = N'" + KH.DiaChiKH + "' where MaKH = '" + KH.MaKH + "'";

            thucthisql(sql);
            return true;
        }
        public bool xoaKH(int ma)
        {
            {
                string sql = "Delete from KhachHang where MaKH = '" + ma + "'";

                thucthisql(sql);
                return true;
            }
        }



        //
        public DataTable TimKiemSanPhamTheoMa(int ma)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM KhachHang WHERE MaKH = @MaKH", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaKH", ma);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable TimKiemSanPhamTheoTen(string ten)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM KhachHang WHERE TenKH LIKE '%' + @TenKH + '%'", conn);
            da.SelectCommand.Parameters.AddWithValue("@TenKH", ten);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public bool KiemTraTonTaiMaKhachHang(int maKH)
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaKH", maKH);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count > 0;
        }
    }
}
