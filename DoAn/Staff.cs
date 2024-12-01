using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace DoAn
{
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển mật khẩu thành mảng byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển mảng byte thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                // Trả về mật khẩu đã băm
                return builder.ToString();
            }
        }
        private void LoadStaffData()
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "SELECT * FROM NHANVIEN"; 

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable); 

                    DanhSachNhanVien.DataSource = dataTable; 
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridViewHeaders()
        {
            DanhSachNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 49, 61);
            DanhSachNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; 
            DanhSachNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); 
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            CustomizeDataGridViewHeaders();

            LoadStaffData();
        }

        private void BtnThemNhanVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(HoTen.Text) || string.IsNullOrWhiteSpace(GioiTinh.Text) ||
                string.IsNullOrWhiteSpace(ChucVu.Text) || string.IsNullOrWhiteSpace(Luong.Text) ||
                string.IsNullOrWhiteSpace(TenDangNhap.Text) || string.IsNullOrWhiteSpace(MatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra tài khoản đã tồn tại chưa
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string queryCheckAccount = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENTK = @TENTK";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmdCheckAccount = new SqlCommand(queryCheckAccount, connection);
                cmdCheckAccount.Parameters.AddWithValue("@TENTK", TenDangNhap.Text);
                int accountExists = (int)cmdCheckAccount.ExecuteScalar();

                if (accountExists > 0)
                {
                    MessageBox.Show("Tài khoản đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Thêm tài khoản mới vào bảng TAIKHOAN
            string queryInsertAccount = "INSERT INTO TAIKHOAN (TENTK, MATKHAU) VALUES (@TENTK, @MATKHAU)";

            string passwordHash = HashPassword(MatKhau.Text); // Giả sử bạn có hàm mã hóa mật khẩu.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmdInsertAccount = new SqlCommand(queryInsertAccount, connection);
                cmdInsertAccount.Parameters.AddWithValue("@TENTK", TenDangNhap.Text);
                cmdInsertAccount.Parameters.AddWithValue("@MATKHAU", passwordHash);
                cmdInsertAccount.ExecuteNonQuery();
            }

            // Lấy mã tài khoản vừa tạo
            int newAccountId;
            string queryGetAccountId = "SELECT MATK FROM TAIKHOAN WHERE TENTK = @TENTK";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmdGetAccountId = new SqlCommand(queryGetAccountId, connection);
                cmdGetAccountId.Parameters.AddWithValue("@TENTK", TenDangNhap.Text);
                newAccountId = (int)cmdGetAccountId.ExecuteScalar();
            }

            // Thêm thông tin nhân viên vào bảng NHANVIEN
            string queryInsertEmployee = "INSERT INTO NHANVIEN (HOTEN, NGAYVL, SDT, GIOITINH, LUONG, CHUCVU, MATK) " +
                                        "VALUES (@HOTEN, @NGAYVL, @SDT, @GIOITINH, @LUONG, @CHUCVU, @MATK)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmdInsertEmployee = new SqlCommand(queryInsertEmployee, connection);
                cmdInsertEmployee.Parameters.AddWithValue("@HOTEN", HoTen.Text);
                cmdInsertEmployee.Parameters.AddWithValue("@NGAYVL", DateTime.Now); // Ngày vào làm
                cmdInsertEmployee.Parameters.AddWithValue("@SDT", "1234567890"); // Giả sử có giá trị số điện thoại
                cmdInsertEmployee.Parameters.AddWithValue("@GIOITINH", GioiTinh.Text);
                cmdInsertEmployee.Parameters.AddWithValue("@LUONG", decimal.Parse(Luong.Text));
                cmdInsertEmployee.Parameters.AddWithValue("@CHUCVU", ChucVu.Text);
                cmdInsertEmployee.Parameters.AddWithValue("@MATK", newAccountId);
                cmdInsertEmployee.ExecuteNonQuery();
            }

            MessageBox.Show("Nhân viên đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCapNhatNhanVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(HoTen.Text) || string.IsNullOrWhiteSpace(GioiTinh.Text) ||
                string.IsNullOrWhiteSpace(ChucVu.Text) || string.IsNullOrWhiteSpace(Luong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Tìm kiếm nhân viên bằng tên và chức vụ
            string queryCheckEmployee = "SELECT MANV FROM NHANVIEN WHERE HOTEN = @HOTEN AND CHUCVU = @CHUCVU";

            int manv = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmdCheckEmployee = new SqlCommand(queryCheckEmployee, connection);
                cmdCheckEmployee.Parameters.AddWithValue("@HOTEN", HoTen.Text);
                cmdCheckEmployee.Parameters.AddWithValue("@CHUCVU", ChucVu.Text);

                var result = cmdCheckEmployee.ExecuteScalar();

                if (result != null)
                {
                    manv = (int)result;
                }
            }

            // Nếu không tìm thấy nhân viên, hiển thị lỗi
            if (manv == -1)
            {
                MessageBox.Show("Không tìm thấy nhân viên với tên và chức vụ đã nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật thông tin nhân viên
            string queryUpdateEmployee = "UPDATE NHANVIEN SET HOTEN = @HOTEN, GIOITINH = @GIOITINH, LUONG = @LUONG " +
                                         "WHERE MANV = @MANV";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmdUpdateEmployee = new SqlCommand(queryUpdateEmployee, connection);
                cmdUpdateEmployee.Parameters.AddWithValue("@HOTEN", HoTen.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@GIOITINH", GioiTinh.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@LUONG", decimal.Parse(Luong.Text));
                cmdUpdateEmployee.Parameters.AddWithValue("@MANV", manv);

                int rowsAffected = cmdUpdateEmployee.ExecuteNonQuery();

                // Kiểm tra xem có bản ghi nào bị cập nhật không
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thông tin nhân viên đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào được thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private int selectedEmployeeId = -1; 
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedEmployeeId = (int)DanhSachNhanVien.Rows[e.RowIndex].Cells["MANV"].Value;
            }
        }

        private void BtnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên này?", "Xóa nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                DeleteEmployee(selectedEmployeeId);
            }
        }

        private void DeleteEmployee(int manv)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            string queryDeleteEmployee = "DELETE FROM NHANVIEN WHERE MANV = @MANV";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmdDelete = new SqlCommand(queryDeleteEmployee, connection);
                    cmdDelete.Parameters.AddWithValue("@MANV", manv);

                    int rowsAffected = cmdDelete.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Nhân viên đã được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadStaffData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
