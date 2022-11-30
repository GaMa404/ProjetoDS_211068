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
    public partial class frmCliente : Form
    {
        Cidade ci;
        Cliente cl;

        public frmCliente()
        {
            InitializeComponent();
        }

        public void limpaControles()
        {
            txtId.Clear();
            txtNome.Clear();
            cboCidade.SelectedIndex = -1;
            txtUf.Clear();
            mskCPF.Clear();
            txtRenda.Clear();
            dtpDataNasc.Value= DateTime.Now;
            picFoto.ImageLocation = "";
            chkVenda.Checked = false;
        }

        public void carregarGrid(string pesquisa)
        {
            cl = new Cliente()
            {
                nome = pesquisa
            };

            dgvClientes.DataSource = cl.Consultar();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.RowCount > 0)
            {
                txtId.Text = dgvClientes.CurrentRow.Cells["id"].Value.ToString();
                txtNome.Text = dgvClientes.CurrentRow.Cells["nome"].Value.ToString();
                cboCidade.Text = dgvClientes.CurrentRow.Cells["cidade"].Value.ToString();
                txtUf.Text = dgvClientes.CurrentRow.Cells["uf"].Value.ToString();
                chkVenda.Checked = (bool)dgvClientes.CurrentRow.Cells["venda"].Value;
                mskCPF.Text = dgvClientes.CurrentRow.Cells["cpf"].Value.ToString();
                dtpDataNasc.Text = dgvClientes.CurrentRow.Cells["data_nasc"].Value.ToString();
                txtRenda.Text = dgvClientes.CurrentRow.Cells["renda"].Value.ToString();
                picFoto.ImageLocation = dgvClientes.CurrentRow.Cells["foto"].Value.ToString();
            }
        }

        private void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidade.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboCidade.SelectedItem;
                txtUf.Text = reg["uf"].ToString();
            }
        }

        private void picFoto_Click(object sender, EventArgs e)
        {
            ofdArquivo.InitialDirectory = "D:/GaMa/C#/Form/ProjetoDS_211068/fotos_clientes";
            ofdArquivo.FileName = "";
            ofdArquivo.ShowDialog();
            picFoto.ImageLocation = ofdArquivo.FileName;

            lbl_foto.Text = "";
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == String.Empty) return;

            cl = new Cliente()
            {
                id_cidade = (int)cboCidade.SelectedValue,
                nome = txtNome.Text,
                data_nasc = dtpDataNasc.Value,
                renda = double.Parse(txtRenda.Text),
                cpf = mskCPF.Text,
                foto = picFoto.ImageLocation,
                venda = chkVenda.Checked
            };

            cl.Incluir();

            limpaControles();
            carregarGrid("");

            lbl_foto.Text = "Clique aqui para escolher a foto";
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;
            {
                cl = new Cliente()
                {
                    id = int.Parse(txtId.Text),
                    id_cidade = (int)cboCidade.SelectedValue,
                    nome = txtNome.Text,
                    data_nasc = dtpDataNasc.Value,
                    renda = double.Parse(txtRenda.Text),
                    cpf = mskCPF.Text,
                    foto = picFoto.ImageLocation,
                    venda = chkVenda.Checked
                };
                cl.Alterar();

                limpaControles();
                carregarGrid("");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;

            if (MessageBox.Show("Deseja excluir o cadastro?", "Exclusão", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cl = new Cliente()
                {
                    id = int.Parse(txtId.Text)
                };

                cl.Excluir();

                limpaControles();
                carregarGrid("");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            ci = new Cidade();
            cboCidade.DataSource = ci.Consultar();
            cboCidade.DisplayMember = "nome";
            cboCidade.ValueMember = "id";

            limpaControles();
            carregarGrid("");

            dgvClientes.Columns["id_cidade"].Visible = false;
            dgvClientes.Columns["foto"].Visible = false;
        }
    }
}
