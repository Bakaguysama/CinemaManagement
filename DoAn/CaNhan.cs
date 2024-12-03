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
    public partial class CaNhan : Form
    {
        private SqlConnection conn;
        public CaNhan()
        {
            InitializeComponent();
        }

        private void guna2Separator3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox_HoTen.Text == "")
            {
                lbl_HoTenChecked.Visible = true;
            }
            else
                lbl_HoTenChecked.Visible = false;
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox_SDT.Text == "")
            {
                lbl_SDTChecked.Visible = true;
            }
            else
                lbl_SDTChecked.Visible = false;
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Nam.Checked)
            {
                radioButton_Nu.Checked = false;
                lbl_GioiTinhChecked.Visible = false;
            }
            else
            {
                radioButton_Nu.Checked = true;
                lbl_GioiTinhChecked.Visible = false;
            }
        }

        private void radioButton_Nu_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Nu.Checked)
            {
                radioButton_Nam.Checked = false;
                lbl_GioiTinhChecked.Visible = false;
            }
            else
            {
                radioButton_Nam.Checked = true;
                lbl_GioiTinhChecked.Visible = false;
            }
        }

        private void guna2Button_SuaMatKhau_Click(object sender, EventArgs e)
        {
            ChinhSuaMatKhau frm = new ChinhSuaMatKhau();
            frm.Show();
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

        private void ConnectToDatabase()
        {
            string connectionString = @"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True";
            conn = new SqlConnection(connectionString);
            conn.Open(); // Mở kết nối
        }

        private void CaNhan_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();
            hienthiThongTin();
        }

        private void hienthiThongTin()
        {
            string userName = LogInForm.GlobalVariables.userName;
            string query = @"SELECT 
	                            tk.MANV,
	                            HOTEN,
	                            NGAYVL,
	                            SDT,
	                            GIOITINH,
	                            CHUCVU
                            FROM TAIKHOAN tk
                            JOIN NHANVIEN nv ON tk.MANV = nv.MANV
                            WHERE TENTK = @userName";
            DataTable dt = LoadDataWithParameters(query, new SqlParameter("@userName", userName));
            if (dt.Rows.Count > 0)
            {
                guna2TextBox_MaNV.Text = dt.Rows[0]["MANV"] == DBNull.Value || dt.Rows[0]["MANV"] == null
                                        ? "N/A"
                                        : dt.Rows[0]["MANV"].ToString();
                guna2TextBox_HoTen.Text = dt.Rows[0]["HOTEN"] == DBNull.Value || dt.Rows[0]["HOTEN"] == null
                                        ? "N/A"  
                                        : dt.Rows[0]["HOTEN"].ToString();
                guna2TextBox_NgayVL.Text = dt.Rows[0]["NGAYVL"] == DBNull.Value || dt.Rows[0]["NGAYVL"] == null
                                        ? "N/A"
                                        : DateTime.Parse(dt.Rows[0]["NGAYVL"].ToString()).ToString("dd/MM/yyyy");

                // Kiểm tra và gán giá trị cho TextBox SDT
                guna2TextBox_SDT.Text = dt.Rows[0]["SDT"] == DBNull.Value || dt.Rows[0]["SDT"] == null
                    ? "N/A"
                    : dt.Rows[0]["SDT"].ToString();

                // Kiểm tra và xử lý giá trị giới tính
                string gioitinh = dt.Rows[0]["GIOITINH"] == DBNull.Value || dt.Rows[0]["GIOITINH"] == null
                    ? "Không xác định"
                    : dt.Rows[0]["GIOITINH"].ToString();

                if (gioitinh == "Nam")
                {
                    radioButton_Nam.Checked = true;
                    radioButton_Nu.Checked = false;
                }
                else if (gioitinh == "Nữ")
                {
                    radioButton_Nam.Checked = false;
                    radioButton_Nu.Checked = true;
                }
                else
                {
                    // Trường hợp không xác định giới tính
                    radioButton_Nam.Checked = false;
                    radioButton_Nu.Checked = false;
                }

                // Kiểm tra và gán giá trị cho TextBox Chức Vụ
                guna2TextBox_ChucVu.Text = dt.Rows[0]["CHUCVU"] == DBNull.Value || dt.Rows[0]["CHUCVU"] == null
                    ? "N/A"
                    : dt.Rows[0]["CHUCVU"].ToString();

            }
            else
            {
                guna2TextBox_MaNV.Text = "N/A";
                guna2TextBox_HoTen.Text = "N/A";
                guna2TextBox_NgayVL.Text = "N/A";
                guna2TextBox_SDT.Text = "N/A";
                guna2TextBox_ChucVu.Text = "N/A";
                radioButton_Nam.Checked = false;
                radioButton_Nu.Checked = false;
            }

        }
     
        private void InsertData(string value1, int value2)
        {
                string query = "INSERT INTO YourTableName (Column1, Column2) VALUES (@Value1, @Value2)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Value1", value1);
                cmd.Parameters.AddWithValue("@Value2", value2);
                int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi lệnh
               
        }

        private void guna2Button_LuuThongTin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox_HoTen.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox_SDT.Text) ||
                (!radioButton_Nam.Checked && !radioButton_Nu.Checked))
            {
                guna2MessageDialog_Warning.Show("Vui lòng nhập đầy đủ thông tin bắt buộc", "Cảnh báo");
                return;
            }

            DialogResult res = MessageBox.Show("Bạn đã chắc chắn muốn lưu dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string insertQuery = @"UPDATE NHANVIEN
                               SET HOTEN = @HOTEN, GIOITINH = @GIOITINH, SDT = @SDT
                               WHERE MANV = (SELECT nv.MANV
                                             FROM NHANVIEN nv 
                                             JOIN TAIKHOAN tk ON tk.MANV = nv.MANV
                                             WHERE TENTK = @TENTK)";

                try
                {
                    using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-89L8K8TI\HUYVU;Database=CINEMAMANAGEMENT;Trusted_Connection=True"))
                    {
                        conn.Open();
                        using (SqlCommand commandInsert = new SqlCommand(insertQuery, conn))
                        {
                            commandInsert.Parameters.Add("@TENTK", SqlDbType.VarChar).Value = LogInForm.GlobalVariables.userName;
                            commandInsert.Parameters.Add("@HOTEN", SqlDbType.NVarChar).Value = guna2TextBox_HoTen.Text;
                            commandInsert.Parameters.Add("@SDT", SqlDbType.VarChar).Value = guna2TextBox_SDT.Text;

                            string GIOITINH = radioButton_Nam.Checked ? "Nam" : "Nữ";
                            commandInsert.Parameters.Add("@GIOITINH", SqlDbType.NVarChar).Value = GIOITINH.Trim();  // Bỏ dấu nháy đơn

                            int rowsAffected = commandInsert.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Lưu thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                hienthiThongTin();
                            }
                            else
                            {
                                guna2MessageDialog_Warning.Show("Không thể lưu thông tin. Vui lòng thử lại.", "Cảnh báo");
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
        }

        private void guna2Button_XoaTK_Click(object sender, EventArgs e)
        {
            XoaTK xoaTK = new XoaTK();
            xoaTK.Show();
        }
    }
}

