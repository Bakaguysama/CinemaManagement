using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.IO;
using Guna.UI2.WinForms;

namespace DoAn
{
    public partial class Ticket : Form
    {
        private string soHoaDon = string.Empty;
        private decimal tongTien = 0;
        private decimal tempMoney = 0;
        public Ticket()
        {
            InitializeComponent();
            LoadDanhSachPhim();
            CustomizeGrid();
            LoadDanhSachSnacks();
            LoadDanhSachNuocUong();
        }

        string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
        private void LoadDanhSachPhim()
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
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
        --WHERE s.GIOCHIEU < GETDATE()
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
                        MessageBox.Show("Không có phim nào hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
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
                        

                        cmbSuatchieu.SelectedIndex = 0;  // Chọn dòng đầu tiên là mặc định
                    }
                    else
                    {
                        MessageBox.Show("Không có suất chiếu nào cho phim này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách suất chiếu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Them so sanh ngay gio 
        private void PhimDangChieu_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = PhimDangChieu.Rows[e.RowIndex];
                LoadDanhSachSuatChieu(row.Cells["MAPHIM"].Value.ToString());
            }
        }

        private void LoadDanhSachGhe(string maSuatChieu, string maPhim)
        {
            // Kết nối đến cơ sở dữ liệu
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

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
                    MessageBox.Show("Lỗi khi tải danh sách ghế: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadDanhSachSnacks()
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Truy vấn SQL để lấy danh sách các món ăn (Thức ăn) từ bảng SANPHAM
            string query = @"
        SELECT MASP, TENSP, GIA
        FROM SANPHAM
        WHERE LOAI = N'Thức ăn' AND SOLUONG > 0;
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
                    MessageBox.Show("Lỗi khi tải danh sách thức ăn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadDanhSachNuocUong()
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Truy vấn SQL để lấy danh sách các món uống (Thức uống) từ bảng SANPHAM
            string query = @"
        SELECT MASP, TENSP, GIA
        FROM SANPHAM
        WHERE LOAI = N'Thức uống' AND SOLUONG > 0;
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
                    MessageBox.Show("Lỗi khi tải danh sách thức uống: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void cmbSuatchieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu SelectedValue không null
            if (cmbSuatchieu.SelectedValue != null && cmbSuatchieu.SelectedValue.ToString() != "Chọn suất chiếu")
            {
                // Kiểm tra thêm giá trị CurrentRow của PhimDangChieu
                if (PhimDangChieu.CurrentRow != null && PhimDangChieu.CurrentRow.Cells[0].Value != null)
                {
                    LoadDanhSachGhe(cmbSuatchieu.SelectedValue.ToString(), PhimDangChieu.CurrentRow.Cells[0].Value.ToString());
                }
            }
        }


        private void btnMua_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            string soDienThoai = txtSoDienThoai.Text;
            if (PhimDangChieu.CurrentRow != null) // Kiểm tra xem có hàng nào được chọn không
            {
                string tenPhim = PhimDangChieu.CurrentRow.Cells[1].Value.ToString(); // Cột thứ 2 (index là 1)
            }
            else
            {
                MessageBox.Show("N/A", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Kiểm tra thông tin khách hàng
            if (string.IsNullOrWhiteSpace(tenKhachHang) || string.IsNullOrWhiteSpace(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbGioiTinh.SelectedIndex == -1)  // Nếu không có giá trị nào được chọn
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra mã ghế đã được chọn chưa
            if (cmbGhe.SelectedIndex == 0)  // "Chọn Ghế" là mục đầu tiên trong ComboBox
            {
                MessageBox.Show("Vui lòng chọn ghế.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maGhe = cmbGhe.SelectedItem.ToString();
            if (string.IsNullOrEmpty(maGhe))
            {
                MessageBox.Show("Mã ghế không hợp lệ. Vui lòng chọn lại ghế.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy giá vé từ bảng VEXEMPHIM
            decimal giaVe = GetGiaVe(maGhe);  // Hàm GetGiaVe sẽ truy vấn giá vé dựa trên mã ghế

            tongTien = giaVe;

            // Kiểm tra khách hàng đã tồn tại trong cơ sở dữ liệu chưa
            string maKhachHang = GetMaKhachHang(tenKhachHang, soDienThoai);

            if (string.IsNullOrEmpty(maKhachHang)) // Nếu không tìm thấy khách hàng, tạo mới khách hàng
            {

                string gioiTinh = cmbGioiTinh.SelectedItem.ToString();

                TaoMoiKhachHang(tenKhachHang, soDienThoai, gioiTinh);
                maKhachHang = GetMaKhachHang(tenKhachHang, soDienThoai);

                if (string.IsNullOrEmpty(maKhachHang)) // Nếu vẫn không lấy được mã khách hàng
                {
                    MessageBox.Show("Không thể tạo khách hàng mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            soHoaDon = TaoHoaDon(maKhachHang);  // Hàm tạo mã hóa đơn

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells[1].Value == null || row.Cells[2].Value == null)
                    continue; // Bỏ qua các hàng không hợp lệ

                string tenSP = row.Cells[1].Value.ToString(); // Lấy giá trị từ cột thứ 2
                int soLuong;

                // Kiểm tra và chuyển đổi số lượng từ cột thứ 3
                if (!int.TryParse(row.Cells[2].Value.ToString(), out soLuong))
                {
                    MessageBox.Show("Số lượng không hợp lệ trong bảng sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string maSP = getMaSP(tenSP); // Lấy mã sản phẩm dựa trên tên sản phẩm
                if (string.IsNullOrEmpty(maSP))
                {
                    MessageBox.Show($"Không tìm thấy mã sản phẩm cho {tenSP}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                tongTien += decimal.Parse(row.Cells[3].Value.ToString());
                InsertCTHD_SP(soHoaDon, maSP, soLuong);
            }
            tempMoney = tongTien;

            InsertCTHD_VXP(maKhachHang, soHoaDon, maGhe);
            decimal tienTra = 0;
            if (!decimal.TryParse(txtTienTra.Text, out tienTra))
            {
                MessageBox.Show("Vui lòng nhập số tiền trả hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tienTra < tongTien)
            {
                MessageBox.Show("Số tiền trả không đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            decimal tienThua = tienTra - tongTien;
            txtTienThua.Text = Convert.ToInt32(tienThua).ToString("N0") + "đ";


            try
            {
                // Đường dẫn thư mục 'HoaDon' trong project
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HoaDon");

                // Kiểm tra và tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Đường dẫn file hóa đơn
                string fileName = $"HoaDon_{soHoaDon}.txt";
                string filePath = Path.Combine(folderPath, fileName);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("--------------- HÓA ĐƠN ---------------");
                    writer.WriteLine($"Ngày tạo hóa đơn: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    writer.WriteLine($"Số hóa đơn: {soHoaDon}");
                    writer.WriteLine($"Khách hàng: {tenKhachHang}");
                    writer.WriteLine($"Số điện thoại: {soDienThoai}");

                    if (PhimDangChieu.CurrentRow != null)
                    {
                        string tenPhim = PhimDangChieu.CurrentRow.Cells[1].Value.ToString();
                        writer.WriteLine($"Phim: {tenPhim}");
                    }
                    writer.WriteLine($"Rạp: {PhimDangChieu.CurrentRow.Cells[5].Value.ToString()}");
                    writer.WriteLine($"Ghế: {maGhe}");
                    writer.WriteLine($"Suất chiếu: {cmbSuatchieu.Text}");
                    writer.WriteLine($"Giá vé: {Convert.ToInt32(giaVe).ToString("N0")}đ");
                    if (guna2DataGridView1.Rows.Count > 0)
                    {
                        writer.WriteLine("Chi tiết sản phẩm:");

                        int stt = 1; // Khởi tạo số thứ tự bắt đầu từ 1

                        foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                        {
                            // Kiểm tra nếu các cột 2, 3, 4 không null và không trống
                            if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()) &&
                                row.Cells[2].Value != null && !string.IsNullOrEmpty(row.Cells[2].Value.ToString()) &&
                                row.Cells[3].Value != null && !string.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                            {
                                string tenSP = row.Cells[1].Value.ToString(); // Lấy tên sản phẩm từ cột 2
                                int soLuong = 0;
                                decimal giaSP = 0;

                                // Kiểm tra và chuyển đổi số lượng từ cột thứ 3
                                if (!int.TryParse(row.Cells[2].Value.ToString(), out soLuong))
                                {
                                    MessageBox.Show($"Số lượng không hợp lệ cho sản phẩm {tenSP}.");
                                    continue; // Tiếp tục với dòng tiếp theo nếu số lượng không hợp lệ
                                }

                                // Kiểm tra và chuyển đổi giá sản phẩm từ cột thứ 4
                                if (!decimal.TryParse(row.Cells[3].Value.ToString(), out giaSP))
                                {
                                    MessageBox.Show($"Giá sản phẩm không hợp lệ cho {tenSP}.");
                                    continue; // Tiếp tục với dòng tiếp theo nếu giá không hợp lệ
                                }

                                // In số thứ tự sản phẩm vào hóa đơn
                                writer.WriteLine($"{stt}. {tenSP} x {soLuong}: {Convert.ToInt32(row.Cells[3].Value).ToString("N0")}đ");

                                // Tăng số thứ tự lên cho sản phẩm tiếp theo
                                stt++;
                            }
                        }
                    }
                    writer.WriteLine($"TỔNG TIỀN CẦN THANH TOÁN: {Convert.ToInt32(tongTien).ToString("N0")}đ");
                    writer.WriteLine("-----------------------------------------");
                    MessageBox.Show("Hóa đơn đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PhimDangChieu.ClearSelection();
                    guna2DataGridView1.Rows.Clear();
                    cmbGhe.SelectedIndex = 0;
                    cmbSuatchieu.SelectedIndex = 0;
                    cmbGioiTinh.SelectedIndex = -1;
                    txtTenKhachHang.Text = string.Empty;
                    txtSoDienThoai.Text = string.Empty;
                    soHoaDon = string.Empty;
                    tongTien = 0;
                    tempMoney = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi xuất hóa đơn: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        private void InsertCTHD_VXP(string maKhachHang, string soHoaDon, string maGhe)
        {
            string maVe = GetMaVeFromGhe(maGhe);
            string query = "INSERT INTO CTHD_VXP (SOHD, MAVE, SOVE) VALUES (@SoHoaDon, @maVe, 1)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                cmd.Parameters.AddWithValue("@maVe", maVe);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertCTHD_SP(string soHoaDon, string maSP, int SoLuong)
        {
            string query = "INSERT INTO CTHD_SP (SOHD, MASP, SOSP) VALUES (@SoHoaDon, @MaSP, @SoLuong)";
            if (maSP != "")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.Parameters.AddWithValue("@SoLuong", SoLuong);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có combobox nào được chọn
            if (cmbSnacks.SelectedIndex < 0 && cmbNuocUong.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng có bị trống hoặc không hợp lệ
            if (cmbSnacks.SelectedIndex >= 1)
            {
                if (string.IsNullOrWhiteSpace(textBox_SoLuongSnacks.Text) || !int.TryParse(textBox_SoLuongSnacks.Text.Trim(), out int soLuongSnacks))
                {
                    MessageBox.Show("Vui lòng nhập đúng số lượng cho sản phẩm Snacks!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (cmbNuocUong.SelectedIndex >= 1)
            {
                if (string.IsNullOrWhiteSpace(textBox_SoLuongNuoc.Text) || !int.TryParse(textBox_SoLuongNuoc.Text.Trim(), out int soLuongNuoc))
                {
                    MessageBox.Show("Vui lòng nhập đúng số lượng cho sản phẩm Nước Uống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Tự động tăng STT
            int STT = guna2DataGridView1.Rows.Count;

            // Thêm sản phẩm Snacks nếu được chọn
            if (cmbSnacks.SelectedIndex >= 1)
            {
                string tenSP = cmbSnacks.SelectedValue.ToString();
                int soLuongConLai = getSoLuong(tenSP);

                // Kiểm tra tổng số lượng
                int soLuongDangChon = int.Parse(textBox_SoLuongSnacks.Text.Trim());
                int soLuongDaChonTrongGrid = 0;

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == getTenSP(tenSP))
                    {
                        soLuongDaChonTrongGrid = int.Parse(row.Cells[2].Value.ToString());
                        break;
                    }
                }

                if (soLuongDangChon + soLuongDaChonTrongGrid > soLuongConLai)
                {
                    MessageBox.Show($"Tổng số lượng sản phẩm {getTenSP(tenSP)} vượt quá số lượng còn lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal giaTien = GetGia("SELECT GIA FROM SANPHAM WHERE MASP = @MaSP", tenSP);
                if (giaTien > 0)
                {
                    bool daTonTai = false;

                    // Kiểm tra sản phẩm đã tồn tại trong DataGridView
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == getTenSP(tenSP))
                        {
                            // Cộng dồn số lượng và giá tiền
                            int soLuongMoi = soLuongDaChonTrongGrid + soLuongDangChon;
                            row.Cells[2].Value = soLuongMoi;
                            row.Cells[3].Value = soLuongMoi * Convert.ToInt32(giaTien);
                            daTonTai = true;
                            break;
                        }
                    }

                    // Nếu chưa tồn tại, thêm mới
                    if (!daTonTai)
                    {
                        guna2DataGridView1.Rows.Add(STT, getTenSP(tenSP), soLuongDangChon, soLuongDangChon * Convert.ToInt32(giaTien));
                        STT++; // Tăng STT
                    }

                    // Cập nhật số lượng còn lại
                    lbl_SLSnackConLai.Text = "Số lượng còn lại: " + (soLuongConLai - soLuongDangChon - soLuongDaChonTrongGrid).ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giá cho sản phẩm Snacks!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                cmbSnacks.SelectedIndex = -1;
                textBox_SoLuongSnacks.Clear();
            }

            // Thêm sản phẩm Nước Uống nếu được chọn
            if (cmbNuocUong.SelectedIndex >= 1)
            {
                string tenSP = cmbNuocUong.SelectedValue.ToString();
                int soLuongConLai = getSoLuong(tenSP);

                // Kiểm tra tổng số lượng
                int soLuongDangChon = int.Parse(textBox_SoLuongNuoc.Text.Trim());
                int soLuongDaChonTrongGrid = 0;

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == getTenSP(tenSP))
                    {
                        soLuongDaChonTrongGrid = int.Parse(row.Cells[2].Value.ToString());
                        break;
                    }
                }

                if (soLuongDangChon + soLuongDaChonTrongGrid > soLuongConLai)
                {
                    MessageBox.Show($"Tổng số lượng sản phẩm {getTenSP(tenSP)} vượt quá số lượng còn lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal giaTien = GetGia("SELECT GIA FROM SANPHAM WHERE MASP = @MaSP", tenSP);
                if (giaTien > 0)
                {
                    bool daTonTai = false;

                    // Kiểm tra sản phẩm đã tồn tại trong DataGridView
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == getTenSP(tenSP))
                        {
                            // Cộng dồn số lượng và giá tiền
                            int soLuongMoi = soLuongDaChonTrongGrid + soLuongDangChon;
                            row.Cells[2].Value = soLuongMoi;
                            row.Cells[3].Value = soLuongMoi * Convert.ToInt32(giaTien);
                            daTonTai = true;
                            break;
                        }
                    }

                    // Nếu chưa tồn tại, thêm mới
                    if (!daTonTai)
                    {
                        guna2DataGridView1.Rows.Add(STT, getTenSP(tenSP), soLuongDangChon, soLuongDangChon * Convert.ToInt32(giaTien));
                        STT++; // Tăng STT
                    }

                    // Cập nhật số lượng còn lại
                    lbl_SLNuocConLai.Text = "Số lượng còn lại: " + (soLuongConLai - soLuongDangChon - soLuongDaChonTrongGrid).ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giá cho sản phẩm Nước Uống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                cmbNuocUong.SelectedIndex = -1;
                textBox_SoLuongNuoc.Clear();
            }

            // Xóa chọn và làm mới input sau khi thêm xong
            cmbSnacks.SelectedIndex = -1;
            textBox_SoLuongSnacks.Clear();
            cmbNuocUong.SelectedIndex = -1;
            textBox_SoLuongNuoc.Clear();
        }



        private string getTenSP(string MaSP)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Truy vấn SQL để lấy tên sản phẩm từ bảng SANPHAM
            string query = @"
                                SELECT TENSP
                                FROM SANPHAM
                                WHERE MASP = @MASP;
                            ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MASP", MaSP); // Thêm tham số cho truy vấn SQL

                try
                {
                    conn.Open(); // Mở kết nối đến cơ sở dữ liệu
                    object result = cmd.ExecuteScalar(); // Thực thi truy vấn và lấy giá trị đầu tiên từ cột TENSP

                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString(); // Trả về giá trị TENSP nếu không null
                    }
                    else
                    {
                        return null; // Nếu không tìm thấy sản phẩm, trả về null
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi (tùy chọn) hoặc ném ngoại lệ
                    throw new Exception("Đã xảy ra lỗi khi truy vấn tên sản phẩm.", ex);
                }
            }
        }

        private string getMaSP(string TENSP)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Truy vấn SQL để lấy tên sản phẩm từ bảng SANPHAM
            string query = @"
                                SELECT MASP
                                FROM SANPHAM
                                WHERE TENSP = @TENSP;
                            ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TENSP", TENSP); // Thêm tham số cho truy vấn SQL

                try
                {
                    conn.Open(); // Mở kết nối đến cơ sở dữ liệu
                    object result = cmd.ExecuteScalar(); // Thực thi truy vấn và lấy giá trị đầu tiên từ cột TENSP

                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString(); // Trả về giá trị TENSP nếu không null
                    }
                    else
                    {
                        return null; // Nếu không tìm thấy sản phẩm, trả về null
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi (tùy chọn) hoặc ném ngoại lệ
                    throw new Exception("Đã xảy ra lỗi khi truy vấn tên sản phẩm.", ex);
                }
            }
        }

        private int getSoLuong(string MaSP)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Truy vấn SQL để lấy số lượng sản phẩm từ bảng SANPHAM
            string query = @"
                            SELECT SOLUONG
                            FROM SANPHAM
                            WHERE MASP = @MASP;
                        ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MASP", MaSP); // Thêm tham số cho truy vấn SQL

                try
                {
                    conn.Open(); // Mở kết nối đến cơ sở dữ liệu
                    object result = cmd.ExecuteScalar(); // Thực thi truy vấn và lấy giá trị đầu tiên từ cột SOLUONG

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result); // Chuyển đổi kết quả sang kiểu int
                    }
                    else
                    {
                        return 0; // Nếu không tìm thấy sản phẩm, trả về 0
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi (tùy chọn) hoặc ném ngoại lệ
                    throw new Exception("Đã xảy ra lỗi khi truy vấn số lượng sản phẩm.", ex);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PhimDangChieu.ClearSelection();
            guna2DataGridView1.Rows.Clear();
            cmbGhe.SelectedIndex = 0;
            cmbSuatchieu.SelectedIndexChanged -= cmbSuatchieu_SelectedIndexChanged; // Tạm thời hủy liên kết sự kiện
            cmbSuatchieu.SelectedIndex = 0;
            cmbSuatchieu.SelectedIndexChanged += cmbSuatchieu_SelectedIndexChanged;
            cmbGioiTinh.SelectedIndex = -1;
            txtTongTien.Text = string.Empty;
            txtTienTra.Text = string.Empty;
            txtTienThua.Text = string.Empty;
            txtTenKhachHang.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
            soHoaDon = string.Empty;
            cmbNuocUong.SelectedIndex = -1;
            cmbSnacks.SelectedIndex = -1;
            lbl_SLSnackConLai.Text = "";
            lbl_SLNuocConLai.Text = "";
            tongTien = 0;
            tempMoney = 0;
        }

        private void guna2Button_TamTinh_Click(object sender, EventArgs e)
        {
            if (cmbGhe.SelectedIndex == 0)  // "Chọn Ghế" là mục đầu tiên trong ComboBox
            {
                MessageBox.Show("Vui lòng chọn ghế.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maGhe = cmbGhe.SelectedItem.ToString();
            if (string.IsNullOrEmpty(maGhe))
            {
                MessageBox.Show("Mã ghế không hợp lệ. Vui lòng chọn lại ghế.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy giá vé từ bảng VEXEMPHIM
            decimal giaVe = GetGiaVe(maGhe);  // Hàm GetGiaVe sẽ truy vấn giá vé dựa trên mã ghế
            tongTien = giaVe;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells[1].Value == null || row.Cells[2].Value == null)
                    continue; // Bỏ qua các hàng không hợp lệ

                string tenSP = row.Cells[1].Value.ToString(); // Lấy giá trị từ cột thứ 2
                int soLuong;

                // Kiểm tra và chuyển đổi số lượng từ cột thứ 3
                if (!int.TryParse(row.Cells[2].Value.ToString(), out soLuong))
                {
                    MessageBox.Show("Số lượng không hợp lệ trong bảng sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string maSP = getMaSP(tenSP); // Lấy mã sản phẩm dựa trên tên sản phẩm
                if (string.IsNullOrEmpty(maSP))
                {
                    MessageBox.Show($"Không tìm thấy mã sản phẩm cho {tenSP}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (row.Cells[3].Value == null || string.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                    continue;

                decimal thanhTien = 0;

                // Kiểm tra và chuyển đổi giá trị trong cột thứ 3
                if (!decimal.TryParse(row.Cells[3].Value.ToString(), out thanhTien))
                {
                    MessageBox.Show($"Giá trị không hợp lệ tại dòng {row.Index + 1}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue; // Bỏ qua dòng nếu không thể chuyển đổi
                }
                tongTien += thanhTien;
            }
            tempMoney = tongTien;
            txtTongTien.Text = Convert.ToInt32(tongTien).ToString("N0") + "đ";
        }

        private void cmbGhe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSnacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSnacks.SelectedIndex >= 1)
            {
                string tenSP = cmbSnacks.SelectedValue.ToString();
                lbl_SLSnackConLai.Text = "Số lượng còn lại: " + getSoLuong(tenSP).ToString();
            }
            else
                lbl_SLSnackConLai.Text = "";
        }

        private void cmbNuocUong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNuocUong.SelectedIndex >= 1)
            {
                string tenSP = cmbNuocUong.SelectedValue.ToString();
                lbl_SLNuocConLai.Text = "Số lượng còn lại: " + getSoLuong(tenSP).ToString();
            }
            else
                lbl_SLNuocConLai.Text = "";
        }

        private void txtBox_TimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtBox_TimKiem.Text.Trim().ToLower(); // Lấy dữ liệu tìm kiếm

            // Kiểm tra DataSource của DataGridView
            if (PhimDangChieu.DataSource is DataTable dataTable)
            {
                // Nếu ô tìm kiếm trống, bỏ bộ lọc và hiển thị tất cả dữ liệu
                if (string.IsNullOrEmpty(searchText))
                {
                    dataTable.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    // Áp dụng bộ lọc với từ khóa tìm kiếm
                    dataTable.DefaultView.RowFilter = $"[TenPhim] LIKE '%{searchText}%'";
                }
            }
            else
            {
                MessageBox.Show("DataSource không phải là DataTable!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2Button_Xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có dòng nào được chọn
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dòng được chọn
            foreach (DataGridViewRow selectedRow in guna2DataGridView1.SelectedRows)
            {
                // Kiểm tra nếu dòng không phải là dòng mới
                if (!selectedRow.IsNewRow)
                {
                    guna2DataGridView1.Rows.Remove(selectedRow);
                }
            }

            // Cập nhật lại STT cho các dòng
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                guna2DataGridView1.Rows[i].Cells[0].Value = i; // STT bắt đầu từ 1
            }

            MessageBox.Show("Xóa dòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
