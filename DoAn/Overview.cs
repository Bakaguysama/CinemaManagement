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
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using System.Data.SqlTypes;
using Guna.UI2.WinForms;

namespace DoAn
{
    public partial class Overview : Form
    {
        private SqlConnection conn; // Kết nối toàn cục
        private const int MaxRetryAttempts = 3; // Số lần thử lại tối đa
        public DateTime defaultDate;

        public Overview()
        {
            InitializeComponent();
            ConfigureBarChart();
            defaultDate = new DateTime(2025, 1, 4);
            guna2DateTimePicker1.Value = DateTime.Now;
        }

        string convertDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ConfigureBarChart()
        {
            // Xóa các series mặc định nếu có
            chart1.Series.Clear();

            // Tạo một Series mới cho biểu đồ
            Series series = new Series("Triệu đồng");
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

            

            chart1.ChartAreas[0].CursorX.IsUserEnabled = false;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = false;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;

            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = false;

            // Đảm bảo không cắt nhãn
            chart1.ChartAreas[0].AxisY.LabelStyle.IsEndLabelVisible = true;
            chart1.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = false;

            //
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;

            // Tăng lề cho ChartArea
            chart1.ChartAreas[0].Position = new ElementPosition(20, 10, 70, 80); // Điều chỉnh vị trí tổng thể
            chart1.ChartAreas[0].InnerPlotPosition = new ElementPosition(15, 5, 75, 90); // Điều chỉnh không gian bên trong

            // Hiển thị nhãn rõ ràng
            chart1.ChartAreas[0].AxisY.LabelStyle.IsEndLabelVisible = true;
            chart1.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = false;
            // Thêm tiêu đề cho biểu đồ
            chart1.Titles.Clear();
            chart1.Titles.Add("Các phim có doanh thu cao nhất trong tuần");
            chart1.Titles[0].Font = new System.Drawing.Font("Segoe UI", 15, System.Drawing.FontStyle.Bold);
        }

        private void Overview_Load(object sender, EventArgs e)
        {
            guna2Button_HomNay.Checked = true; // Bật nút HomNay
            guna2Button_TuyChinh.Checked = false; // Tắt nút TuyChinh
            guna2Button_OK.Visible = false; // Ẩn nút OK

            string date = convertDate(defaultDate);
            TryConnectToDatabase();
            HienThi(date);
        }

        private void HienThi(string date)
        {
            hienthiCongSuatRap(date);
            hienthiDoanhThu(date);
            hienthiSoPhimDangChieu();
            hienthiSoVe(date);
            hienthiChart(date);
            HienThiDoanhThuChart(date);
        }

        private void hienthiChart(string date)
        {
            string sdate = Convert.ToDateTime(date).AddDays(-7).ToString("yyyy-MM-dd");
            string query = @"WITH DOANHTHUPHIM AS
                    (
                        SELECT 
                            vxp.MAPHIM,
                            SUM(SOVE * GIAVE) AS DOANHTHU
                        FROM VEXEMPHIM vxp
                        JOIN CTHD_VXP cthd_vxp ON cthd_vxp.MAVE = vxp.MAVE
                        JOIN HOADON hd ON hd.SOHD = cthd_vxp.SOHD
                        WHERE NGAYHD >= @sdate AND NGAYHD <= @date
                        GROUP BY vxp.MAPHIM
                    )
                    SELECT TOP 5
                        PHIM.TENPHIM,
                        DOANHTHUPHIM.DOANHTHU AS TIEN
                    FROM DOANHTHUPHIM
                    RIGHT JOIN PHIM ON PHIM.MAPHIM = DOANHTHUPHIM.MAPHIM
                    ORDER BY DOANHTHUPHIM.DOANHTHU DESC;";

            // Load data from database
            DataTable data = LoadDataWithParameters(query, new SqlParameter("@date", date), new SqlParameter("@sdate", sdate));

            // Xóa các series hiện có trong chart trước khi thêm dữ liệu mới
            chart1.Series.Clear();

            // Tạo một series mới cho biểu đồ
            Series series = new Series("Doanh thu Phim");
            series.ChartType = SeriesChartType.Bar; // Kiểu biểu đồ cột (Bar Chart)
            series.IsValueShownAsLabel = true; // Hiển thị giá trị trên các cột

            // Nếu có dữ liệu từ cơ sở dữ liệu, thêm vào series
            if (data.Rows.Count > 0)
            {
                bool hasData = false; // Kiểm tra xem có dữ liệu hợp lệ hay không

                foreach (DataRow row in data.Rows)
                {
                    // Kiểm tra giá trị của TIEN có phải là null hay không
                    string tenPhim = row["TENPHIM"]?.ToString() ?? "Không tên"; // Nếu null thì gán giá trị mặc định
                    decimal doanhThu = row["TIEN"] != DBNull.Value ? Convert.ToDecimal(row["TIEN"]) / 1000 : 0;

                    // Nếu doanh thu hợp lệ thì thêm vào series
                    if (doanhThu > 0)
                    {
                        series.Points.AddXY(tenPhim, doanhThu);
                        hasData = true; // Đã có dữ liệu hợp lệ
                    }
                }

                // Nếu có dữ liệu hợp lệ, thêm series vào chart
                if (hasData)
                {
                    chart1.Series.Add(series);

                    // Tùy chỉnh trục X và Y
                    chart1.ChartAreas[0].AxisX.Title = "Phim";
                    chart1.ChartAreas[0].AxisY.Title = "Nghìn đồng";
                    chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
                    chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;

                    // Thêm tiêu đề cho biểu đồ
                    chart1.Titles.Clear();
                    chart1.Titles.Add("Các phim có doanh thu cao nhất trong tuần");
                    chart1.Titles[0].Font = new Font("Segoe UI", 15, FontStyle.Bold);
                }
                else
                {
                    chart1.Text = "Không có dữ liệu hợp lệ."; // Nếu không có dữ liệu hợp lệ
                }
            }
            else
            {
                // Nếu không có dữ liệu nào
                chart1.Text = "Không có dữ liệu.";
            }
        }



        private void hienthiSoVe(string date)
        {
            string query = @"SELECT 
	                                COALESCE(SUM(SOVE), 0) AS SOVE
                                FROM CTHD_VXP cthd_vxp
                                JOIN HOADON hd ON hd.SOHD = cthd_vxp.SOHD
                                WHERE NGAYHD = @date";
            DataTable data = LoadDataWithParameters(query, new SqlParameter("@date", date));
            if (data.Rows.Count > 0)
            {
                label9.Text = Convert.ToInt32(data.Rows[0]["SOVE"]).ToString();
            }
            else
            {
                label9.Text = "N/A";
            }
        }

        private void hienthiSoPhimDangChieu()
        {
            string query = @"SELECT 
	                                COALESCE(COUNT(MAPHIM), 0) AS SOPHIMDANGCHIEU
                                FROM PHIM
                                WHERE TINHTRANG = N'Đang chiếu';";
            DataTable data = LoadData(query);
            if (data.Rows.Count > 0)
            {
                label8.Text = Convert.ToInt32(data.Rows[0]["SOPHIMDANGCHIEU"]).ToString();
            }
            else
            {
                label8.Text = "N/A";
            }
        }

        private void hienthiDoanhThu(string date)
        {
            string query = @"
                            WITH TIMDOANHTHUPHIM AS
                            (
                                SELECT 
                                    vxp.MAPHIM,
                                    SUM(SOVE * GIAVE) AS DOANHTHUp
                                FROM VEXEMPHIM vxp
                                JOIN CTHD_VXP cthd_vxp ON cthd_vxp.MAVE = vxp.MAVE
                                JOIN HOADON hd ON hd.SOHD = cthd_vxp.MAVE
                                WHERE NGAYHD = @date
                                GROUP BY vxp.MAPHIM
                            ),

                            TIMDOANHTHUSANPHAM AS
                            (
                                SELECT 
                                    sp.MASP,
                                    SUM(SOSP * GIA) AS DOANHTHUsp
                                FROM SANPHAM sp
                                JOIN CTHD_SP cthd_sp ON cthd_sp.MASP = sp.MASP
                                JOIN HOADON hd ON hd.SOHD = cthd_sp.SOHD
                                WHERE NGAYHD = @date
                                GROUP BY sp.MASP
                            )

                            SELECT 
                                COALESCE(SUM(DOANHTHUp), 0) + COALESCE(SUM(DOANHTHUsp), 0) AS TONGDOANHTHU
                            FROM 
                                (SELECT SUM(DOANHTHUp) AS DOANHTHUp FROM TIMDOANHTHUPHIM) AS DOANHTHU_PHIM,
                                (SELECT SUM(DOANHTHUsp) AS DOANHTHUsp FROM TIMDOANHTHUSANPHAM) AS DOANHTHU_SANPHAM;";
            DataTable data = LoadDataWithParameters(query, new SqlParameter("@date", date));
            if (data.Rows.Count > 0)
            {
                label3.Text = Convert.ToInt32(data.Rows[0]["TONGDOANHTHU"]).ToString() + "đ";
            }
            else
            {
                label3.Text = "N/A";
            }
        }

        private void hienthiCongSuatRap(string date)
        {
            DateTime dt;
            dt = DateTime.Parse(date);
            string formattedDate = dt.ToString("yyyy-MM-dd");
            string query = @"WITH TIMTONGSOGHE AS
                            (
	                            SELECT 
		                            SUM(SOCHO) AS TONGSOGHE
	                            FROM RAPCHIEUPHIM
                            ),

                            TIMTONGSOGHEDANGDUOCDUNG AS
                            (
	                            SELECT 
		                            COUNT(DISTINCT SOGHE) AS TONGSOGHEDANGDUOCDUNG
	                            FROM VEXEMPHIM vxp
	                            JOIN CTHD_VXP cthd ON vxp.MAVE = cthd.MAVE
	                            JOIN HOADON hd ON hd.SOHD = cthd.SOHD
	                            WHERE NGAYHD = @date
                            )

                            SELECT COALESCE(CAST (TONGSOGHEDANGDUOCDUNG AS FLOAT) / CAST (TONGSOGHE AS FLOAT) * 10000, 0) AS HIEUSUAT
                            FROM TIMTONGSOGHE, TIMTONGSOGHEDANGDUOCDUNG;";
            DataTable data = LoadDataWithParameters(query, new SqlParameter("@date", formattedDate));
            if (data.Rows.Count > 0)
            {
                int res = Convert.ToInt32(data.Rows[0]["HIEUSUAT"]);
                circularProgressBar_CongSuatRap.Value = res;
                circularProgressBar_CongSuatRap.Text = res.ToString() + "%";
            }
            else
            {
                circularProgressBar_CongSuatRap.Text = "N/A";
                circularProgressBar_CongSuatRap.Value = 0;
            }
        }

        private void TryConnectToDatabase()
        {
            string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            int attempt = 0;
            bool isConnected = false;

            while (attempt < MaxRetryAttempts && !isConnected)
            {
                try
                {
                    conn = new SqlConnection(connectionString);
                    conn.Open(); 
                    isConnected = true; 
                }
                catch (Exception ex)
                {
                    attempt++;
                    guna2Message_Warning.Show($"Lỗi kết nối lần {attempt}: {ex.Message}", "Cảnh báo");

                    if (attempt < MaxRetryAttempts)
                    {
                        guna2MessageDialog_ThongBao.Show("Đang thử kết nối lại...");
                    }
                    else
                    {
                        guna2Message_Warning.Show("Không thể kết nối đến cơ sở dữ liệu sau nhiều lần thử.", "Cảnh báo");
                    }
                }
            }
        }

        private void guna2Button_TuyChinh_Click(object sender, EventArgs e)
        {
            guna2Button_TuyChinh.Checked = true; // Bật nút TuyChinh
            guna2Button_HomNay.Checked = false; // Tắt nút HomNay
            guna2Button_OK.Visible = true; // Hiện nút OK
        }

        private void guna2Button_HomNay_Click(object sender, EventArgs e)
        {
            guna2Button_HomNay.Checked = true; // Bật nút HomNay
            guna2Button_TuyChinh.Checked = false; // Tắt nút TuyChinh
            guna2Button_OK.Visible = false; // Ẩn nút OK

            guna2DateTimePicker1.Value = DateTime.Now;
            string today = DateTime.Today.ToString("yyyy-MM-dd");

            // Gọi hàm HienThi với ngày hôm nay
            HienThi(today);
        }

        private void Overview_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private DataTable LoadData(string query)
        {
            DataTable dataTable = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            return dataTable;
        }

        private void guna2Button_OK_Click(object sender, EventArgs e)
        {
            defaultDate = guna2DateTimePicker1.Value;
            HienThi(convertDate(defaultDate));

        }
        private DataTable LoadDataWithParameters(string query, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
        
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters); // Gán các tham số vào lệnh SQL
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable); // Nạp dữ liệu vào DataTable
                }
            }
        
            return dataTable;
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            defaultDate = guna2DateTimePicker1.Value;
        }

        private void HienThiDoanhThuChart(string date)
        {
            DateTime endDate = DateTime.Parse(date);
            DateTime startDate = endDate.AddDays(-7);

            // Tạo truy vấn tối ưu cho 7 ngày
            string query = @"
                            WITH TIMDOANHTHUPHIM AS
                            (
                                SELECT 
                                    hd.NGAYHD,
                                    SUM(SOVE * GIAVE) AS DOANHTHUp
                                FROM VEXEMPHIM vxp
                                JOIN CTHD_VXP cthd_vxp ON cthd_vxp.MAVE = vxp.MAVE
                                JOIN HOADON hd ON hd.SOHD = cthd_vxp.MAVE
                                WHERE hd.NGAYHD BETWEEN @startDate AND @endDate
                                GROUP BY hd.NGAYHD
                            ),
                            TIMDOANHTHUSANPHAM AS
                            (
                                SELECT 
                                    hd.NGAYHD,
                                    SUM(SOSP * GIA) AS DOANHTHUsp
                                FROM SANPHAM sp
                                JOIN CTHD_SP cthd_sp ON cthd_sp.MASP = sp.MASP
                                JOIN HOADON hd ON hd.SOHD = cthd_sp.SOHD
                                WHERE hd.NGAYHD BETWEEN @startDate AND @endDate
                                GROUP BY hd.NGAYHD
                            )
                            SELECT 
                                COALESCE(PHIM.NGAYHD, SP.NGAYHD) AS NGAYHD,
                                COALESCE(SUM(PHIM.DOANHTHUp), 0) + COALESCE(SUM(SP.DOANHTHUsp), 0) AS TONGDOANHTHU
                            FROM 
                                (SELECT NGAYHD, SUM(DOANHTHUp) AS DOANHTHUp FROM TIMDOANHTHUPHIM GROUP BY NGAYHD) AS PHIM
                            FULL JOIN 
                                (SELECT NGAYHD, SUM(DOANHTHUsp) AS DOANHTHUsp FROM TIMDOANHTHUSANPHAM GROUP BY NGAYHD) AS SP
                            ON PHIM.NGAYHD = SP.NGAYHD
                            GROUP BY COALESCE(PHIM.NGAYHD, SP.NGAYHD)
                            ORDER BY NGAYHD;";

            // Thêm tham số cho truy vấn
            DataTable data = LoadDataWithParameters(query,
                new SqlParameter("@startDate", startDate.ToString("yyyy-MM-dd")),
                new SqlParameter("@endDate", endDate.ToString("yyyy-MM-dd")));

            // Vẽ biểu đồ
            chart2.Series.Clear();
            Series series = new Series("Doanh Thu 7 Ngày");
            series.ChartType = SeriesChartType.SplineArea;  
            series.BorderWidth = 4;
            series.BackGradientStyle = GradientStyle.LeftRight;
            series.BackSecondaryColor = Color.FromArgb(64, 196, 234);
            series.BorderColor = Color.Fuchsia;
            series.XValueType = ChartValueType.DateTime;

            // Thêm dữ liệu từ DataTable vào biểu đồ
            foreach (DataRow row in data.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["NGAYHD"]);
                double doanhThu = Convert.ToDouble(row["TONGDOANHTHU"]);
                DataPoint point = new DataPoint();
                point.SetValueXY(ngay.ToString("dd/MM"), doanhThu);
                point.ToolTip = $"Ngày: {ngay:dd/MM}\nDoanh thu: {doanhThu:N0} VND"; // Hiển thị chi tiết

                series.Points.Add(point);
            }
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0:N0}";
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 5;
            series.MarkerColor = Color.FromArgb(229, 16, 64);
            // Thêm series vào chart
            chart2.Series.Add(series);
            chart2.ChartAreas[0].AxisX.Title = "Ngày";
            chart2.ChartAreas[0].AxisY.Title = "Doanh thu (VND)";
            chart2.Titles.Clear();
            chart2.Titles.Add("Doanh thu trong tuần");
            chart2.Titles[0].Font = new Font("Segoe UI", 15, FontStyle.Bold);
        }

        private void chart2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
