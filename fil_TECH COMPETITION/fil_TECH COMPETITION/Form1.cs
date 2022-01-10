using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fil_TECH_COMPETITION
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Andrii Fil";
            label2.Text = "Tech Competition 2021-22";
            this.form_pnl.Controls.Clear();
            Form_info info = new Form_info { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            info.FormBorderStyle = FormBorderStyle.None;
            this.form_pnl.Controls.Add(info);
            info.Show();
            Name_label.Text = "Math Tools";

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            lbl_X.ForeColor = Color.Red;
        }

        private void lbl_X_MouseLeave(object sender, EventArgs e)
        {
            lbl_X.ForeColor = Color.White;
        }

        Point lastpoint;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void bt_prob_Click(object sender, EventArgs e)
        {
            this.form_pnl.Controls.Clear();
            Probability form_prob = new Probability { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form_prob.FormBorderStyle = FormBorderStyle.None;
            this.form_pnl.Controls.Add(form_prob);
            form_prob.Show();
            Name_label.Text = "Probability";
        }

        private void bt_quadr_Click(object sender, EventArgs e)
        {
            this.form_pnl.Controls.Clear();
            Frm_quadr form_quadr = new Frm_quadr { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form_quadr.FormBorderStyle = FormBorderStyle.None;
            this.form_pnl.Controls.Add(form_quadr);
            form_quadr.Show();
            Name_label.Text = "Quadratic";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.form_pnl.Controls.Clear();
            Form_graphics form_gr = new Form_graphics { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form_gr.FormBorderStyle = FormBorderStyle.None;
            this.form_pnl.Controls.Add(form_gr);
            form_gr.Show();
            Name_label.Text = "Graphics";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.form_pnl.Controls.Clear();
            Form_info info = new Form_info { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            info.FormBorderStyle = FormBorderStyle.None;
            this.form_pnl.Controls.Add(info);
            info.Show();
            Name_label.Text = "Math Tools";
        }
    }
}
