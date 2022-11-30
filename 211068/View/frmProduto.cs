using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using _211068.Model;

namespace _211068.View
{
    public partial class frmProduto : Form
    {
        Categoria c;
        Marca m;
        Produto p;

        public frmProduto()
        {
            InitializeComponent();
        }

        public void limpaControles()
        {
            txtId.Clear();
            txtDescricao.Clear();
            cboMarca.SelectedIndex = -1;
            cboCategoria.SelectedIndex = -1;
            txtEstoque.Clear();
            txtValorVenda.Clear();
            picFoto.ImageLocation = "";
        }

        public void carregarGrid(string pesquisa)
        {
            p = new Produto()
            {
                descricao = pesquisa
            };

            dgvProduto.DataSource = p.Consultar();
        }

        private void dgvProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_foto.Text = "";

            if (dgvProduto.RowCount > 0)
            {
                txtId.Text = dgvProduto.CurrentRow.Cells["id"].Value.ToString();
                txtDescricao.Text = dgvProduto.CurrentRow.Cells["descricao"].Value.ToString();
                cboCategoria.Text = dgvProduto.CurrentRow.Cells["categoria_descricao"].Value.ToString();
                cboMarca.Text = dgvProduto.CurrentRow.Cells["nome"].Value.ToString();
                txtValorVenda.Text = dgvProduto.CurrentRow.Cells["valor_venda"].Value.ToString();
                txtEstoque.Text = dgvProduto.CurrentRow.Cells["estoque"].Value.ToString();
                picFoto.ImageLocation = dgvProduto.CurrentRow.Cells["foto"].Value.ToString();
            }
        }

        private void frmProduto_Load(object sender, EventArgs e)
        {
            c = new Categoria();
            cboCategoria.DataSource = c.Consultar();
            cboCategoria.DisplayMember = "descricao";
            cboCategoria.ValueMember = "id";

            m = new Marca();
            cboMarca.DataSource = m.Consultar();
            cboMarca.DisplayMember = "nome";
            cboMarca.ValueMember = "id";

            limpaControles();
            carregarGrid("");

            dgvProduto.Columns["id_categoria"].Visible = false;
            dgvProduto.Columns["id_marca"].Visible = false;
            dgvProduto.Columns["foto"].Visible = false;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtDescricao.Text == String.Empty) return;

            p = new Produto()
            {
                id_categoria = (int)cboCategoria.SelectedValue,
                id_marca = (int)cboMarca.SelectedValue,
                descricao = txtDescricao.Text,
                valor_venda = double.Parse(txtValorVenda.Text),
                estoque = double.Parse(txtEstoque.Text),
                foto = picFoto.ImageLocation,
            };

            p.Incluir();

            limpaControles();
            carregarGrid("");

            lbl_foto.Text = "Clique aqui para escolher a foto";
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;
            {
                p = new Produto()
                {
                    id = int.Parse(txtId.Text),
                    id_categoria = (int)cboCategoria.SelectedValue,
                    id_marca = (int)cboMarca.SelectedValue,
                    descricao = txtDescricao.Text,
                    valor_venda = double.Parse(txtValorVenda.Text),
                    estoque = double.Parse(txtEstoque.Text),
                    foto = picFoto.ImageLocation,
                };

                p.Alterar();

                limpaControles();
                carregarGrid("");
            }

            lbl_foto.Text = "Clique aqui para escolher a foto";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");

            lbl_foto.Text = "Clique aqui para escolher a foto";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;

            if (MessageBox.Show("Deseja excluir o cadastro?", "Exclusão", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                p = new Produto()
                {
                    id = int.Parse(txtId.Text)
                };

                p.Excluir();

                limpaControles();
                carregarGrid("");
            }

            lbl_foto.Text = "Clique aqui para escolher a foto";
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void picFoto_Click(object sender, EventArgs e)
        {
            ofdArquivo.InitialDirectory = "D:/GaMa/C#/Form/ProjetoDS_211068/fotos_produtos";
            ofdArquivo.FileName = "";
            ofdArquivo.ShowDialog();
            picFoto.ImageLocation = ofdArquivo.FileName;

            lbl_foto.Text = "";
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoria.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboCategoria.SelectedItem;
            }
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMarca.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboMarca.SelectedItem;
            }
        }
    }
}
