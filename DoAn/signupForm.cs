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
    public partial class signupForm : Form
    {
        

        private static string connStr = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

        public signupForm()
        {
            InitializeComponent();
            
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch_signupForm_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch_signupForm_ShowPassword.Checked)
            {
                // Đặt PasswordChar là '\0' (kí tự null) để hiển thị mật khẩu
                guna2TextBox_signupForm_Password.PasswordChar = '\0';
                guna2TextBox_signupForm_ConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                // Đặt lại PasswordChar thành kí tự che dấu (ví dụ: '●')
                guna2TextBox_signupForm_Password.PasswordChar = '●';
                guna2TextBox_signupForm_ConfirmPassword.PasswordChar = '●';
            }
        }

        private void guna2Button_signupForm_Signin_Click(object sender, EventArgs e)
        {
            this.Close();
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button_signupForm_Register_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_signupForm_Username.Text == "" || guna2TextBox_signupForm_Password.Text == "" || guna2TextBox_signupForm_ConfirmPassword.Text == "")
            {
                guna2MessageDialog_Warning.Show("Vui lòng nhập dữ liệu", "Cảnh báo");
                return;
            }
            else if (guna2TextBox_signupForm_Password.Text != guna2TextBox_signupForm_ConfirmPassword.Text)
            {
                guna2MessageDialog_Warning.Show("Mật khẩu xác nhận không trùng khớp", "Cảnh báo");
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    try
                    {
                        connection.Open();
                        string tenTaiKhoan = guna2TextBox_signupForm_Username.Text.Trim();
                        string matKhau = guna2TextBox_signupForm_Password.Text.Trim();

                        // Kiểm tra xem tài khoản đã tồn tại hay chưa
                        if (KiemTraTaiKhoanTonTai(connection, tenTaiKhoan))
                        {
                            guna2MessageDialog_Warning.Show("Tên tài khoản đã tồn tại. Vui lòng chọn tên khác.");
                            guna2TextBox_signupForm_Username.Clear();
                            guna2TextBox_signupForm_Password.Clear();
                            guna2TextBox_signupForm_ConfirmPassword.Clear();
                            return;
                        }
                        else
                        {
                            // Nếu chưa tồn tại, thực hiện thêm mới tài khoản
                            string queryInsert = "INSERT INTO TAIKHOAN (TENTK, MATKHAU) VALUES (@TENTK, @MATKHAU)";

                            using (SqlCommand commandInsert = new SqlCommand(queryInsert, connection))
                            {
                                // Gán giá trị cho các tham số
                                commandInsert.Parameters.AddWithValue("@TENTK", tenTaiKhoan);
                                commandInsert.Parameters.AddWithValue("@MATKHAU", matKhau);

                                int rowsAffected = commandInsert.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    guna2MessageDialog_Information.Show("Tài khoản đã được tạo thành công!","Thông báo");
                                    LogInForm.GlobalVariables.userName = tenTaiKhoan;
                                    LogInForm.GlobalVariables.passWord = matKhau;
                                    this.Hide();
                                    LoadingForm loadingForm = new LoadingForm();
                                    loadingForm.Show();
                                }
                                else
                                {
                                    guna2MessageDialog_Warning.Show("Không thể tạo tài khoản. Vui lòng thử lại.", "Cảnh báo");
                                    guna2TextBox_signupForm_Username.Clear();
                                    guna2TextBox_signupForm_Password.Clear();
                                    guna2TextBox_signupForm_ConfirmPassword.Clear();
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        guna2MessageDialog_Warning.Show("Lỗi: " + ex.Message, "Cảnh báo");
                        guna2TextBox_signupForm_Username.Clear();
                        guna2TextBox_signupForm_Password.Clear();
                        guna2TextBox_signupForm_ConfirmPassword.Clear();
                        return;
                    }
                }

                

            }
        }

        static bool KiemTraTaiKhoanTonTai(SqlConnection connection, string tenTaiKhoan)
        {
            string queryCheck = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENTK = @TENTK";

            using (SqlCommand commandCheck = new SqlCommand(queryCheck, connection))
            {
                commandCheck.Parameters.AddWithValue("@TENTK", tenTaiKhoan);

                int count = (int)commandCheck.ExecuteScalar();

                return count > 0; // Trả về true nếu tài khoản đã tồn tại
            }
        }
    }
}
