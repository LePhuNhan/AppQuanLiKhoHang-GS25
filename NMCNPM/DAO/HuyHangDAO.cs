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
namespace NMCNPM_QLHUYHANG.DAO
{
    public class HuyHangDAO
    {
        private static HuyHangDAO instance;

        public static HuyHangDAO Instance
        {
            get { if (instance == null) instance = new HuyHangDAO(); return instance; }
            private set { instance = value; }
        }
        private HuyHangDAO() { }
        public void loadList(System.Windows.Forms.ListView ListView)
        {
            string query = "select hh.sanphamID, sp.sanphamName, hh.soluongHuy," +
                " sp.gia, sp.NCC from dbo.SANPHAM sp, dbo.HUYHANG hh " +
                "where hh.sanphamID=sp.sanphamID";
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
            if (data.Rows.Count > 0)
            {
                text.Text = data.Rows[0][1].ToString();
            }
            else
            {
                DialogResult result = MessageBox.Show("Không tìm thấy hàng", "Cảnh báo", MessageBoxButtons.OK);
                return;
            }


        }
        public void GiamSoLuongKho(int soluongDat, int sanphamID)
        {

            string query = "update KHO set sanphamKho -= CAST( @soluongDat AS bigint)" +
                " " +
                "where sanphamID = CAST( @sanphamID AS bigint)";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { soluongDat, sanphamID });
            if (data == 0)
            {
                MessageBox.Show("Đã xảy ra lỗi khi hủy kho hàng", "WARNING");
            }
        }
        public void GiamSoLuongConLai(int soluongDat, int sanphamID)
        {

            string query = "update KHO set sanphamConLai -= CAST( @soluongDat AS bigint)" +
                " " +
                "where sanphamID = CAST( @sanphamID AS bigint)";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { soluongDat, sanphamID });
            if (data == 0)
            {
                MessageBox.Show("Đã xảy ra lỗi khi hủy kho hàng", "WARNING");
            }
        }
        public void XoaHuyHang(int sanphamID)
        {
            string query = "DELETE FROM HUYHANG WHERE sanphamID = @sanphamID";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { sanphamID });
            if (data == 0)
            {
                MessageBox.Show("Hủy hàng không thành công");

            }
        }
        private void Data_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool HuyHang(int sanphamID, int soluongDat)
        {
            string checkExist = "select hh.sanphamID, sp.sanphamName, hh.soluongHuy, sp.gia, sp.NCC " +
                "from dbo.SANPHAM sp, dbo.HUYHANG hh " +
                "where hh.sanphamID=sp.sanphamID and hh.sanphamID = @sanphamID";
            DataTable tmp = DataProvider.Instance.ExecuteQuery(checkExist, new object[] { sanphamID });
            if (tmp.Rows.Count > 0)
            {
                MessageBox.Show("Không thể hủy hàng do đã tồn tại hàng với ID là " + sanphamID);
                return false;
            }
            else
            {
                string query = "insert into HUYHANG values ( @sanphamID , @soluongDat )";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { sanphamID, soluongDat });
                if (data > 0)
                {
                    MessageBox.Show("Hủy hàng thành công. Vui lòng ấn refresh để cập nhật Danh sách");

                    return true;
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi hủy hàng");

                    return false;
                }
            }
        }
        public void ChinhSua(int soluongDat, int sanphamID)
        {

            string query = "update HUYHANG set soluongHuy = @soluongDat where sanphamID = @sanphamID";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { soluongDat, sanphamID });
            if (data > 0)
            {
                MessageBox.Show("Cập nhật số hàng muốn hủy thành công. Vui lòng ấn refresh để cập nhật Danh sách", "Thành công cập nhật số lượng hàng");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật số lượng hàng", "WARNING");
            }
        }
        public DataTable exportList()
        {
            string query = "SELECT * FROM dbo.HUYHANG order by sanphamID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

    }
}
