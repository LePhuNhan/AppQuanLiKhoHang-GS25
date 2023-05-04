﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NMCNPM_QLKHO.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }
        //LAPTOP-G0K1DP8U\KHANHMINHSQL 
        private string connectionSTR = "Data Source=.\\SQLEXPRESS;Initial Catalog = GS25(1); Integrated Security = True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@username", id);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            //command.Parameters.Clear();
                            command.Parameters.AddWithValue(item, parameter[i]);

                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@username", id);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            //command.Parameters.Clear();
                            command.Parameters.AddWithValue(item, parameter[i]);

                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }
            return data;
        }
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@username", id);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            //command.Parameters.Clear();
                            command.Parameters.AddWithValue(item, parameter[i]);

                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }
            return data;
        }
    }
}
