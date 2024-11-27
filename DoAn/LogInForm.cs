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
            public static string passWord { get; set; }
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
                            GlobalVariables.passWord = matKhau;
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
    }
}
