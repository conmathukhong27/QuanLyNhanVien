using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Windows;

namespace QuanLyNhanVien
{
    class DAO
    {
        public static SqlConnection conn;
        public static string connectionString = "Data Source=welcome-pc\\sqlexpress;Initial Catalog=QuanLyNhanVien;Integrated Security=True";
        public static void OpenConnection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            if (conn.State == System.Data.ConnectionState.Closed)
                try
                {
                    conn.Open();
                    //MessageBox.Show("Mo ket noi thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
        }

        public static void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                try
                {
                    conn.Close();
                    //MessageBox.Show("Dong ket noi thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
        }

        public static bool CheckKeyExit(string sql)
        {
            bool kq = false;

            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);
            if (tbl.Rows.Count > 0)
            {
                kq = true;
            }
            return kq;
        }
        public static string GetFieldValues(string sql)
        {
            DAO.OpenConnection();
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, DAO.conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }

    }
}
