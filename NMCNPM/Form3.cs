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

namespace NMCNPM
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
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
    }
}
