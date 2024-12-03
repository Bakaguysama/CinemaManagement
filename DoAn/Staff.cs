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

            string queryInsertAccount = "INSERT INTO TAIKHOAN (TENTK, MATKHAU) VALUES (@TENTK, @MATKHAU)";
            int newAccountId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmdInsertAccount = new SqlCommand(queryInsertAccount, connection);
                cmdInsertAccount.Parameters.AddWithValue("@TENTK", TenDangNhap.Text);
                cmdInsertAccount.Parameters.AddWithValue("@MATKHAU", MatKhau.Text);
                cmdInsertAccount.ExecuteNonQuery();

                string queryGetAccountId = "SELECT MATK FROM TAIKHOAN WHERE TENTK = @TENTK";
                SqlCommand cmdGetAccountId = new SqlCommand(queryGetAccountId, connection);
                cmdGetAccountId.Parameters.AddWithValue("@TENTK", TenDangNhap.Text);
                newAccountId = (int)cmdGetAccountId.ExecuteScalar();
            }

            string employeeCode = GenerateEmployeeCode(connectionString);

            string queryInsertEmployee = "INSERT INTO NHANVIEN (MANV, HOTEN, NGAYVL, SDT, GIOITINH, LUONG, CHUCVU, MATK) " +
                                         "VALUES (@MANV, @HOTEN, @NGAYVL, @SDT, @GIOITINH, @LUONG, @CHUCVU, @MATK)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmdInsertEmployee = new SqlCommand(queryInsertEmployee, connection);
                cmdInsertEmployee.Parameters.AddWithValue("@MANV", employeeCode);
                cmdInsertEmployee.Parameters.AddWithValue("@HOTEN", HoTen.Text);
                cmdInsertEmployee.Parameters.AddWithValue("@NGAYVL", DateTime.Now);
                cmdInsertEmployee.Parameters.AddWithValue("@GIOITINH", GioiTinh.Text);
                cmdInsertEmployee.Parameters.AddWithValue("@LUONG", decimal.Parse(Luong.Text));
                cmdInsertEmployee.Parameters.AddWithValue("@CHUCVU", ChucVu.Text);
                cmdInsertEmployee.Parameters.AddWithValue("@MATK", newAccountId);
                cmdInsertEmployee.ExecuteNonQuery();
            }

            MessageBox.Show("Nhân viên đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrWhiteSpace(HoTen.Text) || string.IsNullOrWhiteSpace(GioiTinh.Text) ||
                string.IsNullOrWhiteSpace(ChucVu.Text) || string.IsNullOrWhiteSpace(Luong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

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

            if (manv == -1)
            {
                MessageBox.Show("Không tìm thấy nhân viên với tên và chức vụ đã nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            if (e.RowIndex >= 0 && e.RowIndex < DanhSachNhanVien.Rows.Count )
            {
                try
                {
                    if (DanhSachNhanVien.Columns.Contains("MANV"))
                    {
                        var value = DanhSachNhanVien.Rows[e.RowIndex].Cells["MANV"].Value;

                        if (value != DBNull.Value && value != null)
                        {
                            selectedEmployeeId = value.ToString();  
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


        private void BtnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
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


        private void DeleteEmployee(string manv)
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

        private void DanhSachNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
