﻿using NMCNPM_QLKHO.DAO;
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
    public class KhoDAO
    {
        private static KhoDAO instance;

        public static KhoDAO Instance
        {
            get { if (instance == null) instance = new KhoDAO(); return instance; }
            private set { instance = value; }
        }
        private KhoDAO() { }

        public void loadList(ListView ListView)
        {
            string query = "select kh.sanphamID,sp.sanphamName,sp.NCC,sanphamBan,sanphamHuy,sanphamKho,sanphamConLai" +
                " from dbo.KHO kh, dbo.SANPHAM sp " +
                "where kh.sanphamID = sp.sanphamID; ";
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
        public DataTable exportList()
        {
            string query = "SELECT * FROM dbo.KHO order by sanphamID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        
        public void loadSpecificList(ListView listView, string sreachValue)
        {
            string query;
            DataTable data = new DataTable();
            query = "select kh.sanphamID,sp.sanphamName,sp.NCC,sanphamBan,sanphamHuy," +
                "sanphamKho,sanphamConLai\r\n\tfrom dbo.KHO kh, dbo.SANPHAM sp\r\n\t" +
                "where kh.sanphamID like '%' + @sanphamID + '%'"+
                " and kh.sanphamID = sp.sanphamID;";
            data = DataProvider.Instance.ExecuteQuery(query, new object[] { sreachValue });
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView.Items.Add(item);
            }

        }
        
    }
}
