using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GUI
{
    public partial class GUI_DangNhap : Form
    {
        public GUI_DangNhap()
        {
            InitializeComponent();
        }
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI_DangNhap());
        }
        SqlConnection conn;
        string connString = @"Data Source=WINDOWS-11\SQLEXPRESS01;Initial Catalog=DOAN1_NUOCHOA;Integrated Security=True";

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                string query = string.Empty;
                Form mainForm = null;

                // Kiểm tra đăng nhập với bảng DangNhap
                query = "SELECT * FROM DangNhap WHERE TenDN = @TenDN and MK = @MatKhau";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDN", txtTenDN.Text);
                cmd.Parameters.AddWithValue("@MatKhau", txtMK.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    mainForm = new GUI_TrangChuMain();
                }
                else
                {
                    reader.Close();

                    // Kiểm tra đăng nhập với bảng DangNhapNV
                    query = "SELECT * FROM DangNhapNhanVien WHERE TenDN = @TenDN and MK = @MatKhau";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenDN", txtTenDN.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMK.Text);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        mainForm = new GUI_TrangChuNV();
                    }
                }

                reader.Close();

                if (mainForm != null)
                {

                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Hãy nhập lại", "Thông báo");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối!", "Thông báo");
            }
            conn.Close();
        }

        private void ckbHTMK_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbHTMK.Checked == true)
            {
                txtMK.PasswordChar = (char)0;
            }
            else
            {
                txtMK.PasswordChar = '.';
            }
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '.';
        }
    }
    
}
