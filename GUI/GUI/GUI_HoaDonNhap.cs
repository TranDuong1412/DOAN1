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
    public partial class GUI_HoaDonNhap : Form
    {
        private BUS_HoaDonNhap busHDN = new BUS_HoaDonNhap();
        private BUS_NhanVien busnv = new BUS_NhanVien();
        private BUS_NhaCungCap busNCC = new BUS_NhaCungCap();
        public GUI_HoaDonNhap()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int maHDN = int.Parse(txtMaHDN.Text);
            int maNV = int.Parse(txtMaNV.Text);
            int maNCC = int.Parse(txtMaNCC.Text);
            DateTime ngayNhap = dtNgayNhap.Value;


            if (!busnv.KiemTraTonTaiMaNhanVien(maNV))
            {
                MessageBox.Show("Mã nhân viên không tồn tại trong cơ sở dữ liệu!");
                return;
            }

            // Kiểm tra sự tồn tại của mã nhà cung cấp trong cơ sở dữ liệu
            if (!busNCC.KiemTraTonTaiMaNhaCungCap(maNCC))
            {
                MessageBox.Show("Mã nhà cung cấp không tồn tại trong cơ sở dữ liệu!");
                return;
            }

            DTO_HoaDonNhap HDN = new DTO_HoaDonNhap(maHDN, maNV, maNCC, ngayNhap);
            if (busHDN.kiemtramatrung(maHDN) == 1)
            {
                MessageBox.Show("Mã trùng");
            }
            else
            {
                if (busHDN.themHDN(HDN) == true)
                {
                    MessageBox.Show("Thêm thành công!");
                    dgvHDN.DataSource = busHDN.getHoaDonNhap();
                   
                    GUI_ChiTietHDN f = new GUI_ChiTietHDN();
                    f.maHDN = maHDN;
                    f.Show();
                    
                }
            }
        }

        private void GUI_HoaDonNhap_Load(object sender, EventArgs e)
        {
            dgvHDN.DataSource = busHDN.getHoaDonNhap();
            dgvHDN.Columns["MaHDN"].HeaderText = "Mã hóa đơn nhập";
            dgvHDN.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvHDN.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            dgvHDN.Columns["NgayNhap"].HeaderText = "Ngày nhập";
        }

        private void dgvHDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            txtMaHDN.Text = dgvHDN[0, hang].Value.ToString();
            txtMaNV.Text = dgvHDN[1, hang].Value.ToString();
            txtMaNCC.Text = dgvHDN[2, hang].Value.ToString();
            dtNgayNhap.Text = dgvHDN[3, hang].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int maHDN = int.Parse(txtMaHDN.Text);
            int maNV = int.Parse(txtMaNV.Text);
            int maNCC = int.Parse(txtMaNCC.Text);
            DateTime ngayNhap = dtNgayNhap.Value;


            bool isMaNhanVienValid = busnv.KiemTraTonTaiMaNhanVien(maNV);
            bool isMaNhaCungCapValid = busNCC.KiemTraTonTaiMaNhaCungCap(maNCC);

            if (!isMaNhanVienValid)
            {
                MessageBox.Show("Mã nhân viên không tồn tại trong cơ sở dữ liệu!");
                return;
            }

            if (!isMaNhaCungCapValid)
            {
                MessageBox.Show("Mã nhà cung cấp không tồn tại trong cơ sở dữ liệu!");
                return;
            }


            DTO_HoaDonNhap HDN = new DTO_HoaDonNhap(maHDN, maNV, maNCC, ngayNhap);

            if (busHDN.suaHDN(HDN) == true)
            {
                MessageBox.Show("Sửa thành công!");
                dgvHDN.DataSource = busHDN.getHoaDonNhap();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maHDN = int.Parse(txtMaHDN.Text);
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {


                if (busHDN.xoaHDN(maHDN) == true)
                {
                    MessageBox.Show("Xóa thành công!");
                    dgvHDN.DataSource = busHDN.getHoaDonNhap();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTKHDN_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTKHDN.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                DataTable dt;
                if (rdoMaHDN.Checked)
                {
                    int ma = int.Parse(tuKhoa);
                    dt = busHDN.TimKiemSanPhamTheoMa(tuKhoa);
                }
                else
                {
                    dt = busHDN.TimKiemSanPhamTheoTen(tuKhoa);
                }

                dgvHDN.DataSource = dt;
            }
        }

        private void btnCTHDN_Click(object sender, EventArgs e)
        {
            GUI_ChiTietHDN f = new GUI_ChiTietHDN();
            f.Show();
        }
        public int maHDN { get; set; }
    }

}
