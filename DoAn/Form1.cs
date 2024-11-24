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
    public partial class form_Login : Form
    {
        public form_Login()
        {
            InitializeComponent();
        }

        private void guna2Button_loginForm_SignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            signupForm signupForm = new signupForm();
            signupForm.ShowDialog();
            this.Show();
        }

        private void guna2Button_loginForm_Login_Click(object sender, EventArgs e)
        {   
            this.Hide();
            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
