namespace DoAn
{
    partial class Food
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DanhSachSanPham = new System.Windows.Forms.Panel();
            this.foodDataGridView = new System.Windows.Forms.DataGridView();
            this.TimSanPham = new System.Windows.Forms.TextBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.GiaUpdate = new System.Windows.Forms.TextBox();
            this.LoaiUpdate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TenSanPhamUpdate = new System.Windows.Forms.TextBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.Gia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Loai = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SoLuong = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TenSanPham = new System.Windows.Forms.TextBox();
            this.NhapHang = new Guna.UI2.WinForms.Guna2Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DanhSachSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foodDataGridView)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DanhSachSanPham
            // 
            this.DanhSachSanPham.BackColor = System.Drawing.Color.DarkGray;
            this.DanhSachSanPham.Controls.Add(this.foodDataGridView);
            this.DanhSachSanPham.Location = new System.Drawing.Point(23, 106);
            this.DanhSachSanPham.Name = "DanhSachSanPham";
            this.DanhSachSanPham.Size = new System.Drawing.Size(1018, 658);
            this.DanhSachSanPham.TabIndex = 2;
            // 
            // foodDataGridView
            // 
            this.foodDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.foodDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.foodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.foodDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.foodDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foodDataGridView.Location = new System.Drawing.Point(0, 0);
            this.foodDataGridView.Name = "foodDataGridView";
            this.foodDataGridView.RowHeadersWidth = 51;
            this.foodDataGridView.RowTemplate.Height = 24;
            this.foodDataGridView.Size = new System.Drawing.Size(1018, 658);
            this.foodDataGridView.TabIndex = 1;
            this.foodDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.foodDataGridView_CellClick);
            // 
            // TimSanPham
            // 
            this.TimSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.TimSanPham.Location = new System.Drawing.Point(811, 62);
            this.TimSanPham.Name = "TimSanPham";
            this.TimSanPham.Size = new System.Drawing.Size(230, 38);
            this.TimSanPham.TabIndex = 0;
            this.TimSanPham.TextChanged += new System.EventHandler(this.TimSanPham_TextChanged);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.label5);
            this.panel12.Controls.Add(this.GiaUpdate);
            this.panel12.Controls.Add(this.LoaiUpdate);
            this.panel12.Controls.Add(this.label4);
            this.panel12.Controls.Add(this.label3);
            this.panel12.Controls.Add(this.TenSanPhamUpdate);
            this.panel12.Controls.Add(this.guna2Button1);
            this.panel12.Controls.Add(this.label1);
            this.panel12.Location = new System.Drawing.Point(1070, 407);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(290, 357);
            this.panel12.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(31, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 28);
            this.label5.TabIndex = 53;
            this.label5.Text = "Giá Mới";
            // 
            // GiaUpdate
            // 
            this.GiaUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GiaUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.GiaUpdate.Location = new System.Drawing.Point(136, 235);
            this.GiaUpdate.Name = "GiaUpdate";
            this.GiaUpdate.Size = new System.Drawing.Size(141, 30);
            this.GiaUpdate.TabIndex = 54;
            // 
            // LoaiUpdate
            // 
            this.LoaiUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LoaiUpdate.FormattingEnabled = true;
            this.LoaiUpdate.Items.AddRange(new object[] {
            "Thức ăn",
            "Thức uống"});
            this.LoaiUpdate.Location = new System.Drawing.Point(136, 165);
            this.LoaiUpdate.Name = "LoaiUpdate";
            this.LoaiUpdate.Size = new System.Drawing.Size(141, 33);
            this.LoaiUpdate.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(63, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 28);
            this.label4.TabIndex = 51;
            this.label4.Text = "Loại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 28);
            this.label3.TabIndex = 44;
            this.label3.Text = "Sản Phẩm";
            // 
            // TenSanPhamUpdate
            // 
            this.TenSanPhamUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TenSanPhamUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TenSanPhamUpdate.Location = new System.Drawing.Point(136, 98);
            this.TenSanPhamUpdate.Name = "TenSanPhamUpdate";
            this.TenSanPhamUpdate.Size = new System.Drawing.Size(141, 30);
            this.TenSanPhamUpdate.TabIndex = 45;
            // 
            // guna2Button1
            // 
            this.guna2Button1.Animated = true;
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderRadius = 21;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(59, 299);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(189, 44);
            this.guna2Button1.TabIndex = 40;
            this.guna2Button1.Text = "CẬP NHẬT";
            this.guna2Button1.Click += new System.EventHandler(this.BtnCapNhatGia_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(53, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 34);
            this.label1.TabIndex = 28;
            this.label1.Text = "CẬP NHẬT GIÁ";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.Gia);
            this.panel11.Controls.Add(this.label7);
            this.panel11.Controls.Add(this.Loai);
            this.panel11.Controls.Add(this.label14);
            this.panel11.Controls.Add(this.SoLuong);
            this.panel11.Controls.Add(this.label23);
            this.panel11.Controls.Add(this.label2);
            this.panel11.Controls.Add(this.TenSanPham);
            this.panel11.Controls.Add(this.NhapHang);
            this.panel11.Controls.Add(this.label18);
            this.panel11.Location = new System.Drawing.Point(1070, 25);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(290, 357);
            this.panel11.TabIndex = 5;
            // 
            // Gia
            // 
            this.Gia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Gia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Gia.Location = new System.Drawing.Point(136, 253);
            this.Gia.Name = "Gia";
            this.Gia.Size = new System.Drawing.Size(141, 30);
            this.Gia.TabIndex = 52;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(30, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 28);
            this.label7.TabIndex = 51;
            this.label7.Text = "Giá bán";
            // 
            // Loai
            // 
            this.Loai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Loai.FormattingEnabled = true;
            this.Loai.Items.AddRange(new object[] {
            "Thức ăn",
            "Thức uống"});
            this.Loai.Location = new System.Drawing.Point(136, 134);
            this.Loai.Name = "Loai";
            this.Loai.Size = new System.Drawing.Size(141, 33);
            this.Loai.TabIndex = 50;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(63, 139);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 28);
            this.label14.TabIndex = 49;
            this.label14.Text = "Loại";
            // 
            // SoLuong
            // 
            this.SoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SoLuong.Location = new System.Drawing.Point(136, 195);
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.Size = new System.Drawing.Size(141, 30);
            this.SoLuong.TabIndex = 48;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label23.Location = new System.Drawing.Point(3, 78);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(116, 28);
            this.label23.TabIndex = 41;
            this.label23.Text = "Sản Phẩm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(9, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 28);
            this.label2.TabIndex = 46;
            this.label2.Text = "Số Lượng";
            // 
            // TenSanPham
            // 
            this.TenSanPham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TenSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenSanPham.Location = new System.Drawing.Point(136, 76);
            this.TenSanPham.Name = "TenSanPham";
            this.TenSanPham.Size = new System.Drawing.Size(141, 30);
            this.TenSanPham.TabIndex = 43;
            // 
            // NhapHang
            // 
            this.NhapHang.Animated = true;
            this.NhapHang.AutoRoundedCorners = true;
            this.NhapHang.BackColor = System.Drawing.Color.Transparent;
            this.NhapHang.BorderRadius = 19;
            this.NhapHang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.NhapHang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.NhapHang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.NhapHang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.NhapHang.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.NhapHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.NhapHang.ForeColor = System.Drawing.Color.White;
            this.NhapHang.Location = new System.Drawing.Point(59, 304);
            this.NhapHang.Name = "NhapHang";
            this.NhapHang.Size = new System.Drawing.Size(189, 40);
            this.NhapHang.TabIndex = 40;
            this.NhapHang.Text = "NHẬP HÀNG";
            this.NhapHang.Click += new System.EventHandler(this.BtnNhapHang_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bahnschrift", 16.2F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(74, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(172, 34);
            this.label18.TabIndex = 28;
            this.label18.Text = "NHẬP HÀNG";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(396, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(332, 36);
            this.label6.TabIndex = 7;
            this.label6.Text = "DANH SÁCH SẢN PHẨM";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::DoAn.Properties.Resources.search_interface_symbol__2_;
            this.pictureBox1.Location = new System.Drawing.Point(743, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // Food
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 799);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TimSanPham);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.DanhSachSanPham);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Food";
            this.Text = "Food";
            this.Load += new System.EventHandler(this.Food_Load);
            this.DanhSachSanPham.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.foodDataGridView)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DanhSachSanPham;
        private System.Windows.Forms.TextBox TimSanPham;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox GiaUpdate;
        private System.Windows.Forms.ComboBox LoaiUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TenSanPhamUpdate;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ComboBox Loai;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox SoLuong;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TenSanPham;
        private Guna.UI2.WinForms.Guna2Button NhapHang;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView foodDataGridView;
        private System.Windows.Forms.TextBox Gia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}