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

namespace кредитная_оганизация
{
    public partial class Логин : Form
    {
        DB DB = new DB();
        public Логин()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //открытие подключения и получение данных записанных в текстбоксы
            DB.OpenConnection();
            var loginUser = textBox3.Text;
            var passwordUser = textBox2.Text;
            //создание и выполнение запроса на проверку логина и пароля
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string select = $"select EmployeeID, login, password from Employees where login='{loginUser}' and password='{passwordUser}'";
            SqlCommand command = new SqlCommand(select, DB.getConnection);
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(dataTable);
            //проверка на правильность ввода данных
            if (dataTable.Rows.Count == 1 )
            {
                if (loginUser == "admin") {
                    MessageBox.Show("Вы успешно вошли", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Точки main = new Точки();

                    main.Show();
                    this.Hide();
                } else
                {
                    График график = new График();
                    string value = "";
                    string status = $"select * from Employees where login = '{loginUser}';";
                    SqlCommand sqlCommand = new SqlCommand(status, DB.getConnection);
                    DB.OpenConnection();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            value = reader["EmployeeID"].ToString();
                        }
                    }
                    график.id = Convert.ToInt32(value);
                    график.a = 1;
                    график.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Ошибка авторизации! Неверный логин или пароль", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DB.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reg reg = new reg();
            reg.Show();
            this.Hide();
        }
    }
}
