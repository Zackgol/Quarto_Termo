using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Atividade02
{
    public class Conexao
    {
        private static string connectionString =
            "Server=localhost;Database=Lojadb;Uid=root;Pwd=vidaloka1;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
