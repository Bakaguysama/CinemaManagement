using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace DoAn
{
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
            LoadDanhSachPhim();
            CustomizeGrid();
        }

        private void LoadDanhSachPhim()
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = @"
                SELECT 
                    p.MAPHIM, 
                    p.TENPHIM, 
                    s.GIOCHIEU, 
                    r.TENRAP
                FROM 
                    PHIM p
                JOIN 
                    SUATCHIEU s ON p.MAPHIM = s.MAPHIM
                JOIN 
                    RAPCHIEUPHIM r ON s.MARAP = r.MARAP
                WHERE 
                    s.GIOCHIEU >= GETDATE()
                ORDER BY 
                    s.GIOCHIEU;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    PhimDangChieu.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
                }
            }
        }

        private void CustomizeGrid()
        {
            PhimDangChieu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 49, 61);
            PhimDangChieu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            PhimDangChieu.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            PhimDangChieu.EnableHeadersVisualStyles = false;
        }

        private void PhimDangChieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = PhimDangChieu.Rows[e.RowIndex];

                lblMaPhim.Text = row.Cells["MAPHIM"].Value.ToString();
                lblTenPhim.Text = row.Cells["TENPHIM"].Value.ToString();
                LoadDanhSachSuatChieu(lblMaPhim.Text);
            }
        }

        private void LoadDanhSachSuatChieu(string maPhim)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";
            string query = @"
                SELECT MASUAT, GIOCHIEU, MARAP
                FROM SUATCHIEU
                WHERE MAPHIM = @MaPhim;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhim", maPhim);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbSuatchieu.DataSource = dt;
                cmbSuatchieu.DisplayMember = "GIOCHIEU";
                cmbSuatchieu.ValueMember = "MASUAT";
            }
        }

        private void LoadDanhSachGhe(string maSuatChieu)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";
            string query = @"
        SELECT MAGHE, TENRAP
        FROM GHE
        WHERE MASUATCHIEU = @MASUATCHIEU";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@MASUATCHIEU", maSuatChieu);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Clear the ComboBox before adding new items
                    cmbGhe.Items.Clear();

                    // Add new seats to the ComboBox
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbGhe.Items.Add(row["MAGHE"].ToString());
                    }

                    if (cmbGhe.Items.Count > 0)
                    {
                        cmbGhe.SelectedIndex = 0; // Default select the first item if available
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách ghế: " + ex.Message);
                }
            }
        }


        private void btnDatVe_Click(object sender, EventArgs e)
        {
            string maPhim = lblMaPhim.Text;
            string maSuat = cmbSuatchieu.SelectedItem.ToString(); // Bạn có thể lấy mã suất chiếu từ label hoặc ComboBox

            string gheChon = cmbGhe.SelectedItem.ToString(); // Lấy mã ghế người dùng chọn

            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";
            string query = @"
        INSERT INTO VE (MAPHIM, MASUATCHIEU, MAGHE)
        VALUES (@MAPHIM, @MASUATCHIEU, @MAGHE)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MAPHIM", maPhim);
                    cmd.Parameters.AddWithValue("@MASUATCHIEU", maSuat);
                    cmd.Parameters.AddWithValue("@MAGHE", gheChon);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đặt vé thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đặt vé: " + ex.Message);
                }
            }
        }


        private void LoadDanhSachSnacks()
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";
            string query = @"
                SELECT MASP, TENSP, GIA
                FROM SNACKS;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbSnacks.DataSource = dt;
                cmbSnacks.DisplayMember = "TENSP";
                cmbSnacks.ValueMember = "MASP";
            }
        }

        private void LoadDanhSachNuocUong()
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";
            string query = @"
                SELECT MANUOC, TENNUOC, GIA
                FROM NUOCUONG;
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbNuocUong.DataSource = dt;
                cmbNuocUong.DisplayMember = "TENNUOC";
                cmbNuocUong.ValueMember = "MANUOC";
            }
        }

        private void Select(object sender, EventArgs e)
        {
            string maPhim = lblMaPhim.Text;
            string maSuatchieu = cmbSuatchieu.SelectedValue.ToString();

            LoadDanhSachGhe(maSuatchieu);
            LoadDanhSachSnacks();
            LoadDanhSachNuocUong();
        }

        private void btnMua_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            string soDienThoai = txtSoDienThoai.Text;

            if (string.IsNullOrWhiteSpace(tenKhachHang) || string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.");
                return;
            }

            decimal tongTien = 45000; 
            int maKhachHang = GetMaKhachHang(tenKhachHang);
            if (maKhachHang == -1)
            {
                MessageBox.Show("Không tìm thấy khách hàng.");
                return;
            }

            decimal giaSnack = 0;
            string maSnack = "";
            if (cmbSnacks.SelectedValue != null)
            {
                maSnack = cmbSnacks.SelectedValue.ToString();
                giaSnack = GetGia("SELECT GIA FROM SANPHAM WHERE MASP = @MaSP", maSnack);
                tongTien += giaSnack;
            }

            decimal giaNuoc = 0;
            string maNuoc = "";
            if (cmbNuocUong.SelectedValue != null)
            {
                maNuoc = cmbNuocUong.SelectedValue.ToString();
                giaNuoc = GetGia("SELECT GIA FROM SANPHAM WHERE MASP = @MaSP", maNuoc);
                tongTien += giaNuoc;
            }

            txtTongTien.Text = tongTien.ToString("C");

            decimal tienTra = 0;
            if (!decimal.TryParse(txtTienTra.Text, out tienTra))
            {
                MessageBox.Show("Vui lòng nhập số tiền trả hợp lệ.");
                return;
            }

            if (tienTra < tongTien)
            {
                MessageBox.Show("Số tiền trả không đủ.");
            }
            else
            {
                decimal tienThua = tienTra - tongTien;
                txtTienThua.Text = tienThua.ToString("C");

                string maRap = "NHOM12";
                string maPhim = lblMaPhim.Text;

                string danhSachGhe = cmbGhe.SelectedItem.ToString();

                InsertHoaDon(maKhachHang, maRap, maPhim, danhSachGhe, tongTien, tienTra, tienThua);

                string hoaDon = $"Hóa Đơn Mua Vé\n" +
                 $"Khách Hàng: {tenKhachHang}\n" +
                 $"Số Điện Thoại: {soDienThoai}\n" +
                 $"Tên Phim: {lblTenPhim.Text}\n" +
                 $"Suất Chiếu: {cmbSuatchieu.Text}\n" +
                 $"Snacks: {cmbSnacks.Text}\n" +
                 $"Nước Uống: {cmbNuocUong.Text}\n" +
                 $"Tổng Tiền: {tongTien:C}\n" +
                 $"Tiền Trả: {tienTra:C}\n" +
                 $"Tiền Thừa: {tienThua:C}\n" +
                 $"Cảm ơn quý khách đã mua vé!";

                MessageBox.Show(hoaDon, "Hóa Đơn Thành Công");
            }
        }

        private void InsertHoaDon(int maKhachHang, string maRap, string maPhim, string danhSachGhe, decimal tongTien, decimal tienTra, decimal tienThua)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";

            string query = @"
        INSERT INTO HOADON (MAKHACHHANG, MARAP, MAPHIM, DATSANPHAM, TONGTIEN, TIENTRA, TIENTHUA)
        VALUES (@MaKhachHang, @MaRap, @MaPhim, @DanhSachGhe, @TongTien, @TienTra, @TienThua);
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                cmd.Parameters.AddWithValue("@MaRap", maRap);
                cmd.Parameters.AddWithValue("@MaPhim", maPhim);
                cmd.Parameters.AddWithValue("@DanhSachGhe", danhSachGhe);
                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                cmd.Parameters.AddWithValue("@TienTra", tienTra);
                cmd.Parameters.AddWithValue("@TienThua", tienThua);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private int GetMaKhachHang(string tenKhachHang)
        {
            int maKhachHang = -1;

            using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;"))
            {
                string query = "SELECT MAKHACHHANG FROM KHACHHANG WHERE TENKHACHHANG = @TenKhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    maKhachHang = Convert.ToInt32(result);
                }
            }

            return maKhachHang;
        }

        private decimal GetGia(string query, string maSanPham)
        {
            decimal gia = 0;
            using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSanPham);
                conn.Open();
                gia = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            return gia;
        }

    }
}
