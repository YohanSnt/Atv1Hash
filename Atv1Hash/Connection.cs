using System;
using System.Data;
using System.Data.SqlClient;

namespace Atv1Hash.Controller
{
    internal class Connection
    {
        private SqlConnection con;

        private string DataBase = "Logins_YOHAN";
        private string Server = "sqlexpress"; // certifique-se de que está certo!
        private string Username = "aluno";
        private string Password = "aluno";

        public Connection()
        {
            string stringConnection =
                "Data Source=" + Server +
                ";Initial Catalog=" + DataBase +
                ";User ID=" + Username +
                ";Password=" + Password +
                ";Encrypt=false;TrustServerCertificate=true";

            // Inicializa com a connection string correta
            con = new SqlConnection(stringConnection);
        }

        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection ReturnConnection()
        {
            OpenConnection();
            return con;
        }
    }
}
