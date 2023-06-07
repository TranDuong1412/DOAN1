using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Data.SqlClient;

namespace GUI
{
    public partial class GUI_KhachHang : Form
    {
        BUS_KhachHang buskh = new BUS_KhachHang();
        public GUI_KhachHang()
        {
            InitializeComponent();
        }

        private void GUI_KhachHang_Load(object sender, EventArgs e)
        {
            dgvQLKH.DataSource = buskh.getKhachHang();
            dgvQLKH.Columns["MaKH"].HeaderText = "Mã khách hàng";
            dgvQLKH.Columns["TenKH"].HeaderText = "Tên khách hàng";
            dgvQLKH.Columns["SĐTKH"].HeaderText = "Số điện thoại";
            dgvQLKH.Columns["DiaChiKH"].HeaderText = "Địa chỉ";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaKH.Text);
            string ten = txtTenKH.Text;
            string sdt = txtSĐTKH.Text;
            string dc = txtDiaChiKH.Text;


            DTO_KhachHang KH = new DTO_KhachHang(ma, ten, sdt, dc);
            if (buskh.kiemtramatrung(ma) == 1)
            {
                MessageBox.Show("Mã trùng");
            }
            else
            {
                if (buskh.themKH(KH) == true)
                {
                    MessageBox.Show("Thêm thành công!");
                    dgvQLKH.DataSource = buskh.getKhachHang();
                }

            }
        }

        private void dgvQLKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            txtMaKH.Text = dgvQLKH[0, hang].Value.ToString();
            txtTenKH.Text = dgvQLKH[1, hang].Value.ToString();
            txtSĐTKH.Text = dgvQLKH[2, hang].Value.ToString();
            txtDiaChiKH.Text = dgvQLKH[3, hang].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaKH.Text);
            string ten = txtTenKH.Text;
            string sdt = txtSĐTKH.Text;
            string dc = txtDiaChiKH.Text;

            DTO_KhachHang KH = new DTO_KhachHang(ma, ten, sdt, dc);

            if (buskh.suaKH(KH) == true)
            {
                MessageBox.Show("Sửa thành công!");
                dgvQLKH.DataSource = buskh.getKhachHang();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaKH.Text);
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (buskh.xoaKH(ma) == true)
                {
                    MessageBox.Show("Xóa thành công!");
                    dgvQLKH.DataSource = buskh.getKhachHang();
                }
            }
        }

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTKKH.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                DataTable dt;
                if (rdoMaKH.Checked)
                {
                    int ma = int.Parse(tuKhoa);
                    dt = buskh.TimKiemSanPhamTheoMa(ma);
                }
                else
                {
                    dt = buskh.TimKiemSanPhamTheoTen(tuKhoa);
                }

                dgvQLKH.DataSource = dt;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }    

}
