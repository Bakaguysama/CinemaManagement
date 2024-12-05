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
    public partial class XoaTK : Form
    {
        public XoaTK()
        {
            InitializeComponent();
        }

        private void guna2ToggleSwitch_signupForm_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            guna2TextBox_MK.UseSystemPasswordChar = false;
            if (guna2ToggleSwitch_signupForm_ShowPassword.Checked)
            {
                guna2TextBox_MK.PasswordChar = '\0';
            }
            else
            {
                guna2TextBox_MK.PasswordChar = '●';
            }
        }

        private void guna2Button_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button_XacNhan_Click(object sender, EventArgs e)
        {
            if (guna2TextBox_MK.Text == "")
            {
                guna2Message_Warning.Show("Vui lòng nhập mật khẩu", "Cảnh báo");
                return;
            }
            else if (guna2TextBox_MK.Text != LogInForm.GlobalVariables.passWord)
            {
                guna2Message_Warning.Show("Mật khẩu không chính xác", "Cảnh báo");
                guna2TextBox_MK.Clear();
                return;
            }
            else
            {
                DialogResult res = MessageBox.Show("Bạn đã chắc chắn muốn xóa tài khoản không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                    {
                        conn.Open();
                        string maNV = generateMaNV();
                        SqlTransaction transaction = conn.BeginTransaction(); // Khởi tạo Transaction
                        try
                        {
                            // Câu lệnh xóa trong từng bảng
                            string delNhapSPQuery = @"DELETE FROM NHAPSP WHERE MANV = @MANV";
                            string delTaiKhoanQuery = @"DELETE FROM TAIKHOAN WHERE MANV = @MANV";
                            string delNhanVienQuery = @"DELETE FROM NHANVIEN WHERE MANV = @MANV";

                            // Xóa NHAPSP
                            using (SqlCommand cmdnhaSP = new SqlCommand(delNhapSPQuery, conn, transaction))
                            {
                                cmdnhaSP.Parameters.AddWithValue("@MANV", maNV);
                                cmdnhaSP.ExecuteNonQuery();
                            }

                            // Xóa TAIKHOAN
                            using (SqlCommand cmdTaiKhoan = new SqlCommand(delTaiKhoanQuery, conn, transaction))
                            {
                                cmdTaiKhoan.Parameters.AddWithValue("@MANV", maNV);
                                cmdTaiKhoan.ExecuteNonQuery();
                            }

                            // Xóa NHANVIEN
                            int rowsAffected = 0;
                            using (SqlCommand cmdNHANVIEN = new SqlCommand(delNhanVienQuery, conn, transaction))
                            {
                                cmdNHANVIEN.Parameters.AddWithValue("@MANV", maNV);
                                rowsAffected = cmdNHANVIEN.ExecuteNonQuery();
                            }


                            // Kiểm tra kết quả
                            if (rowsAffected > 0)
                            {
                                transaction.Commit(); // Commit nếu thành công
                                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                                CloseAllFormsAndOpenLogin();
                            }
                            else
                            {
                                transaction.Rollback(); // Rollback nếu không có gì để xóa
                                MessageBox.Show("Không tìm thấy tài khoản để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback nếu xảy ra lỗi
                            MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                return;
            }
        }
        private string generateMaNV()
        {
            string queryMaxMaNV = "SELECT MANV FROM TAIKHOAN WHERE TENTK = @TENTK";
            string maNV = string.Empty; // Khởi tạo giá trị trả về
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string tenTK = LogInForm.GlobalVariables.userName; // Lấy giá trị tên tài khoản

                    using (SqlCommand command = new SqlCommand(queryMaxMaNV, connection))
                    {
                        // Thêm tham số truy vấn
                        command.Parameters.Add("@TENTK", SqlDbType.NVarChar).Value = tenTK;

                        // Thực thi truy vấn và đọc kết quả
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            maNV = result.ToString(); // Lấy MANV từ kết quả
                        }
                        else
                        {
                            // Xử lý khi không có dữ liệu trả về
                            maNV = "MANV không tồn tại";
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

            return maNV; // Trả về kết quả
        }
        private void CloseAllFormsAndOpenLogin()
        {
            // Ẩn tất cả các form
            foreach (Form form in Application.OpenForms)
            {
                form.Hide();
            }

            // Mở lại form đăng nhập
            LogInForm loginForm = new LogInForm();
            loginForm.Show();
        }


    }
}
