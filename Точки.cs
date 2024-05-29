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
    enum RoWState
    {
        Existed,
        Deleted
    }
    public partial class Точки : Form
    {
        DB DB = new DB();
        int id = 0;
        int selectedRow;
        string SaleP;

        public Точки()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            НоваяТочка нт = new НоваяТочка();
            нт.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                id = Convert.ToInt32(row.Cells[0].Value.ToString());
                SaleP = row.Cells[1].Value.ToString();
            }
        }

        //удаление выбранной строки из бд
        private void Update()
        {
            string del = $"delete from SalesPoints where PointID = '{id}'";
            DB db = new DB();
            var command = new SqlCommand(del, db.getConnection);
            db.OpenConnection();
            command.ExecuteNonQuery();
            db.CloseConnection();
        }



        private void DeleteRow()
        {
            if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.RowIndex >= 0 && dataGridView1.CurrentCell.RowIndex < dataGridView1.Rows.Count)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows[index].Visible = false;
                if (dataGridView1.Rows[index].Cells[0].Value.ToString() == String.Empty)
                {
                    dataGridView1.Rows[index].Cells[2].Value = RoWState.Deleted;
                    return;
                }
                dataGridView1.Rows[index].Cells[2].Value = RoWState.Deleted;
            }
            else MessageBox.Show("Точка не выбрана", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //создание столбцов в dataGridView
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Id", "PointID");
            dataGridView1.Columns.Add("Название", "PointName");
            dataGridView1.Columns.Add("Адрес", "Address");         
        }

        //функция добавления строк в dataGridView
        private void ReadSinglRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2));
        }
        //обновление отображаемых данных
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string status = $"select * from SalesPoints;";
            SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
            DB.OpenConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ReadSinglRow(dgw, reader);
            }
            reader.Close();
        }

        private void Точки_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DeleteRow();
                Update();
            } else MessageBox.Show("Точка не выбрана", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        

        private void Точки_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                Обновить_график ug = new Обновить_график();
                ug.idPoint = id;
                ug.SalePoint = SaleP;
                ug.Show();
                this.Hide();
            } else MessageBox.Show("Точка не выбрана", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
