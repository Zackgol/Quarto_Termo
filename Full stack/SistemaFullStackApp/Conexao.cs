using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFullStackApp
{
    public static class ConexaoMySQL 
    {
        // String de conexão para o MySQL local
        private static string strConexao =
            "Server=localhost;Port=3306;Database=FullStackDB;Uid=root;Pwd=vidaloka1;";

        public static MySqlConnection ObterConexao() 
        {
            return new MySqlConnection(strConexao);
        }
    }
    
}
