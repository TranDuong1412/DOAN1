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
    public class DAL_SanPham:DBConnec
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dssp ra ngoài màn hình
        public DataTable getSanPham()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from SanPham", conn);
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
                string sql = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", ma);
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

    
        public int themSP(DTO_SanPham SP)
        {
            if (SP.Gia < 0)
            {
                // Trả về mã lỗi -1 cho giá âm
                return -1;
            }

            if (SP.SoLuong < 0)
            {
                // Trả về mã lỗi -2 cho số lượng âm
                return -2;
            }

            string sql = "Insert into SanPham values(N'" + SP.MaSP + "',N'" + SP.TenSP + "',N'" + SP.Gia + "','" + SP.SoLuong + "',N'" + SP.XuatXu + "')";
            thucthisql(sql);

            // Trả về mã thành công 1
            return 1;
        }

        public int suaSP(DTO_SanPham SP)
        {
            if (SP.Gia < 0)
            {
                // Trả về mã lỗi -1 cho giá âm
                return -1;
            }

            if (SP.SoLuong < 0)
            {
                // Trả về mã lỗi -2 cho số lượng âm
                return -2;
            }
            string sql = "Update SanPham set TenSP = N'" + SP.TenSP + "', Gia = '" + SP.Gia + "',SoLuong = '" + SP.SoLuong + "', XuatXu = N'" + SP.XuatXu + "' where MaSP = '" + SP.MaSP + "'";
            thucthisql(sql);

            // Trả về mã thành công 1
            return 1;
        }
        //

        public bool xoaSP(int ma)
        {
            {
                string sql = "Delete from SanPham where MaSP = '" + ma + "'";

                thucthisql(sql);
                return true;
            }
        }



        //
        public DataTable TimKiemSanPhamTheoMa(int ma)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM SanPham WHERE MaSP = @MaSP", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaSP", ma);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable TimKiemSanPhamTheoTen(string ten)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM SanPham WHERE TenSP LIKE '%' + @TenSP + '%'", conn);
            da.SelectCommand.Parameters.AddWithValue("@TenSP", ten);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public bool KiemTraTonTaiSanPham(string maSP)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SanPham WHERE MaSP = @masp", conn);
            cmd.Parameters.AddWithValue("@masp", maSP);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }

        public decimal LayGiaSanPham(string masp)
        {
            string query = "SELECT gia FROM SanPham WHERE MaSP = @masp";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@masp", masp);
            conn.Open();
            decimal gia = Convert.ToDecimal(cmd.ExecuteScalar());
            conn.Close();
            return gia;
        }

        public void ThemSoLuongSanPham(string masp, int soluong)
        {
            string sql = "Update SanPham Set SoLuong = SoLuong + " + soluong + " Where MaSP = N'" + masp + "'";
            thucthisql(sql);
        }
        public void GiamSoLuongSanPham(string masp, int soluong)
        {
            string sql = "Update SanPham Set SoLuong = SoLuong - " + soluong + " Where MaSP = N'" + masp + "'";
            thucthisql(sql);
        }
    }
}
