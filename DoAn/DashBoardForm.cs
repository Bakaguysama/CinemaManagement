using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace DoAn
{
    public partial class DashBoardForm : Form
    {
        public DashBoardForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DashBoardForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
        }


        private void guna2Button_TrangChu_Click(object sender, EventArgs e)
        {
            guna2Panel_Container.Controls.Clear(); // Xóa các control cũ trong panel.

            // Tạo một instance của Form (Dashboard).
            var dashboard = new Dashboard();

            // Đặt Form không còn là cửa sổ độc lập.
            dashboard.TopLevel = false;

            // Loại bỏ đường viền của Form (nếu cần).
            dashboard.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            // Gắn Form vào panel.
            dashboard.Dock = DockStyle.Fill;

            // Thêm Form vào panel.
            guna2Panel_Container.Controls.Add(dashboard);

            // Hiển thị Form.
            dashboard.Show();
        }

        private void guna2Button_Phim_Click(object sender, EventArgs e)
        {
            container(new Movie());

        }
        private void container(object _form)
        {

            if (guna2Panel_Container.Controls.Count > 0) guna2Panel_Container.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel_Container.Controls.Add(fm);
            guna2Panel_Container.Tag = fm;
            fm.Show();

        }
    }
}
