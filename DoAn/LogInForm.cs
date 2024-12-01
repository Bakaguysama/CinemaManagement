using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoAn
{
    public partial class LogInForm : Form
    {
        public static class GlobalVariables
        {
            public static string userName { get; set; } = string.Empty;
            public static string passWord { get; set; } = string.Empty;
        }

        private string connStr = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
        public LogInForm()
        {
            InitializeComponent();
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch_signupForm_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch_signupForm_ShowPassword.Checked)
            {
                // Đặt PasswordChar là '\0' (kí tự null) để hiển thị mật khẩu
                guna2TextBox_signupForm_Password.PasswordChar = '\0';
                
            }
            else
            {
                // Đặt lại PasswordChar thành kí tự che dấu (ví dụ: '●')
                guna2TextBox_signupForm_Password.PasswordChar = '●';
                
            }
        }

        private void guna2Button_signupForm_Register_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button_signupForm_Signin_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_signupForm_Username.Text == "" || guna2TextBox_signupForm_Password.Text == "")
            {
                guna2MessageDialog_Warning.Show("Vui lòng nhập dữ liệu", "Cảnh báo");
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string tenTaiKhoan = guna2TextBox_signupForm_Username.Text.Trim();
                    string matKhau = guna2TextBox_signupForm_Password.Text.Trim();

                    string selectData = "SELECT * FROM TAIKHOAN WHERE LTRIM(RTRIM(TENTK)) = @TENTK AND LTRIM(RTRIM(MATKHAU)) = @MATKHAU";


                    using (SqlCommand command = new SqlCommand(selectData, connection))
                    {
                        command.Parameters.AddWithValue("@TENTK", tenTaiKhoan);
                        command.Parameters.AddWithValue("@MATKHAU", matKhau);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        DataTable table = new DataTable();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            guna2MessageDialog_Information.Show("Đăng nhập thành công", "Thông báo");
                            GlobalVariables.userName = tenTaiKhoan;
                            GlobalVariables.passWord = generateMK(tenTaiKhoan);
                            this.Hide();
                            LoadingForm loadingForm = new LoadingForm();
                            loadingForm.Show();
                        }
                        else
                        {
                            guna2MessageDialog_Warning.Show("Tên tài khoản hoặc mật khẩu của bạn bị sai!", "Cảnh báo");
                            return;
                        }
                    }
                }
            }
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            string tenTaiKhoan = guna2TextBox_signupForm_Username.Text.Trim();
            string matKhau = guna2TextBox_signupForm_Password.Text.Trim();

        }

        private void lbl_DangKy_Click(object sender, EventArgs e)
        {
            this.Hide();
            signupForm signupForm = new signupForm();
            signupForm.Show();
        }

        private void LogInForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        public static string generateMK(string tenTK)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "SELECT MATKHAU FROM TAIKHOAN WHERE TENTK = @TENTK";
            string matKhau = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho truy vấn
                        command.Parameters.Add("@TENTK", SqlDbType.NVarChar).Value = tenTK;

                        // Thực thi truy vấn và lấy giá trị mật khẩu
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            matKhau = result.ToString(); // Lấy giá trị MATKHAU
                        }
                        else
                        {
                            matKhau = "Mật khẩu không tồn tại";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi SQL
                Console.WriteLine($"Lỗi truy vấn SQL: {ex.Message}");
                matKhau = "Lỗi truy vấn cơ sở dữ liệu";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khác
                Console.WriteLine($"Lỗi hệ thống: {ex.Message}");
                matKhau = "Lỗi không xác định";
            }

            return matKhau; // Trả về mật khẩu hoặc thông báo lỗi
        }
        public static void loadMatKhau()
        {
            GlobalVariables.passWord = generateMK(GlobalVariables.userName);
        }
    }
}
