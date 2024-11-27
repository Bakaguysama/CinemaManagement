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
    public partial class Food : Form
    {
        public Food()
        {
            InitializeComponent();
            txtSearch.Text = "Tìm kiếm";  // Văn bản mặc định
            txtSearch.ForeColor = System.Drawing.Color.Gray;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Search_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm")  
            {
                txtSearch.Text = "";  
                txtSearch.ForeColor = System.Drawing.Color.Black; 
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")  
            {
                txtSearch.Text = "Tìm kiếm"; 
                txtSearch.ForeColor = System.Drawing.Color.Gray; 
            }
        }
    }
}
