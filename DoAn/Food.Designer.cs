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
            this.Loai = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SoLuong = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TenSanPham = new System.Windows.Forms.TextBox();
            this.NhapHang = new Guna.UI2.WinForms.Guna2Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Gia = new System.Windows.Forms.TextBox();
            this.DanhSachSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foodDataGridView)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // DanhSachSanPham
            // 
            this.DanhSachSanPham.BackColor = System.Drawing.Color.DarkGray;
            this.DanhSachSanPham.Controls.Add(this.foodDataGridView);
            this.DanhSachSanPham.Controls.Add(this.TimSanPham);
            this.DanhSachSanPham.Location = new System.Drawing.Point(23, 25);
            this.DanhSachSanPham.Name = "DanhSachSanPham";
            this.DanhSachSanPham.Size = new System.Drawing.Size(530, 577);
            this.DanhSachSanPham.TabIndex = 2;
            // 
            // foodDataGridView
            // 
            this.foodDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.foodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.foodDataGridView.Location = new System.Drawing.Point(23, 79);
            this.foodDataGridView.Name = "foodDataGridView";
            this.foodDataGridView.RowHeadersWidth = 51;
            this.foodDataGridView.RowTemplate.Height = 24;
            this.foodDataGridView.Size = new System.Drawing.Size(485, 470);
            this.foodDataGridView.TabIndex = 1;
            this.foodDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.foodDataGridView_CellClick);
            // 
            // TimSanPham
            // 
            this.TimSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.TimSanPham.Location = new System.Drawing.Point(23, 21);
            this.TimSanPham.Name = "TimSanPham";
            this.TimSanPham.Size = new System.Drawing.Size(485, 34);
            this.TimSanPham.TabIndex = 0;
            this.TimSanPham.TextChanged += new System.EventHandler(this.TimSanPham_TextChanged);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.Controls.Add(this.label5);
            this.panel12.Controls.Add(this.GiaUpdate);
            this.panel12.Controls.Add(this.LoaiUpdate);
            this.panel12.Controls.Add(this.label4);
            this.panel12.Controls.Add(this.label3);
            this.panel12.Controls.Add(this.TenSanPhamUpdate);
            this.panel12.Controls.Add(this.guna2Button1);
            this.panel12.Controls.Add(this.label1);
            this.panel12.Location = new System.Drawing.Point(559, 321);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(223, 281);
            this.panel12.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(27, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 53;
            this.label5.Text = "Giá Mới";
            // 
            // GiaUpdate
            // 
            this.GiaUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GiaUpdate.Location = new System.Drawing.Point(110, 148);
            this.GiaUpdate.Name = "GiaUpdate";
            this.GiaUpdate.Size = new System.Drawing.Size(100, 22);
            this.GiaUpdate.TabIndex = 54;
            // 
            // LoaiUpdate
            // 
            this.LoaiUpdate.FormattingEnabled = true;
            this.LoaiUpdate.Items.AddRange(new object[] {
            "Thức ăn",
            "Thức uống"});
            this.LoaiUpdate.Location = new System.Drawing.Point(110, 108);
            this.LoaiUpdate.Name = "LoaiUpdate";
            this.LoaiUpdate.Size = new System.Drawing.Size(100, 24);
            this.LoaiUpdate.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(48, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 24);
            this.label4.TabIndex = 51;
            this.label4.Text = "Loại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(8, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 24);
            this.label3.TabIndex = 44;
            this.label3.Text = "Sản Phẩm";
            // 
            // TenSanPhamUpdate
            // 
            this.TenSanPhamUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TenSanPhamUpdate.Location = new System.Drawing.Point(110, 71);
            this.TenSanPhamUpdate.Name = "TenSanPhamUpdate";
            this.TenSanPhamUpdate.Size = new System.Drawing.Size(100, 22);
            this.TenSanPhamUpdate.TabIndex = 45;
            // 
            // guna2Button1
            // 
            this.guna2Button1.Animated = true;
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderRadius = 14;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(20, 207);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(187, 30);
            this.guna2Button1.TabIndex = 40;
            this.guna2Button1.Text = "CẬP NHẬT";
            this.guna2Button1.Click += new System.EventHandler(this.BtnCapNhatGia_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(38, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 28);
            this.label1.TabIndex = 28;
            this.label1.Text = "CẬP NHẬT GIÁ";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.White;
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
            this.panel11.Location = new System.Drawing.Point(559, 25);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(223, 290);
            this.panel11.TabIndex = 5;
            // 
            // Loai
            // 
            this.Loai.FormattingEnabled = true;
            this.Loai.Items.AddRange(new object[] {
            "Thức ăn",
            "Thức uống"});
            this.Loai.Location = new System.Drawing.Point(110, 100);
            this.Loai.Name = "Loai";
            this.Loai.Size = new System.Drawing.Size(100, 24);
            this.Loai.TabIndex = 50;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label14.Location = new System.Drawing.Point(48, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 24);
            this.label14.TabIndex = 49;
            this.label14.Text = "Loại";
            // 
            // SoLuong
            // 
            this.SoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SoLuong.Location = new System.Drawing.Point(110, 142);
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.Size = new System.Drawing.Size(100, 22);
            this.SoLuong.TabIndex = 48;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label23.Location = new System.Drawing.Point(8, 61);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 24);
            this.label23.TabIndex = 41;
            this.label23.Text = "Sản Phẩm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(14, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 24);
            this.label2.TabIndex = 46;
            this.label2.Text = "Số Lượng";
            // 
            // TenSanPham
            // 
            this.TenSanPham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TenSanPham.Location = new System.Drawing.Point(110, 61);
            this.TenSanPham.Name = "TenSanPham";
            this.TenSanPham.Size = new System.Drawing.Size(100, 22);
            this.TenSanPham.TabIndex = 43;
            // 
            // NhapHang
            // 
            this.NhapHang.Animated = true;
            this.NhapHang.AutoRoundedCorners = true;
            this.NhapHang.BackColor = System.Drawing.Color.Transparent;
            this.NhapHang.BorderRadius = 14;
            this.NhapHang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.NhapHang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.NhapHang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.NhapHang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.NhapHang.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(49)))), ((int)(((byte)(61)))));
            this.NhapHang.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.NhapHang.ForeColor = System.Drawing.Color.White;
            this.NhapHang.Location = new System.Drawing.Point(20, 234);
            this.NhapHang.Name = "NhapHang";
            this.NhapHang.Size = new System.Drawing.Size(187, 30);
            this.NhapHang.TabIndex = 40;
            this.NhapHang.Text = "NHẬP HÀNG";
            this.NhapHang.Click += new System.EventHandler(this.BtnNhapHang_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label18.Location = new System.Drawing.Point(41, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(141, 28);
            this.label18.TabIndex = 28;
            this.label18.Text = "NHẬP HÀNG";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(26, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 24);
            this.label7.TabIndex = 51;
            this.label7.Text = "Giá bán";
            // 
            // Gia
            // 
            this.Gia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Gia.Location = new System.Drawing.Point(110, 184);
            this.Gia.Name = "Gia";
            this.Gia.Size = new System.Drawing.Size(100, 22);
            this.Gia.TabIndex = 52;
            // 
            // Food
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 630);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.DanhSachSanPham);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Food";
            this.Text = "Food";
            this.Load += new System.EventHandler(this.Food_Load);
            this.DanhSachSanPham.ResumeLayout(false);
            this.DanhSachSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foodDataGridView)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

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
    }
}