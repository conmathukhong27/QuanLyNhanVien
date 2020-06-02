using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyNhanVien
{
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            LoadDataToGridView();   
            DAO.CloseConnection();

        }
        private void LoadDataToGridView()
        {
            string sql = "select * from tblNhanvien";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.conn);
            DataTable tblNhanvien = new DataTable();
            adapter.Fill(tblNhanvien);
            GridViewNhanvien.DataSource = tblNhanvien;
        }

        private void GridViewNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtManhanvien.Text = GridViewNhanvien.CurrentRow.Cells["MaNV"].Value.ToString();
            txtHoten.Text = GridViewNhanvien.CurrentRow.Cells["Hoten"].Value.ToString();
            txtQuequan.Text = GridViewNhanvien.CurrentRow.Cells["Quequan"].Value.ToString();
            txtManhanvien.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiem tra DL
            //Các trường không được trống
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Mã nhân viên!");
                txtManhanvien.Focus();
                return;
            }
            if (txtHoten.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Họ và tên!");
                txtHoten.Focus();
                return;
            }
            if (txtQuequan.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Quê quán!");
                txtQuequan.Focus();
                return;
            }
            
            //
            string sql = "select * from tblNhanvien where MaNV='" + txtManhanvien.Text.Trim() + "'";
            DAO.OpenConnection();
            if (DAO.CheckKeyExit(sql))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DAO.CloseConnection();
                txtManhanvien.Focus();
                return;
            }
            else
            {
                sql = "insert into tblNhanvien (MaNV,Hoten,Quequan) " +
                    " values ('" + txtManhanvien.Text.Trim() + "',N'" + txtHoten.Text.Trim() + "',N'" +  txtQuequan.Text.Trim()  +"')";
                SqlCommand cmd = new SqlCommand(sql, DAO.conn);
                cmd.ExecuteNonQuery();
                DAO.CloseConnection();
                LoadDataToGridView();
            }


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string sql = "delete from tblNhanvien where MaNV = '" + txtManhanvien.Text + "'";
                DAO.OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = DAO.conn;
                cmd.ExecuteNonQuery();
                DAO.CloseConnection();
                LoadDataToGridView();
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtManhanvien.Text = "";
            txtHoten.Text = "";
            txtQuequan.Text = "";

            

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
