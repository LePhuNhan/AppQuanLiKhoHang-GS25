using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GS25;
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using Bunifu.UI.WinForms.Helpers.Transitions;

namespace NMCNPM
{
    public partial class Form3 : Form
    {
        private Database db;
        public Form3()
        {
            db = new Database();
            InitializeComponent();
        }
        void LoadHoaDon()
        {
            var timKiem = textBox1.Text.Trim();
            var lstPra = new List<CustomParameter>()
            {
                new CustomParameter()
                {
                    key = "@timKiem",
                    value = timKiem
                }
            };

            var dt = db.SelectData("LoadHoaDon", lstPra);

            dataGridView1.DataSource = dt;
            textBox2.Text = 0.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if(dataGridView1.Rows[i].Cells[11].Value.ToString()== "Tiền Mặt")
                {
                    int text = int.Parse(textBox2.Text);
                    text += int.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString());
                    textBox2.Text = text.ToString();
                }
            }
            textBox3.Text = 0.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if(dataGridView1.Rows[i].Cells[11].Value.ToString()== "BANKING")
                {
                    int text = int.Parse(textBox3.Text);
                    text += int.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString());
                    textBox3.Text = text.ToString();
                }
                
            }
            textBox4.Text = 0.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[11].Value.ToString() == "E-CASH")
                {
                    int text = int.Parse(textBox2.Text);
                    text += int.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString());
                    textBox4.Text = text.ToString();
                }
                
            }
            textBox6.Text = 0.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                int text = int.Parse(textBox6.Text);
                text += int.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString());
                textBox6.Text = text.ToString();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            chart1.Series["Series1"].Points.Add(100);
            chart1.Series["Series1"].Points[0].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[0].AxisLabel = "08/04";

            chart1.Series["Series1"].Points.Add(200);
            chart1.Series["Series1"].Points[1].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[1].AxisLabel = "09/04";

            chart1.Series["Series1"].Points.Add(300);
            chart1.Series["Series1"].Points[2].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[2].AxisLabel = "10/04";

            chart1.Series["Series1"].Points.Add(400);
            chart1.Series["Series1"].Points[3].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[3].AxisLabel = "11/04";

            chart1.Series["Series1"].Points.Add(500);
            chart1.Series["Series1"].Points[4].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[4].AxisLabel = "12/04";

            chart1.Series["Series1"].Points.Add(600);
            chart1.Series["Series1"].Points[5].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[5].AxisLabel = "13/04";

            chart1.Series["Series1"].Points.Add(700);
            chart1.Series["Series1"].Points[6].Color = Color.FromArgb(0, 124, 255);
            chart1.Series["Series1"].Points[6].AxisLabel = "14/04";                            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            LoadHoaDon();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
