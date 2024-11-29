using Avalonia.Controls.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class Movie : Form
    {
        public Movie()
        {
            InitializeComponent();
        }

        private void LoadBangPhim()
        {
            string query = @"SELECT * FROM PHIM"; // Hoặc tùy chỉnh SELECT theo các cột bạn cần
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                {
                    conn.Open();
                    DataTable dataTable = new DataTable();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                    // Thêm dữ liệu vào DataGridView
                    foreach (DataRow row in dataTable.Rows)
                    {
                        guna2DataGridView2.Rows.Add(
                            row["MAPHIM"].ToString(),      // Thay bằng tên cột thực tế trong cơ sở dữ liệu
                            row["TENPHIM"].ToString(),
                            row["THOILUONG"].ToString(),
                            row["THELOAI"].ToString(),
                            row["DAODIEN"].ToString(),
                            row["QUOCGIA"].ToString(),
                            row["NAMPH"].ToString(),
                            row["MOTA"].ToString(),
                            row["TINHTRANG"].ToString(),
                            row["GIANHAP"].ToString()
                        );
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
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Movie_Load(object sender, EventArgs e)
        {
            LoadBangPhim();
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow selectedRow = guna2DataGridView2.Rows[e.RowIndex];

            guna2TextBox_MaPhim.Text = selectedRow.Cells["dgv_MaPhim"].Value.ToString();
            guna2TextBox_TenPhim.Text = selectedRow.Cells["dgv_TenPhim"].Value.ToString();
            guna2TextBox_ThoiLuong.Text = selectedRow.Cells["dgv_ThoiLuong"].Value.ToString();
            guna2TextBox_QuocGia.Text = selectedRow.Cells["dgv_QuocGia"].Value.ToString();
            guna2TextBox_NamPH.Text = selectedRow.Cells["dgv_NamPH"].Value.ToString();
            guna2TextBox_MoTa.Text = selectedRow.Cells["dgv_MoTa"].Value.ToString();
            guna2TextBox_TheLoai.Text = selectedRow.Cells["dgv_TheLoai"].Value.ToString();
            guna2TextBox_DaoDien.Text = selectedRow.Cells["dgv_DaoDien"].Value.ToString();
            guna2TextBox_GiaNhap.Text = selectedRow.Cells["dgv_GiaNhap"].Value.ToString();
            string tinhTrang = selectedRow.Cells["dgv_TinhTrang"].Value?.ToString();
            if (tinhTrang == "Đang chiếu")
            {
                guna2ComboBox_TinhTrang.SelectedIndex = guna2ComboBox_TinhTrang.Items.IndexOf("Đang chiếu");
            }
            else if (tinhTrang == "Ngừng chiếu")
            {
                guna2ComboBox_TinhTrang.SelectedIndex = guna2ComboBox_TinhTrang.Items.IndexOf("Ngừng chiếu");
            }
            else
            {
                guna2ComboBox_TinhTrang.SelectedIndex = -1; // Nếu không hợp lệ, bỏ chọn
            }
        }

        private void guna2ComboBox_TinhTrang_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void guna2ComboBox_TinhTrang_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void guna2TextBox_T_TextChanged(object sender, EventArgs e)
        {
            
        }
        private string GenerateMaPhim()
        {
            string queryMaxMaNV = "SELECT MAX(MAPHIM) FROM PHIM";
            using (SqlConnection connection = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryMaxMaNV, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        return "PH001";
                    }
                    else
                    {
                        string maxMaNV = result.ToString();
                        int numberPart = int.Parse(maxMaNV.Substring(2)) + 1;
                        return $"PH{numberPart:D3}";
                    }
                }
            }
        }


        private void btn_ThemMoi_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_TenPhim.Text == ""
                || guna2TextBox_ThoiLuong.Text == ""
                || guna2TextBox_TheLoai.Text == ""
                || guna2TextBox_DaoDien.Text == ""
                || guna2TextBox_QuocGia.Text == ""
                || guna2TextBox_NamPH.Text == ""
                || guna2TextBox_MoTa.Text == ""
                || guna2ComboBox_TinhTrang.SelectedIndex == -1
                || guna2TextBox_GiaNhap.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!double.TryParse(guna2TextBox_GiaNhap.Text, out double gia) || !int.TryParse(guna2TextBox_ThoiLuong.Text, out int thoiluoung) || !int.TryParse(guna2TextBox_NamPH.Text, out int namph))
            {
                MessageBox.Show("Vui lòng điền chính xác kiểu dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult res = MessageBox.Show("Bạn chắc chắn muốn lưu dữ liệu chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                {
                    conn.Open();
                    if (kiemTraPhim(guna2TextBox_MaPhim.Text, conn))
                    {
                        MessageBox.Show("Đã tồn tại mã phim này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string query = @"INSERT INTO PHIM 
                                        VALUES(@MAPHIM, @TENPHIM, @THOILUONG, @THELOAI, @DAODIEN, @QUOCGIA, @NAMPH, @MOTA, @TINHTRANG, @GIANHAP)";
                    try
                    {
                        using (SqlCommand commandInsert = new SqlCommand(query, conn))
                        {
                            commandInsert.Parameters.Add("@MAPHIM", SqlDbType.Char).Value = GenerateMaPhim();
                            commandInsert.Parameters.Add("@TENPHIM", SqlDbType.NVarChar).Value = guna2TextBox_TenPhim.Text;
                            commandInsert.Parameters.Add("@THOILUONG", SqlDbType.Int).Value = guna2TextBox_ThoiLuong.Text;
                            commandInsert.Parameters.Add("@THELOAI", SqlDbType.NVarChar).Value = guna2TextBox_TheLoai.Text;
                            commandInsert.Parameters.Add("@DAODIEN", SqlDbType.NVarChar).Value = guna2TextBox_DaoDien.Text;
                            commandInsert.Parameters.Add("@QUOCGIA", SqlDbType.NVarChar).Value = guna2TextBox_QuocGia.Text;
                            commandInsert.Parameters.Add("@NAMPH", SqlDbType.Int).Value = guna2TextBox_NamPH.Text;
                            commandInsert.Parameters.Add("@MOTA", SqlDbType.NVarChar).Value = guna2TextBox_MoTa.Text;
                            commandInsert.Parameters.Add("@GIANHAP", SqlDbType.Money).Value = guna2TextBox_GiaNhap.Text;

                            string tinhTrang = guna2ComboBox_TinhTrang.SelectedItem.ToString();
                            commandInsert.Parameters.Add("@TINHTRANG", SqlDbType.NVarChar).Value = tinhTrang;
                            int rowsAffected = commandInsert.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Phim đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearTextBox();
                                LoadBangPhim();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm phim. Vui lòng thử lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                clearTextBox();
                                return;
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

                }
            }
            return;
        }

        private void clearTextBox()
        {
            guna2TextBox_MaPhim.Enabled = true;
            guna2TextBox_MaPhim.Clear();
            guna2TextBox_MaPhim.Enabled = false;
            guna2TextBox_TenPhim.Clear();
            guna2TextBox_ThoiLuong.Clear();
            guna2TextBox_QuocGia.Clear();
            guna2TextBox_NamPH.Clear();
            guna2TextBox_TheLoai.Clear();
            guna2TextBox_DaoDien.Clear();
            guna2TextBox_GiaNhap.Clear();
            guna2ComboBox_TinhTrang.SelectedIndex = -1;
            guna2TextBox_MoTa.Clear();
            guna2DataGridView2.ClearSelection();
            guna2PictureBox1.Image = null;
        }

        private bool kiemTraPhim(string maphim, SqlConnection conn)
        {
            string query = @"SELECT COUNT(MAPHIM) FROM PHIM WHERE MAPHIM = @MAPHIM";
            using (SqlCommand commandCheck = new SqlCommand(query, conn))
            {
                commandCheck.Parameters.AddWithValue("@MAPHIM", maphim);

                int count = (int)commandCheck.ExecuteScalar();

                return count > 0; 
            }
        }

        private void guna2Button_LamMoi_Click(object sender, EventArgs e)
        {
            clearTextBox();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mã phim đã được nhập hay chưa
            if (guna2TextBox_MaPhim.Text == "")
            {
                MessageBox.Show("Vui lòng chọn phim cần cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hộp thoại xác nhận xóa
            DialogResult res = MessageBox.Show(
                "Bạn chắc chắn muốn xóa phim này chứ?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (res == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction(); // Khởi tạo Transaction
                    try
                    {
                        // Câu lệnh xóa trong từng bảng
                        string delCTHDQuery = @"DELETE FROM CTHD_VXP WHERE MAVE IN (SELECT MAVE FROM VEXEMPHIM WHERE MAPHIM = @MAPHIM)";
                        string delSuatChieuQuery = @"DELETE FROM SUATCHIEU WHERE MAPHIM = @MAPHIM";
                        string delVeQuery = @"DELETE FROM VEXEMPHIM WHERE MAPHIM = @MAPHIM";
                        string delPhimQuery = @"DELETE FROM PHIM WHERE MAPHIM = @MAPHIM";
;
                        // Xóa CTHD_VXP
                        using (SqlCommand cmdCTHD = new SqlCommand(delCTHDQuery, conn, transaction))
                        {
                            cmdCTHD.Parameters.AddWithValue("@MAPHIM", guna2TextBox_MaPhim.Text);
                            cmdCTHD.ExecuteNonQuery();
                        }

                        // Xóa SUATCHIEU
                        using (SqlCommand cmdSuatChieu = new SqlCommand(delSuatChieuQuery, conn, transaction))
                        {
                            cmdSuatChieu.Parameters.AddWithValue("@MAPHIM", guna2TextBox_MaPhim.Text);
                            cmdSuatChieu.ExecuteNonQuery();
                        }

                        // Xóa VEXEMPHIM
                        
                        using (SqlCommand cmdVe = new SqlCommand(delVeQuery, conn, transaction))
                        {
                            cmdVe.Parameters.AddWithValue("@MAPHIM", guna2TextBox_MaPhim.Text);
                        }
                        // Xóa phim

                        int rowsAffected = 0;
                        using (SqlCommand cmdVe = new SqlCommand(delPhimQuery, conn, transaction))
                        {
                            cmdVe.Parameters.AddWithValue("@MAPHIM", guna2TextBox_MaPhim.Text);
                            rowsAffected = cmdVe.ExecuteNonQuery();
                        }


                        // Kiểm tra kết quả
                        if (rowsAffected > 0)
                        {
                            transaction.Commit(); // Commit nếu thành công
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearTextBox();
                            LoadBangPhim(); // Tải lại dữ liệu
                        }
                        else
                        {
                            transaction.Rollback(); // Rollback nếu không có gì để xóa
                            MessageBox.Show("Không tìm thấy phim để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback nếu xảy ra lỗi
                        MessageBox.Show("Lỗi khi xóa phim: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_TimPhim_Click(object sender, EventArgs e)
        {
            string searchText = guna2TextBox_TimKiem.Text.ToLower(); // Lấy dữ liệu tìm kiếm

            // HashSet để lưu trữ các kết quả đã hiển thị (tránh trùng lặp)
            HashSet<string> displayedRows = new HashSet<string>();

            // Duyệt qua tất cả các dòng của DataGridView
            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                // Kiểm tra nếu dòng không phải là dòng mới
                if (!row.IsNewRow)
                {
                    // Lấy giá trị của ô trong cột "dgv_TenPhim"
                    var cellValue = row.Cells["dgv_TenPhim"].Value?.ToString().ToLower();

                    // Kiểm tra nếu giá trị này không phải null và khớp với nội dung tìm kiếm
                    if (cellValue != null && cellValue.Contains(searchText))
                    {
                        // Kiểm tra xem kết quả này đã được hiển thị chưa
                        if (!displayedRows.Contains(cellValue))
                        {
                            row.Visible = true; // Hiển thị dòng
                            displayedRows.Add(cellValue); // Thêm vào HashSet để tránh trùng lặp
                        }
                        else
                        {
                            row.Visible = false; // Ẩn dòng trùng lặp
                        }
                    }
                    else
                    {
                        row.Visible = false; // Ẩn dòng nếu không khớp
                    }
                }
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_MaPhim.Text == "")
            {
                MessageBox.Show("Vui lòng chọn phim cần cập nhật!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!double.TryParse(guna2TextBox_GiaNhap.Text, out double gia) || !int.TryParse(guna2TextBox_ThoiLuong.Text, out int thoiluoung) || !int.TryParse(guna2TextBox_NamPH.Text, out int namph))
            {
                MessageBox.Show("Vui lòng điền chính xác kiểu dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (guna2ComboBox_TinhTrang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tình trạng phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult res = MessageBox.Show(
               "Bạn chắc chắn muốn cập nhật phim này chứ?",
               "Xác nhận",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );
            string insertQuery = @"UPDATE PHIM
                                    SET TENPHIM = @TENPHIM,
	                                    THOILUONG = @THOILUONG,
	                                    THELOAI = @THELOAI,
	                                    DAODIEN = @DAODIEN,
	                                    QUOCGIA = @QUOCGIA,
	                                    NAMPH = @NAMPH,
	                                    MOTA = @MOTA,
	                                    TINHTRANG = @TINHTRANG,
	                                    GIANHAP = @GIANHAP
                                    WHERE MAPHIM = @MAPHIM;";

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server =LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                {
                    conn.Open();
                    using (SqlCommand commandInsert = new SqlCommand(insertQuery, conn))
                    {
                        commandInsert.Parameters.Add("@MAPHIM", SqlDbType.Char).Value = guna2TextBox_MaPhim.Text;
                        commandInsert.Parameters.Add("@TENPHIM", SqlDbType.NVarChar).Value = guna2TextBox_TenPhim.Text;
                        commandInsert.Parameters.Add("@THOILUONG", SqlDbType.Int).Value = guna2TextBox_ThoiLuong.Text;
                        commandInsert.Parameters.Add("@THELOAI", SqlDbType.NVarChar).Value = guna2TextBox_TheLoai.Text;
                        commandInsert.Parameters.Add("@DAODIEN", SqlDbType.NVarChar).Value = guna2TextBox_DaoDien.Text;
                        commandInsert.Parameters.Add("@QUOCGIA", SqlDbType.NVarChar).Value = guna2TextBox_QuocGia.Text;
                        commandInsert.Parameters.Add("@NAMPH", SqlDbType.Int).Value = guna2TextBox_NamPH.Text;
                        commandInsert.Parameters.Add("@MOTA", SqlDbType.NVarChar).Value = guna2TextBox_MoTa.Text;
                        commandInsert.Parameters.Add("@GIANHAP", SqlDbType.Money).Value = guna2TextBox_GiaNhap.Text;

                        string tinhTrang = guna2ComboBox_TinhTrang.SelectedItem.ToString();
                        commandInsert.Parameters.Add("@TINHTRANG", SqlDbType.NVarChar).Value = tinhTrang;
                        int rowsAffected = commandInsert.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBangPhim();
                        }
                        else
                        {
                            guna2MessageDialog_Warning.Show("Không thể cập nhật. Vui lòng thử lại.", "Cảnh báo");
                            return;
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
        }

        private void btn_ThemAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Chọn ảnh để tải lên",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Hiển thị ảnh trong PictureBox
                    guna2PictureBox1.Image = Image.FromFile(openFileDialog.FileName);

                    // Đặt kích thước ảnh trong PictureBox
                    guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Stretch ảnh cho vừa với PictureBox
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi khi không thể tải ảnh
                    MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

