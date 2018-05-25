﻿using Foodtruck.Negocio;
using Foodtruck.Negocio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Foodtruck.Grafico
{
    public partial class TelaListaBebidas : Form
    {
        public TelaListaBebidas()
        {
            InitializeComponent();
        }
        private void AbreTelaInclusaoAlteracao(Bebida bebidaSelecionada)
        {
            ManterBebida tela = new ManterBebida();
            tela.MdiParent = this.MdiParent;
            tela.BebidaSelecionada = bebidaSelecionada;
            tela.FormClosed += Tela_FormClosed;
            tela.Show();
        }

        private void Tela_FormClosed(object sender, FormClosedEventArgs e)
        {
            CarregarBebidas();
        }

        private void CarregarBebidas()
        { 
            //Gerenciador gerenciador = new Gerenciador();
            dgBebidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgBebidas.MultiSelect = false;
            dgBebidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgBebidas.AutoGenerateColumns = false;
            List<Bebida> bebidas = Program.Gerenciador.TodasAsBebidas();
            dgBebidas.DataSource = bebidas;
        }

        private void TelaListaBebidas_Load(object sender, EventArgs e)
        {
            CarregarBebidas();
        }

        /*private void btAdicionar_Click(object sender, EventArgs e)
        {
            ManterBebida tela = new ManterBebida();
            tela.Show();
        }*/
        private void btAdicionar_Click(object sender, EventArgs e)
        {
            AbreTelaInclusaoAlteracao(null);
        }

       

        private bool VerificarSelecao()
        {
            if (dgBebidas.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha");
                return false;
            }
            return true;
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
                if (VerificarSelecao())
                {
                    Bebida bebidaSelecionada = (Bebida)dgBebidas.SelectedRows[0].DataBoundItem;
                    AbreTelaInclusaoAlteracao(bebidaSelecionada);
                }
        }

        private void btRemover_Click(object sender, EventArgs e)
        {
            {
                if (VerificarSelecao())
                {
                    DialogResult resultado = MessageBox.Show("Tem certeza?", "Quer remover?", MessageBoxButtons.OKCancel);
                    if (resultado == DialogResult.OK)
                    {
                        Bebida bebidaSelecionada = (Bebida)dgBebidas.SelectedRows[0].DataBoundItem;
                        var validacao = Program.Gerenciador.RemoverBebida(bebidaSelecionada);
                        if (validacao.Valido)
                        {
                            MessageBox.Show("Bebida removida com sucesso");
                        }
                        else
                        {
                            MessageBox.Show("Ocorreu um problema ao remover a bebida");
                        }
                        CarregarBebidas();
                    }
                }
            }
        }
    }
}
/*private void CarregarClientes()
        {
            dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgClientes.MultiSelect = false;
            dgClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgClientes.AutoGenerateColumns = false;
            List<Cliente> clientes = Program.Gerenciador.TodosOsClientes();
            dgClientes.DataSource = clientes;
        }
*/