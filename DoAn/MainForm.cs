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
    public partial class MainForm : Form
    {
        private Overview overviewForm;
        public void addControl(Form f)
        {
            panel_Container.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            panel_Container.Controls.Add(f);
            f.Show();
        }

        public MainForm()
        {
            InitializeComponent();
            guna2Button_TrangChu.Checked = true;

            
            guna2Button_TrangChu_Click(null, EventArgs.Empty);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }

        private void guna2Button_TrangChu_Click(object sender, EventArgs e)
        {
            if (overviewForm == null || overviewForm.IsDisposed)
            {
                overviewForm = new Overview(); // Tạo Form Overview
            }

            // Thêm Overview vào panel
            addControl(overviewForm);
        }

        private void guna2Button_DatVe_Click(object sender, EventArgs e)
        {
            addControl(new Ticket());

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void addStaff_btn_Click(object sender, EventArgs e)
        {
            addControl(new Staff());
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel_Container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button_ThucAnDoUong_Click(object sender, EventArgs e)
        {
            addControl(new FoodAndDrink());

        }
    }
}
