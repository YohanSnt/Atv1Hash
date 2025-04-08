using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Atv1Hash.Controller;

namespace Atv1Hash
{
    public partial class Form2 : Form
    {
        private Connection connection;

        public Form2()
        {
            InitializeComponent();
            connection = new Connection(); // Inicializa a classe Connection.
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = connection.ReturnConnection())
                {
                    string query = "SELECT * FROM Acessos";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable; // Exibe os dados no DataGridView.
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exibir dados: {ex.Message}");
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.CloseConnection(); // Fecha a conexão ao encerrar o formulário.
        }
    }
}
