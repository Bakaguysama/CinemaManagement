using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2ProgressBar1.Value == 100)
            {
                guna2HtmlLabel_Loading.Text = "Done!";
                timer1.Stop();
                this.Close();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                guna2ProgressBar1.Value++;
                guna2HtmlLabel_Loading.Text = guna2ProgressBar1.Value.ToString() + "%";

            }
        }



        private void LoadingForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            guna2ShadowForm1.SetShadowForm(this);
        }
    }
}
