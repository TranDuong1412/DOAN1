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
    public partial class GUI_ChiTietHDX : Form
    {
        BUS_ChiTietHDX busCTHDX = new BUS_ChiTietHDX();
        private BUS_HoaDonXuat busHDX = new BUS_HoaDonXuat();
        private BUS_SanPham bussp = new BUS_SanPham();
        public GUI_ChiTietHDX()
        {
            InitializeComponent();
            txtMaHDX.Text = maHDX.ToString();
        }
        public int maHDX { get; set; }

        private void GUI_ChiTietHDX_Load(object sender, EventArgs e)
        {
            dgvCTHDX.DataSource = busCTHDX.getCTHDX();
            txtMaHDX.Text = maHDX.ToString();
            dgvCTHDX.Columns["maCTHDX"].HeaderText = "Mã chi tiết hóa đơn xuất";
            dgvCTHDX.Columns["maHDX"].HeaderText = "Mã hóa đơn xuất";
            dgvCTHDX.Columns["masp"].HeaderText = "Mã sản phẩm";
            dgvCTHDX.Columns["soluong"].HeaderText = "Số lượng";
            dgvCTHDX.Columns["gia"].HeaderText = "Giá";
            dgvCTHDX.Columns["tongtien"].HeaderText = "Tổng tiền";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            int maCTHDX = int.Parse(txtMaCTHDX.Text);
            int maHDX = int.Parse(txtMaHDX.Text);
            string masp = txtSanPham.Text;
            int sl = int.Parse(txtSoLuong.Text);
            
            // Kiểm tra sự tồn tại của mã hóa đơn nhập và mã sản phẩm trong cơ sở dữ liệu
            if (!busHDX.KiemTraTonTaiHoaDonXuat(maHDX))
            {
                MessageBox.Show("Mã hóa đơn xuất không tồn tại!");
                return;
            }

            decimal gia = bussp.LayGiaSanPham(masp);

            if (!bussp.KiemTraTonTaiSanPham(masp))
            {
                MessageBox.Show("Mã sản phẩm không tồn tại!");
                return;
            }

            // Tính tổng tiền
            decimal tongTien = sl * gia;

            // Tạo đối tượng DTO_ChiTietHoaDonNhap
            DTO_ChiTietHDX CTHDX = new DTO_ChiTietHDX(maCTHDX, maHDX, masp, sl, gia, tongTien);

           
            if (busCTHDX.kiemtramatrung(maCTHDX) == 1)
            {
                MessageBox.Show("Mã trùng");
            }
            else
            {
                int result = busCTHDX.themCTHDX(CTHDX);
                if (result == -2)
                {
                    MessageBox.Show("Số lượng không được nhỏ hơn 0");
                }
                else if (result == 1)
                {
                    MessageBox.Show("Thêm thành công!");
                    dgvCTHDX.DataSource = busCTHDX.getCTHDX();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int maCTHDX = int.Parse(txtMaCTHDX.Text);
            int maHDX = int.Parse(txtMaHDX.Text);
            string masp = txtSanPham.Text;
            int sl = int.Parse(txtSoLuong.Text);

            

            decimal gia = bussp.LayGiaSanPham(masp);

            bool isMaHDNValid = busHDX.KiemTraTonTaiHoaDonXuat(maHDX);
            bool isMaSPValid = bussp.KiemTraTonTaiSanPham(masp);

            if (!isMaHDNValid)
            {
                MessageBox.Show("Mã hóa đơn xuất không tồn tại trong cơ sở dữ liệu!");
                return;
            }

            if (!isMaSPValid)
            {
                MessageBox.Show("Mã sản phẩm không tồn tại trong cơ sở dữ liệu!");
                return;
            }


            // Tính tổng tiền
            decimal tongTien = sl * gia;

            // Tạo đối tượng DTO_ChiTietHoaDonNhap
            DTO_ChiTietHDX CTHDX = new DTO_ChiTietHDX(maCTHDX, maHDX, masp, sl, gia, tongTien);
       
            int result = busCTHDX.suaCTHDX(CTHDX);
            if (result == -2)
            {
                MessageBox.Show("Số lượng không được nhỏ hơn 0");
            }
            else if (result == 1)
            {
                MessageBox.Show("Sửa thành công!");
                dgvCTHDX.DataSource = busCTHDX.getCTHDX();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maCTHDX = int.Parse(txtMaCTHDX.Text);
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {


                if (busCTHDX.xoaCTHDX(maCTHDX) == true)
                {
                    MessageBox.Show("Xóa thành công!");
                    dgvCTHDX.DataSource = busCTHDX.getCTHDX();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCTHDX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int hang = e.RowIndex;
            txtMaCTHDX.Text = dgvCTHDX[0, hang].Value.ToString();
            txtMaHDX.Text = dgvCTHDX[1, hang].Value.ToString();
            txtSanPham.Text = dgvCTHDX[2, hang].Value.ToString();
            txtSoLuong.Text = dgvCTHDX[3, hang].Value.ToString();
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            string maHDX = txtMaHDX.Text;
            decimal tongTien = busCTHDX.TinhTongTien(maHDX);
            txtTongTien.Text = tongTien.ToString();
        }
    }
}
