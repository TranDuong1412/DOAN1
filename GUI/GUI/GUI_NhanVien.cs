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
    public partial class GUI_NhanVien : Form
    {
        BUS_NhanVien busnv = new BUS_NhanVien();
        public GUI_NhanVien()
        {
            InitializeComponent();
        }

        private void GUI_NhanVien_Load(object sender, EventArgs e)
        {
            dgvDSNV.DataSource = busnv.getNhanVien();
            dgvDSNV.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvDSNV.Columns["TenNV"].HeaderText = "Tên nhân viên";
            dgvDSNV.Columns["SĐTNV"].HeaderText = "Số điện thoại";
            dgvDSNV.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dgvDSNV.Columns["ChucVu"].HeaderText = "Chức vụ";
            dgvDSNV.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvDSNV.Columns["GioiTinh"].HeaderText = "Giới tính";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNV.Text);
            string ten = txtTenNV.Text;
            string sdt = txtSDTNV.Text;
            string mk = txtMKNV.Text;
            string cv = cboCV.SelectedItem.ToString();
            DateTime ngs = DateTime.Parse(dtNSNV.Value.ToShortDateString());

            string gt = cboGT.SelectedItem != null ? cboGT.SelectedItem.ToString() : string.Empty;

            DTO_NhanVien NV = new DTO_NhanVien(ma, ten, sdt, mk, cv, ngs, gt);
            if (busnv.kiemtramatrung(ma) == 1)
            {
                MessageBox.Show("Mã trùng");
            }
            else
            {
                if (busnv.themNV(NV) == true)
                {
                    MessageBox.Show("Thêm thành công!");
                    dgvDSNV.DataSource = busnv.getNhanVien();
                }
            }
        }

        private void dgvDSNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int hang = e.RowIndex;
            txtMaNV.Text = dgvDSNV[0, hang].Value.ToString();
            txtTenNV.Text = dgvDSNV[1, hang].Value.ToString();
            txtSDTNV.Text = dgvDSNV[2, hang].Value.ToString();
            txtMKNV.Text = dgvDSNV[3, hang].Value.ToString();
            cboCV.SelectedItem = dgvDSNV[4, hang].Value.ToString();
            dtNSNV.Text = dgvDSNV[5, hang].Value.ToString();
            cboGT.SelectedItem = dgvDSNV[6, hang].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNV.Text);
            string ten = txtTenNV.Text;
            string sdt = txtSDTNV.Text;
            string mk = txtMKNV.Text;
            string cv = cboCV.SelectedItem.ToString();
            DateTime ngs = DateTime.Parse(dtNSNV.Value.ToShortDateString());
            string gt = cboGT.SelectedItem != null ? cboGT.SelectedItem.ToString() : string.Empty;


            DTO_NhanVien NV = new DTO_NhanVien(ma, ten, sdt, mk, cv, ngs, gt);

            if (busnv.suaNV(NV) == true)
            {
                MessageBox.Show("Sửa thành công!");

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNV.Text);
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {


                if (busnv.xoaNV(ma) == true)
                {
                    MessageBox.Show("Xóa thành công!");
                    dgvDSNV.DataSource = busnv.getNhanVien();
                }

            }
        }

        private void btnTKNV_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTKNV.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                DataTable dt;
                if (rdoMaNV.Checked)
                {
                    int ma = int.Parse(tuKhoa);
                    dt = busnv.TimKiemSanPhamTheoMa(ma);
                }
                else
                {
                    dt = busnv.TimKiemSanPhamTheoTen(tuKhoa);
                }

                dgvDSNV.DataSource = dt;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
