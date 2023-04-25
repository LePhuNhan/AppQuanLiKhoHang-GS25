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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();           
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.FlatAppearance.BorderSize = 0;
            OpenChildForm(new Form3());
        }
        bool check1 = false;
        private void button1_Click(object sender, EventArgs e)
        {
            check1 = !check1;
            if (check1)
            {
                panel2.Width = panel2.MinimumSize.Width;
                check2 = false;
                button2.Image = Properties.Resources.statistic_1;
                button6.Location = new Point(2, 97);
                button7.Location = new Point(2, 142);
                panel4.Height = 0;
            }
            else
            {
                panel2.Width = panel2.MaximumSize.Width;
            }
        }
        bool check2 = false;
        private void button2_Click(object sender, EventArgs e)
        {
            check2 = !check2;
            if (check2)
            {
                button2.Image = Properties.Resources.statistic_2;
                button6.Location = new Point(2, 187);
                button7.Location = new Point(2, 232);
                panel4.Height = 90;
            }
            else
            {
                button2.Image = Properties.Resources.statistic_1;
                button6.Location = new Point(2, 97);
                button7.Location = new Point(2, 142);
                panel4.Height = 0;              
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3());
            label1.Text = "Thống kê doanh thu";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form4());
            label1.Text = "Thống kê kho hàng";
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if(currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel7.Controls.Add(childForm);
            panel7.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form5());
            label1.Text = "Đặt hàng";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form6());
            label1.Text = "Hủy hàng";
        }
    }
}
