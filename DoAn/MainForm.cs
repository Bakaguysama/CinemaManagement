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
            lbl_UserName.Text = "Hello, " + LogInForm.GlobalVariables.userName;
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

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button_CaNhan_Click(object sender, EventArgs e)
        {
            DialogResult res = guna2MessageDialog1.Show("Bạn chắc chắn muốn đăng xuất chứ?", "Xác nhận");
            if (res == DialogResult.Yes)
            {
                this.Close();
                LogInForm logInForm = new LogInForm();
                logInForm.Show();
            }
            else
            {
                return;
            }
        }

        private void guna2Button_CaiDat_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            addControl(new CaNhan());
        }

        private void guna2Button_Phim_Click(object sender, EventArgs e)
        {
            addControl(new Movie());
        }

        private void guna2Button_ThucAnDoUong_Click(object sender, EventArgs e)
        {
            addControl(new Food());

        }

        private void guna2Button_RapChieu_Click(object sender, EventArgs e)
        {
            addControl(new Staff());
        }

        private string getChucVuNV()
        {
            string query = @"SELECT CHUCVU FROM NHANVIEN nv JOIN TAIKHOAN tk ON nv.MANV = tk.MANV WHERE TENTK = @TENTK";
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string chucVu = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string tenTK = LogInForm.GlobalVariables.userName; // Lấy giá trị tên tài khoản

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số truy vấn
                        command.Parameters.Add("@TENTK", SqlDbType.NVarChar).Value = tenTK;

                        // Thực thi truy vấn và đọc kết quả
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            chucVu = result.ToString(); // Lấy MANV từ kết quả
                        }
                        else
                        {
                            // Xử lý khi không có dữ liệu trả về
                            chucVu = "MANV không tồn tại";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return chucVu; // Trả về kết quả
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string chucvu = getChucVuNV();
            if (chucvu.Trim().Equals("Quản Lí", StringComparison.OrdinalIgnoreCase))
            {
                guna2Button_RapChieu.Visible = true;
            }
            else
            {
                guna2Button_RapChieu.Visible = false;
            }
        }

        private void guna2ControlBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ControlBox4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
