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
    public class DAL_NhaCungCap:DBConnec
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dsncc ra ngoài màn hình
        public DataTable getNhaCC()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from NhaCungCap", conn);
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
                string sql = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNCC", ma);
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

        public bool themNCC(DTO_NhaCungCap NCC)
        {
            string sql = "Insert into NhaCungCap values(N'" + NCC.MaNCC + "',N'" + NCC.TenNCC + "',N'" + NCC.SĐTNCC + "',N'" + NCC.DiaChiNCC + "')";

            thucthisql(sql);
            return true;
        }
        public bool suaNCC(DTO_NhaCungCap NCC)
        {
            string sql = "Update NhaCungCap set TenNCC = N'" + NCC.TenNCC + "', SĐTNCC = '" + NCC.SĐTNCC + "',DiaChiNCC=N'" + NCC.DiaChiNCC + "' where MaNCC = '" + NCC.MaNCC + "'";

            thucthisql(sql);
            return true;
        }
        public bool xoaNCC(int ma)
        {
            {
                string sql = "Delete from NhaCungCap where MaNCC = '" + ma + "'";

                thucthisql(sql);
                return true;
            }
        }



        //
        public DataTable TimKiemSanPhamTheoMa(int ma)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM NhaCungCap WHERE MaNCC = @MaNCC", conn);
            da.SelectCommand.Parameters.AddWithValue("@MaNCC", ma);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable TimKiemSanPhamTheoTen(string ten)
        {
            conn.Open();
            da = new SqlDataAdapter("SELECT * FROM NhaCungCap WHERE TenNCC LIKE '%' + @TenNCC + '%'", conn);
            da.SelectCommand.Parameters.AddWithValue("@TenNCC", ten);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public bool KiemTraTonTaiMaNhaCungCap(int maNhaCungCap)
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNhaCungCap";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count > 0;
        }
    }
}
