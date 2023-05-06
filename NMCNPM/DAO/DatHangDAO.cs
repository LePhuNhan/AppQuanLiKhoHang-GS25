using NMCNPM_QLKHO.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NMCNPM_QLDATHANG.DAO
{
    public class DatHangDAO
    {
        private static DatHangDAO instance;

        public static DatHangDAO Instance
        {
            get { if (instance == null) instance = new DatHangDAO(); return instance; }
            private set { instance = value; }
        }
        private DatHangDAO() { }
        public void loadList(System.Windows.Forms.ListView ListView)
        {
            string query = "select dh.sanphamID, sp.sanphamName, dh.soluongDat," +
                " sp.gia, sp.NCC from dbo.SANPHAM sp, dbo.DATHANG dh " +
                "where dh.sanphamID=sp.sanphamID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                ListView.Items.Add(item);
            }

        }
        public void loadListSanPham(System.Windows.Forms.ListView ListView)
        {
            string query = "select * from dbo.SANPHAM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                ListView.Items.Add(item);
            }

        }
        public void loadSpecificList(System.Windows.Forms.TextBox text, string sreachValue)
        {
            string query;
            DataTable data = new DataTable();
            query = "select kh.sanphamID,sp.sanphamName,sp.NCC,sanphamBan,sanphamHuy," +
                "sanphamKho,sanphamConLai\r\n\tfrom dbo.KHO kh, dbo.SANPHAM sp\r\n\t" +
                "where kh.sanphamID like '%' + @sanphamID + '%'" +
                " and kh.sanphamID = sp.sanphamID;";
            data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            if (data.Rows.Count>0)
            {
                text.Text = data.Rows[0][1].ToString(); 
            }
            else
            {
                DialogResult result = MessageBox.Show("Không tìm thấy hàng", "Cảnh báo", MessageBoxButtons.OK);
                return;
            }
            
            
        }

        public void ThemSoLuongDat(int soluongDat, int sanphamID)
        {

            string query = "update KHO set sanphamDat += CAST( @soluongDat AS bigint)" +
                " " +
                "where sanphamID = CAST( @sanphamID AS bigint)";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { soluongDat, sanphamID });
            if (data == 0)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm kho hàng", "WARNING");
            }
        }
        
        public void XoaDatHang(int sanphamID)
        {
            string query = "DELETE FROM DATHANG WHERE sanphamID = @sanphamID";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { sanphamID });
            if(data==0)
            {
                MessageBox.Show("Đặt hàng không thành công");

            }
        }
        private void Data_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool DatHang(int sanphamID,int soluongDat)
        {
            string checkExist = "select dh.sanphamID, sp.sanphamName, dh.soluongDat, sp.gia, sp.NCC " +
                "from dbo.SANPHAM sp, dbo.DATHANG dh " +
                "where dh.sanphamID=sp.sanphamID and dh.sanphamID = @sanphamID";
            DataTable tmp = DataProvider.Instance.ExecuteQuery(checkExist, new object[] { sanphamID });
            if (tmp.Rows.Count > 0)
            {
                MessageBox.Show("Không thể thêm hàng do đã tồn tại hàng với ID là " + sanphamID);
                return false;
            }
            else
            {
                string query = "insert into DATHANG values ( @sanphamID , @soluongDat )";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { sanphamID, soluongDat });
                if (data > 0)
                {
                    MessageBox.Show("Thêm hàng thành công!");

                    return true;
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi thêm hàng");

                    return false;
                }
            }
        }
        public void ChinhSua(int soluongDat, int sanphamID)
        {

            string query = "update DATHANG set soluongDat = @soluongDat where sanphamID = @sanphamID";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { soluongDat, sanphamID });
            if (data > 0)
            {
                MessageBox.Show("Cập nhật số hàng muốn đặt thành công!", "Thành công cập nhật số lượng hàng");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật số lượng hàng", "WARNING");
            }
        }
        public DataTable exportList()
        {
            string query = "SELECT * FROM dbo.DATHANG order by sanphamID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

    }
}
