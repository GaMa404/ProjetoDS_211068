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
    public partial class frmMarca : Form
    {
        Marca m;

        public frmMarca()
        {
            InitializeComponent();
        }
        public void limpaControles()
        {
            txtMarca.Clear();
            txtPesquisa.Clear();
        }

        public void carregarGrid(string pesquisa)
        {
            m = new Marca()
            {
                nome = pesquisa
            };

            dgvMarca.DataSource = m.Consultar();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtMarca.Text == String.Empty) return;

            m = new Marca()

            {
                nome = txtMarca.Text,
            };

            m.Incluir();

            limpaControles();
            carregarGrid("");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == String.Empty) return;

            m = new Marca()
            {
                id = int.Parse(txtId.Text),
                nome = txtMarca.Text,
            };

            m.Alterar();

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
                m = new Marca()
                {
                    id = int.Parse(txtId.Text)
                };

                m.Excluir();

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

        private void dgvMarca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMarca.RowCount > 0)
            {
                txtId.Text = dgvMarca.CurrentRow.Cells["id"].Value.ToString();
                txtMarca.Text = dgvMarca.CurrentRow.Cells["nome"].Value.ToString();
            }
        }

        private void frmMarca_Load(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
        }
    }
}
