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
    public class DAL_ChiTietHDX:DBConnec
    {
        DAL_SanPham dasp = new DAL_SanPham();

        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        //hiển thị dskh ra ngoài màn hình
        public DataTable getCTHDX()
        {
            conn.Open();
            da = new SqlDataAdapter("Select * from ChiTietHDX", conn);
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
        public int kiemtramatrung(int maCTHDX)
        {
            int i = 0;
            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM ChiTietHDX WHERE MaCTHDX = @MaCTHDX";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaCTHDX", maCTHDX);
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

        public int themCTHDX(DTO_ChiTietHDX CTHDX)
        {
            if (CTHDX.SoLuong < 0)
            {
                // Trả về mã lỗi -2 cho số lượng âm
                return -2;
            }

            string sql = "Insert into ChiTietHDX values(N'" + CTHDX.MaCTHDX + "',N'" + CTHDX.MaHDX + "',N'" + CTHDX.MaSP + "',N'" + CTHDX.SoLuong + "','" + CTHDX.Gia + "','" + CTHDX.TongTien + "')";
            thucthisql(sql);

            // Trả về mã thành công 1
            return 1;

        }
        public int suaCTHDX(DTO_ChiTietHDX CTHDX)
        {
            if (CTHDX.SoLuong < 0)
            {
                // Trả về mã lỗi -2 cho số lượng âm
                return -2;
            }

            string sql = "Update ChiTietHDX set MaHDX = N'" + CTHDX.MaHDX + "', MaSP = '" + CTHDX.MaSP + "',SoLuong = '" + CTHDX.SoLuong + "',Gia = '" + CTHDX.Gia + "',TongTien = '" + CTHDX.TongTien + "' where MaCTHDX = '" + CTHDX.MaCTHDX + "'";
            thucthisql(sql);
            //
            dasp.GiamSoLuongSanPham(CTHDX.MaSP, CTHDX.SoLuong);
            //
            // Trả về mã thành công 1
            return 1;
        }
        public bool xoaCTHDX(int maCTHDX)
        {
            {
                string sql = "Delete from ChiTietHDX where MaCTHDX = '" + maCTHDX + "'";

                thucthisql(sql);
                return true;
            }
        }
        public decimal TinhTongTien(string maHDX)
        {
            decimal Tongtien = 0;


            {
                conn.Open();

                string query = "SELECT SUM(TongTien) FROM ChiTietHDX WHERE MaHDX = @MaHDX";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@MaHDX", maHDX);

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
