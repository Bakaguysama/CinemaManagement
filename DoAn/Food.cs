using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DoAn
{
    public partial class Food : Form
    {
        public Food()
        {
            InitializeComponent();
        }

        // Kết nối cơ sở dữ liệu
        private readonly string connectionString = @"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

        // Tải dữ liệu sản phẩm từ bảng SANPHAM
        private void LoadFoodData()
        {
            string query = "SELECT MASP, TENSP, LOAI, GIA FROM SANPHAM";

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

                    // Tùy chỉnh giao diện DataGridView
                    foodDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 49, 61);
                    foodDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    foodDataGridView.RowHeadersVisible = false;
                    foodDataGridView.AllowUserToAddRows = false;
                    foodDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tìm kiếm sản phẩm theo tên
        private void SearchFoodData(string searchTerm)
        {
            string query = "SELECT MASP, TENSP, LOAI, GIA FROM SANPHAM WHERE TENSP LIKE @searchTerm";

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
                    foodDataGridView.DataSource = dataTable;
                    
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lấy MASP từ tên sản phẩm
        private string GetProductIdByName(string productName)
        {
            string query = "SELECT MASP FROM SANPHAM WHERE TENSP = @TenSanPham";
            string productId = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TenSanPham", productName);

                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        productId = result.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return productId;
        }

        // Sự kiện tải dữ liệu khi form load
        private void Food_Load(object sender, EventArgs e)
        {
            LoadFoodData();
        }

        // Sự kiện tìm kiếm sản phẩm
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

        // Sự kiện nhập hàng
        private void BtnNhapHang_Click(object sender, EventArgs e)
        {
            // Kiểm tra Tên sản phẩm
            if (string.IsNullOrWhiteSpace(TenSanPham.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra Loại sản phẩm
            if (Loai.SelectedItem == null)
            {
                MessageBox.Show("Bạn phải chọn loại sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra số lượng
            if (!int.TryParse(SoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra giá sản phẩm
            if (!decimal.TryParse(Gia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá sản phẩm phải là một số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong cơ sở dữ liệu chưa
            string masp = GetProductIdByName(TenSanPham.Text);

            if (string.IsNullOrEmpty(masp))
            {
                // Nếu sản phẩm chưa tồn tại, thêm mới sản phẩm
                string loaiSanPham = Loai.SelectedItem.ToString();

                string insertProductQuery = "INSERT INTO SANPHAM (MASP, TENSP, LOAI, GIA) VALUES (@MASP, @TENSP, @LOAI, @GIA)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand insertCommand = new SqlCommand(insertProductQuery, connection);

                        // Cố định hoặc lấy giá từ giao diện
                        insertCommand.Parameters.AddWithValue("@MASP", GenerateNewProductId());
                        insertCommand.Parameters.AddWithValue("@TENSP", TenSanPham.Text);
                        insertCommand.Parameters.AddWithValue("@LOAI", loaiSanPham);
                        insertCommand.Parameters.AddWithValue("@GIA", gia); // Lấy giá từ ô Gia

                        insertCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Sau khi thêm sản phẩm mới, thực hiện nhập hàng
                masp = GetProductIdByName(TenSanPham.Text); // Lấy lại MASP sau khi đã thêm mới
            }

            // Nếu sản phẩm đã tồn tại hoặc đã được thêm mới, tiếp tục nhập hàng vào bảng NHAPSP
            string query = "INSERT INTO NHAPSP (MANV, MASP, SOLUONG) VALUES (@MANV, @MASP, @SOLUONG)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@MANV", "NV001"); // Cố định hoặc từ input người dùng
                    command.Parameters.AddWithValue("@MASP", masp);
                    command.Parameters.AddWithValue("@SOLUONG", soLuong);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Nhập hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadFoodData();
        }

        private string GenerateNewProductId()
        {
            string newProductId = "SP" + Guid.NewGuid().ToString("N").Substring(0, 3); // Lấy 5 ký tự đầu từ GUID
            return newProductId;
        }





        // Sự kiện cập nhật giá sản phẩm
        private void BtnCapNhatGia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TenSanPhamUpdate.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(GiaUpdate.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là một số hợp lệ và lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string masp = GetProductIdByName(TenSanPhamUpdate.Text);

            if (string.IsNullOrEmpty(masp))
            {
                MessageBox.Show("Sản phẩm không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadFoodData();
        }

        private void foodDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string tenSanPham = foodDataGridView.Rows[e.RowIndex].Cells["TENSP"].Value.ToString();
                string loaiSanPham = foodDataGridView.Rows[e.RowIndex].Cells["LOAI"].Value.ToString();
                decimal giaSanPham = Convert.ToDecimal(foodDataGridView.Rows[e.RowIndex].Cells["GIA"].Value);

                TenSanPhamUpdate.Text = tenSanPham;
                LoaiUpdate.SelectedItem = loaiSanPham; 
                GiaUpdate.Text = giaSanPham.ToString("C"); 
            }
        }

    }
}
