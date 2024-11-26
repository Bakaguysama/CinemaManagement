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
    public partial class CaNhan : Form
    {
        public CaNhan()
        {
            InitializeComponent();
        }

        private void guna2Separator3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Nam.Checked)
            {
                radioButton_Nu.Checked = false;
            }
            else
                radioButton_Nu.Checked = true;
        }

        private void radioButton_Nu_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Nu.Checked)
            {
                radioButton_Nam.Checked = false;
            }
            else
                radioButton_Nam.Checked = true;
        }

        private void guna2Button_SuaMatKhau_Click(object sender, EventArgs e)
        {
            ChinhSuaMatKhau frm = new ChinhSuaMatKhau();
            frm.Show();
        }
    }
}
