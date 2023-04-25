using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Tên quản lý")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.FromArgb(0, 124, 255);
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Tên quản lý";
                textBox2.ForeColor = Color.FromArgb(0, 212, 234);
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Mật khẩu")
            {
                textBox3.UseSystemPasswordChar = true;
                button2.BackgroundImage = Properties.Resources.hidden;
                textBox3.Text = "";
                textBox3.ForeColor = Color.FromArgb(0, 124, 255);
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.UseSystemPasswordChar = false;
                button2.BackgroundImage = Properties.Resources.view;
                textBox3.Text = "Mật khẩu";
                textBox3.ForeColor = Color.FromArgb(0, 212, 234);
            }
        }

        bool check = true;
        private void button2_Click(object sender, EventArgs e)
        {
            if (check)
            {
                textBox3.UseSystemPasswordChar = false;
                button2.BackgroundImage = Properties.Resources.view;
                check = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
                button2.BackgroundImage = Properties.Resources.hidden;
                check = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Close();
        }
    }
}
