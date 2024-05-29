
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
using System.Text.RegularExpressions;


namespace кредитная_оганизация
{
    public partial class reg : Form
    {
        DB DB = new DB();
        public reg()
        {
            InitializeComponent();
        }

        //функция проверки уникальности логина
        private Boolean checkUser()
        {
            //получение данных записанных в текстбокс
            var loginUser = textBox3.Text;
            //создание и выполнение запроса на проверку логина
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string select = $"select EmployeeID, login, password  from Employees where login='{loginUser}'";
            SqlCommand command = new SqlCommand(select, DB.getConnection);
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else { return false; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Логин лг = new Логин();
            лг.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //запуск функции
            if (checkUser())
            {
                return;
            }

            var loginUser = textBox1.Text;
            var passwordUser = textBox2.Text;
            var passwordUser2 = textBox3.Text;
            var fullname = textBox4.Text;
            var positon = textBox5.Text;
            var phone = textBox6.Text;
            var email = textBox7.Text;
            string select = $"INSERT INTO Employees (FullName, Position, PhoneNumber, Email, login, password) VALUES ('{fullname}', '{positon}', '{phone}', '{email}', '{loginUser}', '{passwordUser}')";
            SqlCommand command = new SqlCommand(select, DB.getConnection);
            if (passwordUser == passwordUser2) {
                if (email.Contains("@"))
                {
                    if (loginUser != "" & passwordUser != "" & passwordUser2 != "" & fullname != "" & positon != "" & phone != "" & email != "")
                    {
                        if (phone.Contains("1") || phone.Contains("2") || phone.Contains("3") || phone.Contains("4") || phone.Contains("5") || phone.Contains("6") || phone.Contains("7") || phone.Contains("8") || phone.Contains("9") || phone.Contains("0") || phone.Contains("+") || phone.Contains("-")) {
                            DB.OpenConnection();

                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Вы успешно зарегистрированы", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Логин login = new Логин();
                                login.Show();
                                this.Hide();
                            } else MessageBox.Show("Косяк", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DB.CloseConnection();
                        } else MessageBox.Show("Неверный формат номера телефона", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else MessageBox.Show("Поля не заполнены", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else MessageBox.Show("Неверный формат эл-почты", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else MessageBox.Show("Пароли не совпадают", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void reg_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }
    }
}
