using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class signupForm : Form
    {
        private form_Login mainform;
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
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
