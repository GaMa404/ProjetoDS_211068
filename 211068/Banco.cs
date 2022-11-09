using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _211068
{
    public class Banco
    {
        public static MySqlConnection Conexao;
        public static MySqlCommand Comando;
        public static MySqlDataAdapter Adaptador;
        public static DataTable DadosTabela;

        public static void AbrirConexao()
        {
            try
            {
                Conexao = new MySqlConnection("server=localhost;port=3307;uid=root;pwd=etecjau");

                Conexao.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void FecharConexao()
        {
            try
            {
                Conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CriarBanco()
        {
            try
            {
                AbrirConexao();

                Comando = new MySqlCommand("CREATE DATABASE IF NOT EXISTS db_vendas; USE db_vendas;", Conexao);
                Comando.ExecuteNonQuery();
                
                // ======================================================================================

                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS cidade (" +
                                           " id INT AUTO_INCREMENT," +
                                           " nome VARCHAR(120)," +
                                           " uf CHAR(2)," +
                                           " PRIMARY KEY (id));", Conexao);
                Comando.ExecuteNonQuery();


                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS marca (" +
                                          " id INT AUTO_INCREMENT," +
                                          " nome VARCHAR(120)," +
                                          " PRIMARY KEY (id));", Conexao);
                Comando.ExecuteNonQuery();


                Comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS categoria (" +
                                          " id INT AUTO_INCREMENT," +
                                          " descricao VARCHAR(150)," +
                                          " PRIMARY KEY (id));", Conexao);
                Comando.ExecuteNonQuery();

                // ======================================================================================

                FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
