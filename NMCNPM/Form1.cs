using NMCNPM_QLKHO.DAO;
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
        public static string displayname = "";
        string LoadDisplayName(string userName, string passWord)
        {
            return AccountDAO.Instance.LoadAccountDisplayname(userName, passWord);
        }
        bool LoginAccountNVVP(string userName, string passWord)
        {

            return AccountDAO.Instance.LoginAccountNVVP(userName, passWord);
        }
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
                textBox2.Text = "201209018";
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
                textBox3.Text = "vanphonggs25";
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
            string userName = textBox2.Text;
            string passWord = textBox3.Text;
            if (textBox2.Text == "Tên tài khoản" || textBox3.Text == "Mật khẩu")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu", "Warning");
            }
            else
            {
                if (LoginAccountNVVP(userName, passWord))
                {
                    displayname = LoadDisplayName(userName, passWord);
                    textBox3.Clear();
                    OpenChildForm(new Menu());

                }
                else
                {
                    textBox2.Clear();
                    textBox3.Clear();
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu, Mời bạn nhập lại!!!", "Warning");
                }
            }
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
