using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _211068.Model
{
    public class Produto
    {
        public int id { get; set; }
        public int id_categoria { get; set; }
        public int id_marca { get; set; }
        public string descricao { get; set; }
        public double estoque { get; set; }
        public double valor_venda { get; set; }
        public string foto { get; set; }

        public void Incluir()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("INSERT INTO produto (id_categoria, id_marca, descricao, estoque, valor_venda, foto)" +
                                                 "VALUES (@id_categoria, @id_marca, @descricao, @estoque, @valor_venda, @foto)", Banco.Conexao);

                Banco.Comando.Parameters.AddWithValue("@id_categoria", id_categoria);
                Banco.Comando.Parameters.AddWithValue("@id_marca", id_marca);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.Comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.Comando.Parameters.AddWithValue("@valor_venda", valor_venda);
                Banco.Comando.Parameters.AddWithValue("@foto", foto);

                Banco.Comando.ExecuteNonQuery();

                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================================================

        public void Alterar()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("UPDATE produto SET id_categoria = @id_categoria, id_marca = @id_marca, descricao = @descricao, " +
                                                 "estoque = @estoque, valor_venda = @valor_venda, foto = @foto WHERE id=@id", Banco.Conexao);

                Banco.Comando.Parameters.AddWithValue("@id_categoria", id_categoria);
                Banco.Comando.Parameters.AddWithValue("@id_marca", id_marca);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.Comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.Comando.Parameters.AddWithValue("@valor_venda", valor_venda);
                Banco.Comando.Parameters.AddWithValue("@foto", foto);
                Banco.Comando.Parameters.AddWithValue("@id", id);

                Banco.Comando.ExecuteNonQuery();

                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================================================

        public void Excluir()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("DELETE FROM produto WHERE id = @id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@id", id);

                Banco.Comando.ExecuteNonQuery();

                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================================================
        public DataTable Consultar()
        {
            try
            {
                Banco.Comando = new MySqlCommand("SELECT p.*, m.nome, c.descricao AS categoria_descricao" +
                                                 "FROM produto p" +
                                                 "JOIN marca m ON (m.id = p.id_marca)" +
                                                 "JOIN categoria c ON (c.id = c.id_categoria" +
                                                 "WHERE p.descricao LIKE @descricao ORDER BY p.descricao", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao + "%");
                Banco.Adaptador = new MySqlDataAdapter(Banco.Comando);
                Banco.DadosTabela = new DataTable();
                Banco.Adaptador.Fill(Banco.DadosTabela);
                return Banco.DadosTabela;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
