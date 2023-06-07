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
    public class DAL_ChiTietHDN:DBConnec
    {
        DAL_SanPham dasp = new DAL_SanPham();

        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dskh ra ngoài màn hình
        public DataTable getCTHDN()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from ChiTietHDN", conn);
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
        public int kiemtramatrung(int maCTHDN)
        {
            int i = 0;
            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaCTHDN", maCTHDN);
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

        public int themCTHDN(DTO_ChiTietHDN CTHDN)
        {

            if (CTHDN.SoLuong < 0)
            {
                // Trả về mã lỗi -2 cho số lượng âm
                return -2;
            }

            string sql = "Insert into ChiTietHDN values(N'" + CTHDN.MaCTHDN + "',N'" + CTHDN.MaHDN + "',N'" + CTHDN.MaSP + "',N'" + CTHDN.SoLuong + "','" + CTHDN.Gia + "','" + CTHDN.TongTien + "')";
            thucthisql(sql);

            // Trả về mã thành công 1
            return 1;
        }
        public int suaCTHDN(DTO_ChiTietHDN CTHDN)
        {

            if (CTHDN.SoLuong < 0)
            {
                // Trả về mã lỗi -2 cho số lượng âm
                return -2;
            }
            string sql = "Update ChiTietHDN set MaHDN = N'" + CTHDN.MaHDN + "', MaSP = '" + CTHDN.MaSP + "',SoLuong = '" + CTHDN.SoLuong + "',Gia = '" + CTHDN.Gia + "',TongTien = '" + CTHDN.TongTien + "' where MaCTHDN = '" + CTHDN.MaCTHDN + "'";
            thucthisql(sql);
            //
            dasp.ThemSoLuongSanPham(CTHDN.MaSP, CTHDN.SoLuong);
            //
            // Trả về mã thành công 1

            return 1;
        }
        public bool xoaCTHDN(int maCTHDN)
        {
            {
                string sql = "Delete from ChiTietHDN where MaCTHDN = '" + maCTHDN + "'";

                thucthisql(sql);
                return true;
            }
        }
        //

        public decimal TinhTongTien(string maHDN)
        {

            decimal Tongtien = 0;

            //using (SqlConnection connection = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT SUM(tongtien) FROM ChiTietHDN WHERE MaHDN = @MaHDN";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@MaHDN", maHDN);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Tongtien = Convert.ToDecimal(result);
                    }
                }
            }
            conn.Close();
            return Tongtien;

        }
    }
}
