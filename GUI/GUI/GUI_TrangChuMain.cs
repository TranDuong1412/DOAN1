using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI_TrangChuMain : Form
    {
        public GUI_TrangChuMain()
        {
            InitializeComponent();
        }

        private void btnQLSP_Click(object sender, EventArgs e)
        {
            GUI_SanPham f = new GUI_SanPham();
            f.Show();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            GUI_NhanVien f = new GUI_NhanVien();
            f.Show();
        }

        private void GUI_TrangChuMain_Load(object sender, EventArgs e)
        {

        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            GUI_KhachHang f = new GUI_KhachHang();
            f.Show();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            GUI_NhaCungCap f = new GUI_NhaCungCap();
            f.Show();
        }

        private void btnHDN_Click(object sender, EventArgs e)
        {
            GUI_HoaDonNhap f = new GUI_HoaDonNhap();
            f.Show();
        }

        private void btnHDB_Click(object sender, EventArgs e)
        {
            GUI_HoaDonXuat f = new GUI_HoaDonXuat();
            f.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
