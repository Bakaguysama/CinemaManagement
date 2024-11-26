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
    }
}
