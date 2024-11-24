using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DoAn
{
    public partial class Dashboard : Form
    {
        List<string> imagePaths = new List<string>();
        int currentIndex = 0;

        public Dashboard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // Loại bỏ đường viền
            this.Dock = DockStyle.Fill;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Thêm các đường dẫn ảnh vào danh sách
            imagePaths.Add("D:\\UIT\\2nd year\\LTTQ\\rac\\350x495-linhmieu.jpg");
            imagePaths.Add("D:\\UIT\\2nd year\\LTTQ\\rac\\700x1000-gladiator.jpg");
            imagePaths.Add("D:\\UIT\\2nd year\\LTTQ\\rac\\wkd_forestduo_470x700.jpg");

            // Hiển thị ảnh đầu tiên
            if (imagePaths.Count > 0)
            {
                guna2PictureBox_MovieSlider.Image = Image.FromFile(imagePaths[currentIndex]);
            }

            // Khởi động Timer
            sliderTimer.Start();
        }

        private void sliderTimer_Tick(object sender, EventArgs e)
        {
            // Chuyển sang ảnh tiếp theo
            currentIndex++;
            if (currentIndex >= imagePaths.Count)
                currentIndex = 0;

            // Thay đổi ảnh với hiệu ứng
            guna2PictureBox_MovieSlider.Image = Image.FromFile(imagePaths[currentIndex]);
        }

        private void guna2Button_SliderNext_Click(object sender, EventArgs e)
        {
            sliderTimer.Stop(); // Dừng Timer tạm thời

            // Chuyển sang ảnh tiếp theo
            currentIndex++;
            if (currentIndex >= imagePaths.Count)
                currentIndex = 0;

            // Thay đổi ảnh với hiệu ứng
            guna2PictureBox_MovieSlider.Image = Image.FromFile(imagePaths[currentIndex]);

            sliderTimer.Start(); // Khởi động lại Timer
        }

        private void guna2Button_SliderPrevious_Click(object sender, EventArgs e)
        {
            sliderTimer.Stop(); // Dừng Timer tạm thời

            // Chuyển sang ảnh trước đó
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = imagePaths.Count - 1;

            // Thay đổi ảnh với hiệu ứng
            guna2PictureBox_MovieSlider.Image = Image.FromFile(imagePaths[currentIndex]);

            sliderTimer.Start(); // Khởi động lại Timer
        }

        // Hàm thêm hiệu ứng khi thay đổi ảnh
        private void ChangeImageWithTransition(Image newImage)
        {
            
        }

        private void guna2PictureBox_MovieSlider_Click(object sender, EventArgs e)
        {

        }
    }
}
