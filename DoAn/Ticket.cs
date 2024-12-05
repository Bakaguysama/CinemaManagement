using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace DoAn
{
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
            LoadDanhSachPhim();
            CustomizeGrid();
            LoadDanhSachSnacks();
            LoadDanhSachNuocUong();
        }

        string connectionString = @"Server=MSI;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
        private void LoadDanhSachPhim()
        {
            string connectionString = @"Server=MSI;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = @"
        SELECT 
            p.MAPHIM, 
            p.TENPHIM, 
            p.THELOAI,
            p.QUOCGIA,
            s.GIOCHIEU, 
            r.TENRAP
        FROM 
            PHIM p
        JOIN 
            SUATCHIEU s ON p.MAPHIM = s.MAPHIM
        JOIN 
            RAPCHIEUPHIM r ON s.MARAP = r.MARAP
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

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có phim nào hiện tại.");
                    }
                    else
                    {
                        PhimDangChieu.DataSource = dt;
                        PhimDangChieu.RowHeadersVisible = false;
                        PhimDangChieu.AllowUserToAddRows = false;
                        PhimDangChieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
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

        //Them so sanh ngay gio 
        private void LoadDanhSachSuatChieu(string maPhim)
        {
            string connectionString = @"Server=MSI;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";
            string query = @"
        SELECT MASUAT, GIOCHIEU, MARAP
        FROM SUATCHIEU
        WHERE MAPHIM = @MaPhim
        ORDER BY GIOCHIEU;  -- Sắp xếp giờ chiếu theo thứ tự thời gian
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaPhim", maPhim);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Nếu có dữ liệu, gán dữ liệu cho ComboBox
                    if (dt.Rows.Count > 0)
                    {
                        cmbSuatchieu.DataSource = dt;
                        cmbSuatchieu.DisplayMember = "GIOCHIEU";  // Hiển thị giờ chiếu trong ComboBox
                        cmbSuatchieu.ValueMember = "MASUAT";  // Lưu giá trị của MASUAT (ID của suất chiếu)

                        // Tùy chọn để cho phép chọn một giá trị mặc định hoặc không (thêm dòng đầu tiên "Chọn suất chiếu")
                        DataRow row = dt.NewRow();
                        row["GIOCHIEU"] = "Chọn suất chiếu";  // Văn bản hiển thị đầu tiên trong ComboBox
                        dt.Rows.InsertAt(row, 0);  // Chèn vào đầu danh sách

                        cmbSuatchieu.SelectedIndex = 0;  // Chọn dòng đầu tiên là mặc định
                    }
                    else
                    {
                        MessageBox.Show("Không có suất chiếu nào cho phim này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách suất chiếu: " + ex.Message);
                }
            }
        }

        //Them so sanh ngay gio 
        private void PhimDangChieu_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = PhimDangChieu.Rows[e.RowIndex];

                lblMaPhim.Text = row.Cells["MAPHIM"].Value.ToString();
                lblTenPhim.Text = row.Cells["TENPHIM"].Value.ToString();
                lblTheLoai.Text = row.Cells["THELOAI"].Value.ToString();
                lblQuocGia.Text = row.Cells["QUOCGIA"].Value.ToString();

                LoadDanhSachSuatChieu(lblMaPhim.Text);

            }
        }

        private void LoadDanhSachGhe(string maSuatChieu, string maPhim)
        {
            // Kết nối đến cơ sở dữ liệu
            string connectionString = @"Server=MSI;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";

            string query = @"
        SELECT DISTINCT v.SOGHE
        FROM VEXEMPHIM v
        WHERE v.MASUAT = @MASUAT AND v.MAPHIM = @MAPHIM";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    da.SelectCommand.Parameters.AddWithValue("@MASUAT", maSuatChieu);
                    da.SelectCommand.Parameters.AddWithValue("@MAPHIM", maPhim);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbGhe.Items.Clear();

                    cmbGhe.Items.Add("Chọn Ghế");

                    foreach (DataRow row in dt.Rows)
                    {
                        cmbGhe.Items.Add(row["SOGHE"].ToString());
                    }

                    // Nếu có ghế, chọn mục đầu tiên ("Chọn Ghế")
                    if (cmbGhe.Items.Count > 0)
                    {
                        cmbGhe.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi nếu có sự cố
                    MessageBox.Show("Lỗi khi tải danh sách ghế: " + ex.Message);
                }
            }
        }

        private void LoadDanhSachSnacks()
        {
            string connectionString = @"Server=MSI;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";

            // Truy vấn SQL để lấy danh sách các món ăn (Thức ăn) từ bảng SANPHAM
            string query = @"
        SELECT MASP, TENSP, GIA
        FROM SANPHAM
        WHERE LOAI = N'Thức ăn';
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm dòng "Chọn thức ăn" vào đầu DataTable
                    DataRow row = dt.NewRow();
                    row["TENSP"] = "Chọn thức ăn";
                    row["MASP"] = DBNull.Value;  // Mã sản phẩm không có giá trị
                    dt.Rows.InsertAt(row, 0);

                    // Đặt dữ liệu vào ComboBox
                    cmbSnacks.DataSource = dt;
                    cmbSnacks.DisplayMember = "TENSP";  // Hiển thị tên sản phẩm
                    cmbSnacks.ValueMember = "MASP";    // Lấy mã sản phẩm

                    // Chọn mục đầu tiên ("Chọn thức ăn")
                    cmbSnacks.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách thức ăn: " + ex.Message);
                }
            }
        }

        private void LoadDanhSachNuocUong()
        {
            string connectionString = @"Server=MSI;Database=CINEMAMANAGEMENT;Trusted_Connection=True;";

            // Truy vấn SQL để lấy danh sách các món uống (Thức uống) từ bảng SANPHAM
            string query = @"
        SELECT MASP, TENSP, GIA
        FROM SANPHAM
        WHERE LOAI = N'Thức uống';
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm dòng "Chọn nước uống" vào đầu DataTable
                    DataRow row = dt.NewRow();
                    row["TENSP"] = "Chọn nước uống";
                    row["MASP"] = DBNull.Value;  // Mã sản phẩm không có giá trị
                    dt.Rows.InsertAt(row, 0);

                    // Đặt dữ liệu vào ComboBox
                    cmbNuocUong.DataSource = dt;
                    cmbNuocUong.DisplayMember = "TENSP";  // Hiển thị tên sản phẩm
                    cmbNuocUong.ValueMember = "MASP";    // Lấy mã sản phẩm

                    // Chọn mục đầu tiên ("Chọn nước uống")
                    cmbNuocUong.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách thức uống: " + ex.Message);
                }
            }
        }
        private void cmbSuatchieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSuatchieu.SelectedValue.ToString() != "Chọn suất chiếu")
            {
                LoadDanhSachGhe(cmbSuatchieu.SelectedValue.ToString(), lblMaPhim.Text);
            }
        }

        private void btnMua_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            string soDienThoai = txtSoDienThoai.Text;

            // Kiểm tra thông tin khách hàng
            if (string.IsNullOrWhiteSpace(tenKhachHang) || string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.");
                return;
            }

            if (cmbGioiTinh.SelectedIndex == -1)  // Nếu không có giá trị nào được chọn
            {
                MessageBox.Show("Vui lòng chọn giới tính.");
                return;
            }

            // Kiểm tra mã ghế đã được chọn chưa
            if (cmbGhe.SelectedIndex == 0)  // "Chọn Ghế" là mục đầu tiên trong ComboBox
            {
                MessageBox.Show("Vui lòng chọn ghế.");
                return;
            }

            string maGhe = cmbGhe.SelectedItem.ToString();
            if (string.IsNullOrEmpty(maGhe))
            {
                MessageBox.Show("Mã ghế không hợp lệ. Vui lòng chọn lại ghế.");
                return;
            }

            // Lấy giá vé từ bảng VEXEMPHIM
            decimal giaVe = GetGiaVe(maGhe);  // Hàm GetGiaVe sẽ truy vấn giá vé dựa trên mã ghế

            decimal tongTien = giaVe;

            // Kiểm tra khách hàng đã tồn tại trong cơ sở dữ liệu chưa
            string maKhachHang = GetMaKhachHang(tenKhachHang, soDienThoai);

            if (string.IsNullOrEmpty(maKhachHang)) // Nếu không tìm thấy khách hàng, tạo mới khách hàng
            {

                string gioiTinh = cmbGioiTinh.SelectedItem.ToString();

                TaoMoiKhachHang(tenKhachHang, soDienThoai, gioiTinh);
                maKhachHang = GetMaKhachHang(tenKhachHang, soDienThoai);

                if (string.IsNullOrEmpty(maKhachHang)) // Nếu vẫn không lấy được mã khách hàng
                {
                    MessageBox.Show("Không thể tạo khách hàng mới.");
                    return;
                }
            }

            string maSnack = cmbSnacks.SelectedValue.ToString();
            string maNuoc = cmbNuocUong.SelectedValue.ToString();
            if (cmbSnacks.SelectedIndex == -1 )
            {
                maSnack = "";
            }
            if (cmbNuocUong.SelectedIndex != -1)
            {
                maNuoc = "";
            }

            // Tính tổng tiền với món ăn (nếu có chọn)
            decimal giaSnack = 0;
            if (cmbSnacks.SelectedIndex != -1)
            {
                maSnack = cmbSnacks.SelectedValue.ToString();
                giaSnack = GetGia("SELECT GIA FROM SANPHAM WHERE MASP = @MaSP", maSnack);
                tongTien += giaSnack;  // Cộng giá của món ăn vào tổng tiền
            }

            // Tính tổng tiền với nước uống (nếu có chọn)
            decimal giaNuoc = 0;
            if (cmbNuocUong.SelectedIndex != -1)
            {
                maNuoc = cmbNuocUong.SelectedValue.ToString();
                giaNuoc = GetGia("SELECT GIA FROM SANPHAM WHERE MASP = @MaSP", maNuoc);
                tongTien += giaNuoc;  // Cộng giá của nước uống vào tổng tiền
            }

            // Hiển thị tổng tiền
            txtTongTien.Text = tongTien.ToString("C");

            // Kiểm tra tiền trả
            decimal tienTra = 0;
            if (!decimal.TryParse(txtTienTra.Text, out tienTra))
            {
                MessageBox.Show("Vui lòng nhập số tiền trả hợp lệ.");
                return;
            }

            if (tienTra < tongTien)
            {
                MessageBox.Show("Số tiền trả không đủ.");
                return;
            }

            decimal tienThua = tienTra - tongTien;
            txtTienThua.Text = tienThua.ToString("C");

            // Xuất hóa đơn
            string soHoaDon = TaoHoaDon(maKhachHang);  // Hàm tạo mã hóa đơn


            InsertCTHD(maKhachHang, soHoaDon, maSnack, giaSnack, maNuoc, giaNuoc, maGhe);  
            MessageBox.Show("Hóa đơn đã được tạo thành công!");
            txtTongTien.Text = string.Empty;
            txtTienTra.Text = string.Empty;
            txtTienThua.Text = string.Empty;
            txtTenKhachHang.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
        }




        // Hàm lấy giá ghế từ bảng VEXEMPHIM
        private decimal GetGiaVe(string maGhe)
        {
            string query = "SELECT GIAVE FROM VEXEMPHIM WHERE SOGHE = @MaGhe";
            decimal giaVe = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGhe", maGhe);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    giaVe = Convert.ToDecimal(result);
                }
            }
            return giaVe;
        }

        // Hàm lấy mã khách hàng từ cơ sở dữ liệu
        private string GetMaKhachHang(string tenKhachHang, string soDienThoai)
        {
            string query = "SELECT MAKH FROM KHACHHANG WHERE HOTEN = @TenKhachHang AND SDT = @SoDienThoai";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString(); // Trả về MAKH dưới dạng chuỗi
                }
                else
                {
                    return null; // Không tìm thấy khách hàng
                }
            }
        }


        // Hàm tạo mới khách hàng
        private string GenerateNewCustomerId()
        {
            // Tạo số ngẫu nhiên từ 000 đến 999
            Random rand = new Random();
            int randomNumber = rand.Next(1000);  // Tạo số từ 0 đến 999

            // Đảm bảo số có 3 chữ số
            string maKhachHang = "KH" + randomNumber.ToString("D3");  // Đảm bảo có 3 chữ số, ví dụ: KH001, KH099

            return maKhachHang;
        }

        private int TaoMoiKhachHang(string tenKhachHang, string soDienThoai, string gioiTinh)
        {
            // Kiểm tra nếu giới tính không hợp lệ thì gán giá trị mặc định "Nam"
            if (gioiTinh != "Nam" && gioiTinh != "Nữ")
            {
                gioiTinh = "Nam"; // Gán mặc định là "Nam" nếu giá trị không hợp lệ
            }

            // Lấy mã khách hàng mới theo định dạng KHxxx
            string maKhachHangMoi = GenerateNewCustomerId();

            // Kiểm tra nếu mã khách hàng đã tồn tại
            while (IsCustomerIdExist(maKhachHangMoi))
            {
                maKhachHangMoi = GenerateNewCustomerId(); // Tạo mã khách hàng mới nếu mã cũ đã tồn tại
            }

            string query = "INSERT INTO KHACHHANG (MAKH, HOTEN, SDT, NGAYDK, GIOITINH) " +
                           "VALUES (@MaKH, @TenKhachHang, @SoDienThoai, GETDATE(), @GioiTinh) " +
                           "SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKH", maKhachHangMoi);
                cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                conn.Open();
                object result = cmd.ExecuteScalar();

                // Kiểm tra nếu result là DBNull thì trả về -1 hoặc một giá trị mặc định khác
                if (result == DBNull.Value)
                {
                    // Xử lý lỗi nếu không có giá trị trả về
                    return -1;  // Hoặc một giá trị mặc định để báo lỗi
                }

                return Convert.ToInt32(result); // Trả về mã khách hàng mới
            }
        }

        // Kiểm tra mã khách hàng có tồn tại trong cơ sở dữ liệu hay không
        private bool IsCustomerIdExist(string maKhachHang)
        {
            string query = "SELECT COUNT(*) FROM KHACHHANG WHERE MAKH = @MaKH";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKH", maKhachHang);
                conn.Open();

                int count = (int)cmd.ExecuteScalar();
                return count > 0;  // Nếu có bản ghi, trả về true (mã khách hàng đã tồn tại)
            }
        }


        // Hàm tạo số hóa đơn
        private string TaoHoaDon(string maKhachHang)
        {
            // Tạo mã hóa đơn ngẫu nhiên
            string soHoaDonMoi = "HD" + GenerateRandomCode(3);  // SOHD-XXXX, với XXXX là chuỗi ngẫu nhiên

            // Chèn hóa đơn mới vào bảng
            string queryInsertHoaDon = "INSERT INTO HOADON (SOHD, MAKH, NGAYHD) VALUES (@SoHD, @MaKhachHang, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmdInsert = new SqlCommand(queryInsertHoaDon, conn);
                cmdInsert.Parameters.AddWithValue("@SoHD", soHoaDonMoi);
                cmdInsert.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                conn.Open();
                cmdInsert.ExecuteNonQuery();
            }

            return soHoaDonMoi;  // Trả về mã hóa đơn mới
        }

        private string GenerateRandomCode(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  // Chữ cái và số
            StringBuilder randomCode = new StringBuilder();
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                randomCode.Append(validChars[rand.Next(validChars.Length)]);
            }

            return randomCode.ToString();
        }



        // Hàm thêm chi tiết hóa đơn
        private void InsertCTHD(string maKhachHang, string soHoaDon, string maSnack, decimal giaSnack, string maNuoc, decimal giaNuoc, string maGhe)
        {
            string query = "INSERT INTO CTHD_SP (SOHD, MASP, SOSP) VALUES (@SoHoaDon, @MaSnack, 1)";
            if (maSnack != "")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                    cmd.Parameters.AddWithValue("@MaSnack", maSnack);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            if (maNuoc != "")
            {
                query = "INSERT INTO CTHD_SP (SOHD, MASP, SOSP) VALUES (@SoHoaDon, @MaNuoc, 1)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                    cmd.Parameters.AddWithValue("@MaNuoc", maNuoc);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Thêm chi tiết ghế vào bảng CTHD_VXP
            string maVe = GetMaVeFromGhe(maGhe);
            query = "INSERT INTO CTHD_VXP (SOHD, MAVE, SOVE) VALUES (@SoHoaDon, @maVe, 1)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                cmd.Parameters.AddWithValue("@maVe", maVe);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string GetMaVeFromGhe(string maGhe)
        {
            string query = "SELECT MAVE FROM VEXEMPHIM WHERE SOGHE = @MaGhe";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGhe", maGhe);
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString();  // Trả về mã vé
                }
                else
                {
                    return null;  // Trường hợp không tìm thấy mã vé
                }
            }
        }
        private decimal GetGia(string query, string maSP)
        {
            decimal gia = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    gia = Convert.ToDecimal(result);
                }
            }
            return gia;
        }


    }
}
