using NMCNPM_QLKHO.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMCNPM_QLKHO.DAO
{
    public class DoanhThuDAO
    {
        private static DoanhThuDAO instance;

        public static DoanhThuDAO Instance
        {
            get { if (instance == null) instance = new DoanhThuDAO(); return instance; }
            private set { instance = value; }
        }
        private DoanhThuDAO() { }

        public void loadList(ListView ListView)
        {
            string query = "select hdi.hoadonID, nvHo, nvTen, ngaythangSold, " +
                "gioSold, sp.sanphamID, sanphamName, soluong, gia, chietkhau," +
                " thanhtien, hinhthucThanhToan\r\n\tfrom dbo.HOADON hd,dbo.NHANVIEN " +
                "nv, dbo.SANPHAM sp, dbo.HDINFO hdi\r\n\t" +
                "where hdi.hoadonID=hd.hoadonID\r\n\tand " +
                "hdi.sanphamID=sp.sanphamID\r\n\tand hd.nvID=nv.nvID;";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    if (i == 3)
                    {
                        string tmp = row[i].ToString();
                        if (tmp.Length == 20)
                        {
                            tmp = tmp.Substring(0, 8);
                        }
                        else if (tmp.Length == 21)
                        {
                            tmp = tmp.Substring(0, 9);
                        }
                        else if (tmp.Length == 22)
                        {
                            tmp = tmp.Substring(0, 10);
                        }
                        item.SubItems.Add(tmp);
                    }
                    else
                    {
                        item.SubItems.Add(row[i].ToString());
                    }

                }
                ListView.Items.Add(item);
            }

        }
        public DataTable exportList()
        {
            string query = "SELECT * FROM dbo.HOADON order by hoadonID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        
        public void loadSpecificList(ListView listView, string sreachValue)
        {
            string query;
            DataTable data = new DataTable();
            query = "select hdi.hoadonID, nvHo, nvTen, ngaythangSold, gioSold, " +
                "sp.sanphamID, sanphamName, soluong, gia, chietkhau, thanhtien," +
                " hinhthucThanhToan\r\n\tfrom dbo.HOADON hd,dbo.NHANVIEN nv," +
                " dbo.SANPHAM sp, dbo.HDINFO hdi\r\n\twhere hdi.hoadonID like '%' + @hoadonID + '%'" +
                " and hdi.hoadonID=hd.hoadonID\r\n\tand hdi.sanphamID=sp.sanphamID\r\n\tand hd.nvID=nv.nvID;";
            data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    if (i == 3)
                    {
                        string tmp = row[i].ToString();
                        if (tmp.Length == 20)
                        {
                            tmp = tmp.Substring(0, 8);
                        }
                        else if (tmp.Length == 21)
                        {
                            tmp = tmp.Substring(0, 9);
                        }
                        else if (tmp.Length == 22)
                        {
                            tmp = tmp.Substring(0, 10);
                        }
                        item.SubItems.Add(tmp);
                    }
                    else
                    {
                        item.SubItems.Add(row[i].ToString());
                    }

                }
                listView.Items.Add(item);
            }

        }
        public string selectLastEmployssList()
        {
            string query = "select TOP(1) nvID from NHANVIEN order by nvID DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows[0][0].ToString();
        }
    }

}
