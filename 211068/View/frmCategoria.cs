using _211068.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _211068.View
{
    public partial class frmCategoria : Form
    {

        Categoria c;

        public frmCategoria()
        {
            InitializeComponent();
        }

        public void limpaControles()
        {
            txtCategoria.Clear();
            txtPesquisa.Clear();
        }

        public void carregarGrid(string pesquisa)
        {
            c = new Categoria()
            {
                descricao = pesquisa
            };

            dgvCategoria.DataSource = c.Consultar();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtCategoria.Text == String.Empty) return;

            c = new Categoria()

            {
                descricao = txtCategoria.Text,
            };

            c.Incluir();

            limpaControles();
            carregarGrid("");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == String.Empty) return;

            c = new Categoria()
            {
                id = int.Parse(txtId.Text),
                descricao = txtCategoria.Text,
            };

            c.Alterar();

            limpaControles();
            carregarGrid("");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;

            if (MessageBox.Show("Deseja excluir o cadastro?", "Exclusão", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                c = new Categoria()
                {
                    id = int.Parse(txtId.Text)
                };

                c.Excluir();

                limpaControles();
                carregarGrid("");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategoria.RowCount > 0)
            {
                txtId.Text = dgvCategoria.CurrentRow.Cells["id"].Value.ToString();
                txtCategoria.Text = dgvCategoria.CurrentRow.Cells["descricao"].Value.ToString();
            }
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
        }
    }
}
