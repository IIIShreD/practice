using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace кредитная_оганизация
{
    public partial class График : Form
    {
        public int id;
        public int Pointid;
        public string SalePoint = "";
        public int a = 0;
        DB DB = new DB();
        public График()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Обновить_график f = new Обновить_график();
            f.id = id;
            f.idPoint = Pointid;
            f.SalePoint = SalePoint;
            f.Show();
            this.Hide();
        }

        //создание столбцов в dataGridView
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ScheduleID", "ScheduleID");
            dataGridView1.Columns.Add("EmployeeId", "EmployeeId");
            dataGridView1.Columns.Add("SalePointId", "SalePointId");
            dataGridView1.Columns.Add("WorkTime", "WorkTime");
            dataGridView1.Columns.Add("Workdate", "Workdate");
        }

        //функция добавления строк в dataGridView
        private void ReadSinglRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), record.GetString(4));
        }
        //обновление отображаемых данных
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string status = $"select * from EmployeeSchedule where EmployeeId = {id};";
            SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
            DB.OpenConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ReadSinglRow(dgw, reader);
            }
            reader.Close();
        }

        private void График_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void График_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
            string value = "";
            string value1 = "";
            string status = $"select * from Employees where EmployeeID = {id};";
            SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
            DB.OpenConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    value = reader["FullName"].ToString();
                    value1 = reader["Position"].ToString();
                }
            }
            textBox1.Text = value;
            textBox2.Text = value1;
            if (a == 1) button1.Visible = false;
        }
    }
}
