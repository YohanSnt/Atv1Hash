using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Atv1Hash.Controller;
using System.Security.Cryptography;
using System.Text;


namespace Atv1Hash
{
    public partial class Form1 : Form
    {
        private Connection connection;

        public Form1()
        {
            InitializeComponent();
            connection = new Connection(); // Inicializa a classe Connection.
        }

        private void registrarbtn_Click(object sender, EventArgs e)
        {
            if (textBox1 != textBox3)
            {
                Console.WriteLine("Senhas distintas");
            }
            try
            {
                using (SqlConnection conn = connection.ReturnConnection())
                {
                    string query = "INSERT INTO Acessos (usuario, senha, email) VALUES (@usuario, @senha, @email)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", textBox2.Text);

                        string senhaHash = GerarHashSHA256(textBox1.Text);
                        cmd.Parameters.AddWithValue("@senha", senhaHash);

                        cmd.Parameters.AddWithValue("@email", textBox4.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro inserido com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar: {ex.Message}");
            }
        }

        private void abrirForm2Btn_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.CloseConnection(); // Fecha a conexão ao encerrar o formulário.
        }

        private string GerarHashSHA256(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2")); // Converte para hexadecimal
                }
                return sb.ToString();
            }
        }

    }
}
