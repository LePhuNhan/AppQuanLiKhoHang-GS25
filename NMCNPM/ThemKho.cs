using System;
using System.Collections.Generic;
using System.ComponentModel;
using NMCNPM_QLKHO;
using NMCNPM_QLDATHANG.DAO;
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
using System.Diagnostics;
using NMCNPM_QLKHO.DAO;

namespace NMCNPM
{
    public partial class ThemKho : Form
    {
        public ThemKho()
        {
            InitializeComponent();
            loadListView();
        }
        void loadListView()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            DatHangDAO.Instance.loadList(listView1);
            DatHangDAO.Instance.loadListSanPham(listView2);
            string time = DateTime.Now.ToString();
            label4.Text=time;
        }
        void clearInput()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Clear();
            string time = DateTime.Now.ToString();
            label4.Text = time;
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = listView1.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                listView1.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                if (sorter.Order == SortOrder.Ascending)
                    sorter.Order = SortOrder.Descending;
                else
                    sorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listView1.Sort();//hienthi
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox2.Text = listView1.FocusedItem.SubItems[0].Text.ToString();
                textBox3.Text = listView1.FocusedItem.SubItems[1].Text.ToString();
                //string date = listView1.FocusedItem.SubItems[3].Text.ToString();
                //dateTimePicker1.Value = DateTime.Parse(date);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                DatHangDAO.Instance.loadSpecificList(textBox3, textBox2.Text);
            }
            else
            {
                DialogResult result = MessageBox.Show("Vui lòng nhập mã sản phẩm muốn đặt", "Thông báo", MessageBoxButtons.OK);
                button4_Click(sender, e);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            clearInput();
            loadListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length <= 5)
            {
                DialogResult res = MessageBox.Show("Vui lòng điền đúng mã hàng muốn đặt", "Cảnh báo", MessageBoxButtons.OK);
            }
            if (textBox1.Text == "")
            {
                DialogResult result = MessageBox.Show("Vui lòng điền số lượng hàng muốn đặt", "Cảnh báo", MessageBoxButtons.OK);
            }
            if (textBox2.Text.Length>5 && textBox3.Text!="" && textBox1.Text != "")
            {
                DatHangDAO.Instance.DatHang(int.Parse(textBox2.Text), int.Parse(textBox1.Text));
                textBox1.Clear();
                clearInput();
                loadListView();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length <= 5)
            {
                DialogResult res = MessageBox.Show("Vui lòng điền đúng mã hàng muốn đổi số lượng", "Cảnh báo", MessageBoxButtons.OK);
            }
            if (textBox1.Text == "")
            {
                DialogResult result = MessageBox.Show("Vui lòng điền số lượng hàng muốn đổi", "Cảnh báo", MessageBoxButtons.OK);
            }
            if (textBox2.Text != "" && textBox3.Text != "" && textBox1.Text != "")
            {
                DatHangDAO.Instance.ChinhSua(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                textBox1.Clear();
                clearInput();
                loadListView();
            }
        }
        private void ExportToExcel(ListView lv, string sheetName, string title)
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
                    Microsoft.Office.Interop.Excel.Range head = ws.get_Range("A1", "E1");
                    head.Interior.ColorIndex = 33;
                    head.MergeCells = true;

                    head.Value2 = title;

                    head.Font.Bold = true;

                    head.Font.Name = "Times New Roman";

                    head.Font.Size = "20";

                    head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    // Tạo tiêu đề cột 

                    Microsoft.Office.Interop.Excel.Range cl1 = ws.get_Range("A3", "A3");

                    cl1.Value2 = "Mã sản phẩm";

                    cl1.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range cl2 = ws.get_Range("B3", "B3");

                    cl2.Value2 = "Tên sản phẩm";

                    cl2.ColumnWidth = 25.0;

                    Microsoft.Office.Interop.Excel.Range cl3 = ws.get_Range("C3", "C3");

                    cl3.Value2 = "Số lượng đặt";
                    cl3.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range cl4 = ws.get_Range("D3", "D3");

                    cl4.Value2 = "Giá mua";

                    cl4.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl5 = ws.get_Range("E3", "E3");

                    cl5.Value2 = "Nhà cung cấp";

                    cl5.ColumnWidth = 20.0;

                   

                    Microsoft.Office.Interop.Excel.Range rowHead = ws.get_Range("A3", "E3");

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

                    int columnEnd = 5;

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
        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xuất file Excel không", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                updateKHO();
                System.Data.DataTable dataTable = DatHangDAO.Instance.exportList();
                DateTime curr = DateTime.Now;
                string sheetName = "Đặt hàng";
                string title = "Danh sách Đặt Hàng ngày " + curr.ToString("dd-MM-yyyy");
                //ExportFile(dataTable, sheetName, title);
                ExportToExcel(listView1, sheetName, title);
                clearInput();
                loadListView();
            }
            else
                return;
            
        }
        void updateKHO()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                DatHangDAO.Instance.ThemSoLuongDat(int.Parse(listView1.Items[i].SubItems[2].Text.ToString()), int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
                DatHangDAO.Instance.XoaDatHang(int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                textBox2.Text = listView2.FocusedItem.SubItems[0].Text.ToString();
                textBox3.Text = listView2.FocusedItem.SubItems[1].Text.ToString();
                //string date = listView1.FocusedItem.SubItems[3].Text.ToString();
                //dateTimePicker1.Value = DateTime.Parse(date);
            }
        }

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = listView1.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                listView1.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                if (sorter.Order == SortOrder.Ascending)
                    sorter.Order = SortOrder.Descending;
                else
                    sorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listView2.Sort();//hienthi
        }
    }
}
