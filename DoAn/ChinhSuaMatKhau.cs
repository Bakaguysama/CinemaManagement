using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class ChinhSuaMatKhau : Form
    {
        public ChinhSuaMatKhau()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ToggleSwitch_signupForm_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch_signupForm_ShowPassword.Checked)
            {
                guna2TextBox_NewPW.PasswordChar = '\0';
                guna2TextBox_ConfirmNewPW.PasswordChar = '\0';
            }
            else
            {
                guna2TextBox_NewPW.PasswordChar = '●';
                guna2TextBox_ConfirmNewPW.PasswordChar = '●';
            }
        }

        private void guna2Button_XacNhan_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_OldPW.Text == "" || guna2TextBox_NewPW.Text == "" || guna2TextBox_ConfirmNewPW.Text == "")
            {
                guna2MessageDialog_Warning.Show("Vui lòng nhập đủ thông tin", "Cảnh báo");
            }
            else if (guna2TextBox_OldPW.Text != LogInForm.GlobalVariables.passWord)
            {
                guna2MessageDialog_Warning.Show("Mật khẩu cũ không chính xác", "Cảnh báo");
            }
            else if (guna2TextBox_NewPW.Text != guna2TextBox_ConfirmNewPW.Text)
            {
                guna2MessageDialog_Warning.Show("Mật khẩu xác nhận không trùng khớp", "Cảnh báo");
            }
            else
            {
                DialogResult res = MessageBox.Show("Bạn đã chắc chắn muốn thay đổi mật khẩu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string insertQuery = @"UPDATE TAIKHOAN
                               SET MATKHAU = @MATKHAU
                               WHERE TENTK = @TENTK";

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                        {
                            conn.Open();
                            using (SqlCommand commandInsert = new SqlCommand(insertQuery, conn))
                            {
                                commandInsert.Parameters.Add("@MATKHAU", SqlDbType.NVarChar).Value = guna2TextBox_NewPW.Text;
                                commandInsert.Parameters.Add("@TENTK", SqlDbType.NVarChar).Value = LogInForm.GlobalVariables.userName;
                                int rowsAffected = commandInsert.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LogInForm.loadMatKhau();
                                    guna2TextBox_ConfirmNewPW.Clear();
                                    guna2TextBox_NewPW.Clear();
                                    guna2TextBox_OldPW.Clear();
                                    this.Close();
                                }
                                else
                                {
                                    guna2MessageDialog_Warning.Show("Không thể đổi mật khẩu. Vui lòng thử lại.", "Cảnh báo");
                                }
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Lỗi cơ sở dữ liệu: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
