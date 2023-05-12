using NMCNPM_QLKHO;
using NMCNPM_QLDOANHTHU.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using Bunifu.UI.WinForms.Helpers.Transitions;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;
using NMCNPM_QLKHO.DAO;
using System.Data.SqlClient;

namespace NMCNPM
{
    public partial class DoangThu : Form
    {

        public DoangThu()
        {
            InitializeComponent();
            loadListView();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = GS25(1); Integrated Security = True");
            SqlDataAdapter ad = new SqlDataAdapter("select ngaythangSold, tongSold from DOANHTHU " +
                "where DATEDIFF(day,ngaythangSold,getdate())<=7", conn);
            System.Data.DataTable dt = new System.Data.DataTable();
            ad.Fill(dt);
            chart1.DataSource = dt;
            chart1.Series["Series1"].XValueMember = "ngaythangSold";
            chart1.Series["Series1"].YValueMembers = "tongSold";
            
        }
        void loadListView()
        {
            listView1.Items.Clear();
            DoanhThuDAO.Instance.loadList(listView1);
            textBox2.Text = 0.ToString();
            foreach (ListViewItem item in listView1.Items)
            {
                int test = int.Parse(textBox2.Text);
                test += int.Parse(item.SubItems[10].Text.ToString());
                textBox2.Text = test.ToString();
            }
            textBox3.Text = 0.ToString();
            foreach (ListViewItem item in listView1.Items)
            {
                int test = int.Parse(textBox3.Text);
                test += int.Parse(item.SubItems[10].Text.ToString());
                textBox3.Text = test.ToString();
            }
            textBox3.Text = 0.ToString();
            foreach (ListViewItem item in listView1.Items)
            {
                int test = int.Parse(textBox3.Text);
                test += int.Parse(item.SubItems[10].Text.ToString());
                textBox3.Text = test.ToString();
            }
            textBox6.Text = 0.ToString();
            foreach (ListViewItem item in listView1.Items)
            {
                int test = int.Parse(textBox6.Text);
                test += int.Parse(item.SubItems[10].Text.ToString());
                textBox6.Text = test.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearInput();
            loadListView();
        }
        void clearInput()
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = listView1.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = System.Windows.Forms.SortOrder.Ascending;
                listView1.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                if (sorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    sorter.Order = System.Windows.Forms.SortOrder.Descending;
                else
                    sorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            listView1.Sort();//hienthi
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            
            if (textBox1.Text != "")
            {
                DoanhThuDAO.Instance.loadSpecificList(listView1, textBox1.Text);
            }
            
            else
            {
                DialogResult result = MessageBox.Show("Vui lòng nhập mã hóa đơn muốn tìm", "Thông báo", MessageBoxButtons.OK);
                button3_Click(sender, e);
            }
        }
        private void ExportToExcel(ListView lv,string sheetName, string title)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    

                    ws.Name = sheetName;
                    app.Visible = false;
                    Microsoft.Office.Interop.Excel.Range head = ws.get_Range("A1", "L1");
                    head.Interior.ColorIndex = 33;
                    head.MergeCells = true;

                    head.Value2 = title;

                    head.Font.Bold = true;

                    head.Font.Name = "Times New Roman";

                    head.Font.Size = "20";

                    head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    // Tạo tiêu đề cột 

                    Microsoft.Office.Interop.Excel.Range cl1 = ws.get_Range("A3", "A3");

                    cl1.Value2 = "Mã hóa đơn";

                    cl1.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range cl2 = ws.get_Range("B3", "B3");

                    cl2.Value2 = "Họ tên đệm nhân viên";

                    cl2.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl3 = ws.get_Range("C3", "C3");

                    cl3.Value2 = "Tên nhân viên";
                    cl3.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range cl4 = ws.get_Range("D3", "D3");

                    cl4.Value2 = "Ngày tháng năm";

                    cl4.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl5 = ws.get_Range("E3", "E3");

                    cl5.Value2 = "Giờ";

                    cl5.ColumnWidth = 12.0;

                    Microsoft.Office.Interop.Excel.Range cl6 = ws.get_Range("F3", "F3");

                    cl6.Value2 = "Mã sản phẩm";

                    cl6.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range cl7 = ws.get_Range("G3", "G3");

                    cl7.Value2 = "Tên sản phẩm";

                    cl7.ColumnWidth = 25.0;

                    Microsoft.Office.Interop.Excel.Range cl8 = ws.get_Range("H3", "H3");

                    cl8.Value2 = "Số lượng";

                    cl8.ColumnWidth = 10.0;

                    Microsoft.Office.Interop.Excel.Range cl9 = ws.get_Range("I3", "I3");

                    cl9.Value2 = "Giá tiền";

                    cl9.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range c20 = ws.get_Range("J3", "J3");

                    c20.Value2 = "Chiết khấu";

                    c20.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range c21 = ws.get_Range("K3", "K3");

                    c21.Value2 = "Thành tiền";

                    c21.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range c22 = ws.get_Range("L3", "L3");

                    c22.Value2 = "Hình thức thanh toán";

                    c22.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range rowHead = ws.get_Range("A3", "L3");

                    rowHead.Font.Bold = true;

                    // Kẻ viền

                    rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

                    // Thiết lập màu nền

                    rowHead.Interior.ColorIndex = 33;

                    rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    
                    int i = 4;
                    foreach (ListViewItem item in lv.Items)
                    {
                        for (int k = 1; k <= item.SubItems.Count; k++)
                        {
                            ws.Cells[i, k] = item.SubItems[k - 1].Text;
                            
                        }
                        i++;
                    }
                    int rowStart = 4;

                    int columnStart = 1;

                    int rowEnd = rowStart + listView1.Items.Count - 1;

                    int columnEnd = 12;

                    // Ô bắt đầu điền dữ liệu

                    Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[rowStart, columnStart];

                    // Ô kết thúc điền dữ liệu

                    Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[rowEnd, columnEnd];

                    // Lấy về vùng điền dữ liệu

                    Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);


                    // Kẻ viền

                    range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

                    // Căn giữa cột mã nhân viên

                    //Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[rowEnd, columnStart];

                    //Microsoft.Office.Interop.Excel.Range c4 = ws.get_Range(c1, c3);

                    //Căn giữa cả bảng 
                    ws.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(c1, c2).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(c1, c2).WrapText = false;
                    wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Exported Successfully.");
                    
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //lỗi-không hiện thông báo
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xuất file Excel không", "Cảnh báo", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                System.Data.DataTable dataTable = DoanhThuDAO.Instance.exportList();
                DateTime curr = DateTime.Now;
                string sheetName = "Doanh Thu";
                string title = "Danh sách Doanh Thu ngày " + curr.ToString("dd-MM-yyyy");
                //ExportFile(dataTable, sheetName, title);

                ExportToExcel(listView1, sheetName, title);
            }
            else
                return;
            
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 )
            {
                textBox1.Text = listView1.FocusedItem.SubItems[0].Text;
                //string date = listView1.FocusedItem.SubItems[3].Text;
                dateTimePicker1.Value = DateTime.Parse(listView1.FocusedItem.SubItems[3].Text);
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = "0";
                textBox6.Text = "0";

                if (listView1.FocusedItem.SubItems[11].Text.ToString() == "Tiền Mặt")
                {
                    textBox2.Text = listView1.FocusedItem.SubItems[10].Text;
                }

                if (listView1.FocusedItem.SubItems[11].Text == "BANKING")
                {
                    textBox3.Text = listView1.FocusedItem.SubItems[10].Text;
                }

                if (listView1.FocusedItem.SubItems[11].Text == "E-CASH")
                {
                    textBox4.Text = listView1.FocusedItem.SubItems[10].Text;
                }

                textBox6.Text = listView1.FocusedItem.SubItems[10].Text;
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if(textBox1.Text != "")
            {
                textBox1.Text = "";
            }
            if (textBox1.Text == "")
            {
                string date1 = dateTimePicker1.Value.ToString();
                DoanhThuDAO.Instance.loadDatetime(listView1, dateTimePicker1.Value.ToString());
            }
        }
    }
}
