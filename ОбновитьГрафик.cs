using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace кредитная_оганизация
{
    public partial class Обновить_график : Form
    {
        DB DB = new DB();
        public int idPoint = 0;
        public string SalePoint = "";
        public int id = 0;
        int selectedRow;
        public Обновить_график()
        {
            InitializeComponent();
        }

        private void Обновить_график_Load(object sender, EventArgs e)
        {
            label2.Text = SalePoint;
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                id = Convert.ToInt32(row.Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DateTime selectedDate = dateTimePicker1.Value;
                string dateString = selectedDate.ToShortDateString();
                dateString.ToString();

                string stas = $"select Workdate from EmployeeSchedule where Workdate = '{dateString}' and EmployeeId = {id};";
                SqlCommand sqlCommand1 = new SqlCommand(stas, DB.getConnection);
                DB.OpenConnection();
                SqlDataReader reader = sqlCommand1.ExecuteReader();
                if (!reader.HasRows)
                {
                    if (textBox1.Text != "")
                    {
                        int n;
                        bool isNumber = int.TryParse(textBox1.Text, out n);
                        if (isNumber)
                        {
                            if (n <= 12 & n >= 4)
                            {
                                string status = $"insert into EmployeeSchedule (EmployeeId, SalePointId, WorkTime, Workdate) values ('{id}', '{idPoint}', '{n}', '{dateString}');";
                                SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
                                DB.OpenConnection();
                                sqlCommand.ExecuteNonQuery();
                                MessageBox.Show("График обновлен", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            } else MessageBox.Show("Время работы должно быть от 4 до 12 часов", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        } else MessageBox.Show("Время введено не корректно", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else MessageBox.Show("Время работы не выбрано", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else MessageBox.Show("Сотрудник уже работает в этот день", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else MessageBox.Show("Сотрудник не выбран", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DateTime selectedDate = dateTimePicker1.Value;
                string dateString = selectedDate.ToShortDateString();
                dateString.ToString();

                string stas = $"select Workdate from EmployeeSchedule where Workdate = '{dateString}' and EmployeeId = {id};";
                SqlCommand sqlCommand1 = new SqlCommand(stas, DB.getConnection);
                DB.OpenConnection();
                SqlDataReader reader = sqlCommand1.ExecuteReader();
                if (reader.HasRows)
                {
                    string status = $"delete from EmployeeSchedule where EmployeeId = {id} and Workdate = '{dateString}';";
                    SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
                    DB.OpenConnection();
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("График обновлен", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }  else MessageBox.Show("Сотрудник не работает в этот день", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            } else MessageBox.Show("Сотрудник не выбран", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }
        //создание столбцов в dataGridView
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("EmployeeID", "EmployeeID");
            dataGridView1.Columns.Add("FullName", "FullName");
            dataGridView1.Columns.Add("Position", "Position");
            dataGridView1.Columns.Add("PhoneNumber", "PhoneNumber");
            dataGridView1.Columns.Add("Email", "Email");
        }

        //функция добавления строк в dataGridView
        private void ReadSinglRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4));
        }
        //обновление отображаемых данных
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string status = $"select * from Employees;";
            SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
            DB.OpenConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ReadSinglRow(dgw, reader);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Точки точки = new Точки();
            точки.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            График g = new График();
            g.id = id;
            g.Pointid = idPoint;
            g.SalePoint = SalePoint;
            g.Show();
            this.Hide();
        }

        private void Обновить_график_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
