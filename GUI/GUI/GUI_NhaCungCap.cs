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
    public partial class GUI_NhaCungCap : Form
    {
        BUS_NhaCungCap busncc = new BUS_NhaCungCap();
        public GUI_NhaCungCap()
        {
            InitializeComponent();
        }

        private void GUI_NhaCungCap_Load(object sender, EventArgs e)
        {
            dgvNCC.DataSource = busncc.getNhaCC();
            dgvNCC.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns["TenNCC"].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns["SĐTNCC"].HeaderText = "Số điện thoại";
            dgvNCC.Columns["DiaChiNCC"].HeaderText = "Địa chỉ";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNCC.Text);
            string ten = txtTenNCC.Text;
            string sdt = txtSĐTNCC.Text;
            string dc = txtDiaChiNCC.Text;


            DTO_NhaCungCap NCC = new DTO_NhaCungCap(ma, ten, sdt, dc);
            if (busncc.kiemtramatrung(ma) == 1)
            {
                MessageBox.Show("Mã trùng");
            }
            else
            {
                if (busncc.themNCC(NCC) == true)
                {
                    MessageBox.Show("Thêm thành công!");
                    dgvNCC.DataSource = busncc.getNhaCC();
                }
            }
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            txtMaNCC.Text = dgvNCC[0, hang].Value.ToString();
            txtTenNCC.Text = dgvNCC[1, hang].Value.ToString();
            txtSĐTNCC.Text = dgvNCC[2, hang].Value.ToString();
            txtDiaChiNCC.Text = dgvNCC[3, hang].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNCC.Text);
            string ten = txtTenNCC.Text;
            string sdt = txtSĐTNCC.Text;
            string dc = txtDiaChiNCC.Text;

            DTO_NhaCungCap NCC = new DTO_NhaCungCap(ma, ten, sdt, dc);

            if (busncc.suaNCC(NCC) == true)
            {
                MessageBox.Show("Sửa thành công!");
                dgvNCC.DataSource = busncc.getNhaCC();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNCC.Text);
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {


                if (busncc.xoaNCC(ma) == true)
                {
                    MessageBox.Show("Xóa thành công!");
                    dgvNCC.DataSource = busncc.getNhaCC();
                }
            }
        }

        private void btnTKNCC_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTKNCC.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                DataTable dt;
                if (rdoMaNCC.Checked)
                {
                    int ma = int.Parse(tuKhoa);
                    dt = busncc.TimKiemSanPhamTheoMa(ma);
                }
                else
                {
                    dt = busncc.TimKiemSanPhamTheoTen(tuKhoa);
                }

                dgvNCC.DataSource = dt;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
