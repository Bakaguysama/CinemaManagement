using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace DoAn
{
    public partial class Food : Form
    {
        public Food()
        {
            InitializeComponent();
        }

        private void LoadFoodData()
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "SELECT * FROM SANPHAM";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foodDataGridView.DataSource = dataTable;

                    foodDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 49, 61);

                    foodDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foodDataGridView.RowHeadersVisible = false;
                    foodDataGridView.AllowUserToAddRows = false;

                    foodDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Food_Load(object sender, EventArgs e)
        {
            LoadFoodData();
        }

        private void TimSanPham_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = TimSanPham.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadFoodData();
            }
            else
            {
                SearchFoodData(searchTerm);
            }
        }

        private void SearchFoodData(string searchTerm)
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "SELECT * FROM SANPHAM WHERE TenSanPham LIKE @searchTerm";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        foodDataGridView.DataSource = dataTable;
                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetProductIdByName(string productName)
        {
            string productId = string.Empty;

            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "SELECT MASP FROM SANPHAM WHERE TENSP = @TENSP";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TENSP", productName);

                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    productId = result.ToString();
                }
                else
                {
                    productId = string.Empty; 
                }
            }

            return productId;
        }


        private void BtnNhapHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TenSanPham.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Loai.SelectedItem == null)
            {
                MessageBox.Show("Bạn phải chọn loại sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int soLuong;
            if (!int.TryParse(SoLuong.Text, out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "INSERT INTO NHAPSP (MASP, LOAI, SOLUONG) VALUES (@MASP, @LOAI, @SOLUONG)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@MASP", GetProductIdByName(TenSanPham.Text));
                    command.Parameters.AddWithValue("@LOAI", Loai.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@SOLUONG", soLuong);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Nhập hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnCapNhatGia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TenSanPhamUpdate.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal gia;
            if (!decimal.TryParse(GiaUpdate.Text, out gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là một số hợp lệ và lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string masp = GetProductIdByName(TenSanPhamUpdate.Text);
            if (masp == null)
            {
                MessageBox.Show("Sản phẩm không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            string query = "UPDATE SANPHAM SET GIA = @GIA WHERE MASP = @MASP";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@GIA", gia);
                    command.Parameters.AddWithValue("@MASP", masp);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật giá thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void foodDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void foodDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
