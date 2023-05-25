using NMCNPM_QLKHO;
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
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using Bunifu.UI.WinForms.Helpers.Transitions;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;
using NMCNPM_QLDOANHTHU.DAO;
using NMCNPM_QLDATHANG.DAO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NMCNPM
{
    public partial class Kho : Form
    {
        public Kho()
        {
            InitializeComponent();
            loadListView();
        }
        void loadListView()
        {
            listView1.Items.Clear();
            KhoDAO.Instance.UpdateSoLuongConLai();
            KhoDAO.Instance.loadList(listView1);
            dateTimePicker1.Value = DateTime.Now;
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
                textBox1.Text = listView1.FocusedItem.SubItems[0].Text.ToString();
                //string date = listView1.FocusedItem.SubItems[3].Text.ToString();
                //dateTimePicker1.Value = DateTime.Parse(date);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (textBox1.Text != "")
            {
                KhoDAO.Instance.loadSpecificList(listView1, textBox1.Text);
            }
            else
            {
                DialogResult result = MessageBox.Show("Vui lòng nhập mã sản phẩm muốn tìm", "Thông báo", MessageBoxButtons.OK);
                button3_Click(sender, e);
            }
        }
        private void ExportToExcel(System.Windows.Forms.ListView lv, string sheetName, string title)
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
                    Microsoft.Office.Interop.Excel.Range head = ws.get_Range("A1", "H1");
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

                    cl3.Value2 = "Nhà cung cấp";
                    cl3.ColumnWidth = 15.0;

                    Microsoft.Office.Interop.Excel.Range cl4 = ws.get_Range("D3", "D3");

                    cl4.Value2 = "Số lượng bán được";

                    cl4.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl5 = ws.get_Range("E3", "E3");

                    cl5.Value2 = "Số lượng đã đặt";

                    cl5.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl6 = ws.get_Range("F3", "F3");

                    cl6.Value2 = "Số lượng đã hủy";

                    cl6.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl7 = ws.get_Range("G3", "G3");

                    cl7.Value2 = "Số lượng trong kho";

                    cl7.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range cl8 = ws.get_Range("H3", "H3");

                    cl8.Value2 = "Số lượng còn lại";

                    cl8.ColumnWidth = 20.0;

                    Microsoft.Office.Interop.Excel.Range rowHead = ws.get_Range("A3", "H3");

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

                    int columnEnd = 8;

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
                System.Data.DataTable dataTable = KhoDAO.Instance.exportList();
                if (dateTimePicker1.Value > DateTime.Now)
                {
                    DialogResult error = MessageBox.Show("Lỗi chọn thời gian", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DateTime curr = dateTimePicker1.Value;
                string sheetName = "Kho Hàng";
                string title = "Danh sách Kho Hàng ngày " + curr.ToString("dd-MM-yyyy");
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
                KhoDAO.Instance.ThemSoLuongConLai(int.Parse(listView1.Items[i].SubItems[4].Text.ToString()),int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
                KhoDAO.Instance.XoaDatHang(int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
                KhoDAO.Instance.GiamSoLuongConLai(int.Parse(listView1.Items[i].SubItems[5].Text.ToString()), int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
                KhoDAO.Instance.XoaHuyHang(int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
                KhoDAO.Instance.XoaBanHang(int.Parse(listView1.Items[i].SubItems[0].Text.ToString()));
                KhoDAO.Instance.UpdateSoLuongKho();
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số!", "Thông báo - GS25", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
