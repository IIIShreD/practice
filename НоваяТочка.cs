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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace кредитная_оганизация
{
    public partial class НоваяТочка : Form
    {
        DB DB = new DB();
        public НоваяТочка()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Точки тч = new Точки();
            тч.Show();
            this.Hide();
        }

        private void НоваяТочка_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var loginUser = textBox1.Text;
            var passwordUser = textBox2.Text;
            string select = $"INSERT INTO SalesPoints (PointName, Address) VALUES ('{loginUser}', '{passwordUser}')";
            SqlCommand command = new SqlCommand(select, DB.getConnection);
            DB.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Вы успешно зарегистрированы", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Точки login = new Точки();
                login.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Косяк", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DB.CloseConnection();

        }

    }
}
