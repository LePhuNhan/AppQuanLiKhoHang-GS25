using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace NMCNPM_QLKHO.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }
        public bool LoginAccountNVVP(string userName, string passWord)
        {
            string query = "USP_Login_AppQuanLy @userName , @passWord , @typeID";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord, 1 });
            return result.Rows.Count == 1;
        }
        public string LoadAccountDisplayname(string userName, string passWord)
        {
            string query = "select TenHienThi from TAIKHOAN where nvTaiKhoan = @userName and MatKhau = @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });
            return result.Rows[0][0].ToString();
        }

    }
}
