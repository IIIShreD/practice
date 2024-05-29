using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace кредитная_оганизация
{
    class DB
    {
        //подключение базы данных
        SqlConnection DataB = new SqlConnection(@"Data Source=DESKTOP-FPPDM7F; Initial Catalog=Сотрудники; Integrated Security=True; MultipleActiveResultSets=true");
        //функция окрытия подключения
        public void OpenConnection()
        {
            if (DataB.State == System.Data.ConnectionState.Closed)
            {
                DataB.Open();
            }

        }
        //функция закрытия подключения
        public void CloseConnection()
        {
            if (DataB.State == System.Data.ConnectionState.Open)
            {
                DataB.Close();
            }

        }
        //получение статуса подключения
        public SqlConnection getConnection
        {
            get { return DataB; }
        }
    }
}
