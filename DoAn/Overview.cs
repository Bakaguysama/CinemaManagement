using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoAn
{
    public partial class Overview : Form
    {
        public Overview()
        {
            InitializeComponent();
            ConfigureBarChart();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ConfigureBarChart()
        {
            // Xóa các series mặc định nếu có
            chart1.Series.Clear();

            // Tạo một Series mới cho biểu đồ
            Series series = new Series("Ngàn VNĐ");
            series.ChartType = SeriesChartType.Bar; // Kiểu biểu đồ ngang

            // Thêm dữ liệu vào Series
            series.Points.AddXY("Avengers", 50);
            series.Points.AddXY("Deadpool and Wolverine", 70);
            series.Points.AddXY("Doraemon", 40);
            series.Points.AddXY("Lord of rings", 60);
            series.Points.AddXY("Item 5", 120);

            // Tùy chỉnh hiển thị giá trị trên mỗi cột/bar
            series.IsValueShownAsLabel = true;

            series.Points[0].Color = System.Drawing.Color.Red;    // Thanh Item 1
            series.Points[1].Color = System.Drawing.Color.Blue;   // Thanh Item 2
            series.Points[2].Color = System.Drawing.Color.Green;  // Thanh Item 3
            series.Points[3].Color = System.Drawing.Color.Orange; // Thanh Item 4
            series.Points[4].Color = System.Drawing.Color.Purple; // Thanh Item 5

            // Thêm Series vào Chart
            chart1.Series.Add(series);

            // Tùy chỉnh ChartArea (trục X, trục Y, và diện tích vẽ biểu đồ)
            chart1.ChartAreas[0].AxisX.Title = "Phim";
            chart1.ChartAreas[0].AxisY.Title = "Doanh thu";
            chart1.ChartAreas[0].AxisX.Interval = 1; // Khoảng cách giữa các nhãn trục X

            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true; // Cho phép zoom trên trục X
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true; // Cho phép zoom trên trục Y
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;    // Bật con trỏ trục X
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true; // Bật chọn zoom trên trục X
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;    // Bật con trỏ trục Y
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true; // Bật chọn zoom trên trục Y
            // Thêm tiêu đề cho biểu đồ
            chart1.Titles.Clear();
            chart1.Titles.Add("Top 5 phim có doanh thu cao nhất");
            chart1.Titles[0].Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
        }

        private void Overview_Load(object sender, EventArgs e)
        {

        }
    }
}
