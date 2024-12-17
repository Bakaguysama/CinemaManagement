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

        private void LoadStaffData()
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
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

                    DanhSachNhanVien.RowHeadersVisible = false;  
                    DanhSachNhanVien.AllowUserToAddRows = false; 

                    DanhSachNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

            DanhSachNhanVien.Refresh();
        }



        private void Staff_Load(object sender, EventArgs e)
        {
            CustomizeDataGridViewHeaders();

            LoadStaffData();
        }
        private void BtnThemNhanVien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HoTen.Text) || string.IsNullOrWhiteSpace(GioiTinh.Text) ||
                string.IsNullOrWhiteSpace(ChucVu.Text) || string.IsNullOrWhiteSpace(Luong.Text) ||
                string.IsNullOrWhiteSpace(TenDangNhap.Text) || string.IsNullOrWhiteSpace(MatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Sinh mã nhân viên
                    string employeeCode = GenerateEmployeeCode(connectionString);

                    // Thêm nhân viên mới vào bảng NHANVIEN
                    AddNewEmployee(connection, transaction, employeeCode, HoTen.Text, GioiTinh.Text, ChucVu.Text, Luong.Text, SDT.Text);

                    // Thêm tài khoản mới vào bảng TAIKHOAN sau khi đã có nhân viên
                    string newAccountId = AddNewAccount(connection, transaction, TenDangNhap.Text, MatKhau.Text, employeeCode);

                    if (newAccountId == null)
                    {
                        MessageBox.Show("Không thể thêm tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    transaction.Commit();
                    MessageBox.Show("Nhân viên và tài khoản đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            LoadStaffData();
        }

        // Thêm nhân viên mới vào bảng NHANVIEN
        private void AddNewEmployee(SqlConnection connection, SqlTransaction transaction, string manv, string hoten, string gioitinh, string chucvu, string luong, string sdt)
        {
            string queryInsertEmployee = "INSERT INTO NHANVIEN (MANV, HOTEN, NGAYVL, SDT, GIOITINH, LUONG, CHUCVU) " +
                                         "VALUES (@MANV, @HOTEN, @NGAYVL, @SDT, @GIOITINH, @LUONG, @CHUCVU)";
            using (SqlCommand cmdInsertEmployee = new SqlCommand(queryInsertEmployee, connection, transaction))
            {
                cmdInsertEmployee.Parameters.AddWithValue("@MANV", manv);
                cmdInsertEmployee.Parameters.AddWithValue("@HOTEN", hoten);
                cmdInsertEmployee.Parameters.AddWithValue("@NGAYVL", DateTime.Now);
                cmdInsertEmployee.Parameters.AddWithValue("@SDT", sdt);
                cmdInsertEmployee.Parameters.AddWithValue("@GIOITINH", gioitinh);
                cmdInsertEmployee.Parameters.AddWithValue("@LUONG", decimal.Parse(luong));
                cmdInsertEmployee.Parameters.AddWithValue("@CHUCVU", chucvu);
                cmdInsertEmployee.ExecuteNonQuery();
            }
        }

        // Thêm tài khoản mới vào bảng TAIKHOAN (có liên kết với MANV)
        private string AddNewAccount(SqlConnection connection, SqlTransaction transaction, string tenDangNhap, string matKhau, string manv)
        {
            string queryInsertAccount = "INSERT INTO TAIKHOAN (TENTK, MATKHAU, MANV) OUTPUT INSERTED.TENTK VALUES (@TENTK, @MATKHAU, @MANV)";
            using (SqlCommand cmdInsertAccount = new SqlCommand(queryInsertAccount, connection, transaction))
            {
                cmdInsertAccount.Parameters.AddWithValue("@TENTK", tenDangNhap);
                cmdInsertAccount.Parameters.AddWithValue("@MATKHAU", matKhau);
                cmdInsertAccount.Parameters.AddWithValue("@MANV", manv); // Liên kết với MANV của nhân viên
                return (string)cmdInsertAccount.ExecuteScalar();
            }
        }


        private string GenerateEmployeeCode(string connectionString)
        {
            string newCode = "NV001";  // Mã mặc định nếu chưa có nhân viên nào

            string queryGetMaxEmployeeCode = "SELECT MAX(MANV) FROM NHANVIEN"; // Lấy mã nhân viên lớn nhất hiện tại
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmdGetMaxEmployeeCode = new SqlCommand(queryGetMaxEmployeeCode, connection);
                var result = cmdGetMaxEmployeeCode.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    string maxCode = (string)result;
                    int codeNumber = int.Parse(maxCode.Substring(2)); // Lấy phần số sau "NV"
                    codeNumber++; // Tăng mã nhân viên lên 1
                    newCode = "NV" + codeNumber.ToString("D3"); // Đảm bảo định dạng NV001, NV002, ...
                }
            }

            return newCode;
        }

        private void BtnCapNhatNhanVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường thông tin không được để trống
            if (string.IsNullOrWhiteSpace(HoTen.Text) || string.IsNullOrWhiteSpace(GioiTinh.Text) ||
                string.IsNullOrWhiteSpace(ChucVu.Text) || string.IsNullOrWhiteSpace(Luong.Text) ||
                string.IsNullOrWhiteSpace(TenDangNhap.Text) || string.IsNullOrWhiteSpace(MatKhau.Text) || string.IsNullOrWhiteSpace(SDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kết nối tới cơ sở dữ liệu
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            // Kiểm tra xem nhân viên có tồn tại trong cơ sở dữ liệu không
            string queryCheckEmployee = "SELECT MANV FROM NHANVIEN WHERE MANV = @MANV";

            string manv = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra xem nhân viên có tồn tại với MANV hiện tại không
                SqlCommand cmdCheckEmployee = new SqlCommand(queryCheckEmployee, connection);
                cmdCheckEmployee.Parameters.AddWithValue("@MANV", selectedEmployeeId);

                var result = cmdCheckEmployee.ExecuteScalar();

                if (result != null)
                {
                    manv = result.ToString();  // Sử dụng string thay vì int
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã nhân viên này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Cập nhật thông tin nhân viên vào cơ sở dữ liệu
            string queryUpdateEmployee = "UPDATE NHANVIEN SET HOTEN = @HOTEN, GIOITINH = @GIOITINH, CHUCVU = @CHUCVU, LUONG = @LUONG, SDT = @SDT " +
                                         "WHERE MANV = @MANV; " +
                                         "UPDATE TAIKHOAN SET TENTK = @TENTK, MATKHAU = @MATKHAU WHERE MANV = @MANV";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmdUpdateEmployee = new SqlCommand(queryUpdateEmployee, connection);
                cmdUpdateEmployee.Parameters.AddWithValue("@HOTEN", HoTen.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@GIOITINH", GioiTinh.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@CHUCVU", ChucVu.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@LUONG", decimal.Parse(Luong.Text)); // Chuyển đổi sang decimal cho trường lương
                cmdUpdateEmployee.Parameters.AddWithValue("@SDT", SDT.Text); // Thêm cột SDT cho bảng NHANVIEN
                cmdUpdateEmployee.Parameters.AddWithValue("@TENTK", TenDangNhap.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@MATKHAU", MatKhau.Text);
                cmdUpdateEmployee.Parameters.AddWithValue("@MANV", manv);  // Sử dụng manv dưới dạng string

                int rowsAffected = cmdUpdateEmployee.ExecuteNonQuery();

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


        private string selectedEmployeeId = string.Empty;

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DanhSachNhanVien.Rows.Count)
            {
                try
                {
                    if (DanhSachNhanVien.Columns.Contains("MANV"))
                    {
                        var value = DanhSachNhanVien.Rows[e.RowIndex].Cells["MANV"].Value;

                        if (value != DBNull.Value && value != null)
                        {
                            selectedEmployeeId = value.ToString();  // Lưu ID nhân viên

                            // Gọi phương thức để load thông tin nhân viên
                            LoadEmployeeDetails(selectedEmployeeId);
                        }
                        else
                        {
                            MessageBox.Show("Mã nhân viên không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cột 'MANV' không tồn tại trong DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadEmployeeDetails(string employeeId)
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT HOTEN, GIOITINH, CHUCVU, LUONG, TENTK, MATKHAU, SDT 
                         FROM NHANVIEN NV 
                         JOIN TAIKHOAN TK ON NV.MANV = TK.MANV
                         WHERE NV.MANV = @MANV";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MANV", employeeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Cập nhật các trường thông tin vào các ô trên giao diện
                            HoTen.Text = reader["HOTEN"].ToString();
                            GioiTinh.SelectedItem = reader["GIOITINH"].ToString();
                            ChucVu.SelectedItem = reader["CHUCVU"].ToString();
                            Luong.Text = reader["LUONG"].ToString();
                            TenDangNhap.Text = reader["TENTK"].ToString();
                            MatKhau.Text = reader["MATKHAU"].ToString();
                            SDT.Text = reader["SDT"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin nhân viên với mã nhân viên này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên này? Tất cả dữ liệu liên quan sẽ bị xóa.",
                                                        "Xóa nhân viên",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    DeleteEmployee(selectedEmployeeId);
                    MessageBox.Show("Nhân viên đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadStaffData();
        }

        private void DeleteEmployee(string employeeId)
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Xóa tài khoản của nhân viên (nếu tồn tại)
                    string queryDeleteAccount = "DELETE FROM TAIKHOAN WHERE MANV = @MANV";
                    using (SqlCommand cmdDeleteAccount = new SqlCommand(queryDeleteAccount, connection, transaction))
                    {
                        cmdDeleteAccount.Parameters.AddWithValue("@MANV", employeeId);
                        cmdDeleteAccount.ExecuteNonQuery();
                    }

                    // Xóa dữ liệu trong NHAPSP (nếu có)
                    string queryDeleteProductEntries = "DELETE FROM NHAPSP WHERE MANV = @MANV";
                    using (SqlCommand cmdDeleteProductEntries = new SqlCommand(queryDeleteProductEntries, connection, transaction))
                    {
                        cmdDeleteProductEntries.Parameters.AddWithValue("@MANV", employeeId);
                        cmdDeleteProductEntries.ExecuteNonQuery();
                    }

                    // Xóa nhân viên khỏi bảng NHANVIEN
                    string queryDeleteEmployee = "DELETE FROM NHANVIEN WHERE MANV = @MANV";
                    using (SqlCommand cmdDeleteEmployee = new SqlCommand(queryDeleteEmployee, connection, transaction))
                    {
                        cmdDeleteEmployee.Parameters.AddWithValue("@MANV", employeeId);
                        cmdDeleteEmployee.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Có lỗi xảy ra khi xóa nhân viên: {ex.Message}");
                }
            }
        }

        private void DanhSachNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
