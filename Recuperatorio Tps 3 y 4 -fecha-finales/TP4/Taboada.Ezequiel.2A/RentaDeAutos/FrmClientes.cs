using Entidades;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RentaDeAutos
{
    public partial class FrmClientes : Form
    {

        private List<Cliente> clientes;
        private Cliente cliente;

        public Cliente Cliente { get => cliente; set => cliente = value; }

        public FrmClientes()
        {
            InitializeComponent();
            clientes = new List<Cliente>();

        }

        private async void FrmClientes_Load(object sender, EventArgs e)
        {
            BloquearAcciones();
            this.clientes = await FrmClientes.TraerClientes();
            if (clientes is not null)
            {
                this.dataGridView1.DataSource = clientes;
                LiberarAcciones();
            }
            else
            {
                MessageBox.Show("No se logro concectar con la base de datos de clientes");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.cliente = (Cliente)this.dataGridView1.CurrentRow.DataBoundItem;
            if (MessageBox.Show("*** ESTE PROCESO ES IRREVERSIBLE *** \n\n ¿Seguro que quiere eliminar ese Cliente?", "Eliminar Cliente", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (ClienteDAO.Eliminar(cliente.Dni))
                {
                    MessageBox.Show("Cliente eliminado");
                    Limpiar();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmClienteAlta frmAlta = new FrmClienteAlta(this.clientes);
            frmAlta.ShowDialog();
            if (frmAlta.DialogResult == DialogResult.OK)
            {
                if (ClienteDAO.Guardar(frmAlta.Cliente))
                {
                    MessageBox.Show("Alta exitosa");
                    Limpiar();
                }
            }
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtDni.Text))
            {
                List<Cliente> aux = new List<Cliente>();
                foreach (Cliente item in this.clientes)
                {
                    if (item.Dni.StartsWith(this.txtDni.Text.ToUpper()))
                    {
                        aux.Add(item);
                    }
                }
                this.dataGridView1.DataSource = aux;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            this.dataGridView1.DataSource = null;
            this.txtDni.Text = null;
            this.dataGridView1.DataSource = ClienteDAO.Leer();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            FrmClienteModificacion modificacion = new FrmClienteModificacion((Cliente)this.dataGridView1.CurrentRow.DataBoundItem);
            modificacion.ShowDialog();
            if (modificacion.DialogResult == DialogResult.OK)
            {

                if (ClienteDAO.Modificar(modificacion.Cliente))
                {
                    MessageBox.Show("Modificacion exitosa");
                    Limpiar();
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cliente = (Cliente)this.dataGridView1.CurrentRow.DataBoundItem;
            this.DialogResult = DialogResult.OK;
        }

        private void BloquearAcciones()
        {
            dataGridView1.Enabled = false;
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
            txtDni.Enabled = false;           
        }

        private void LiberarAcciones()
        {
            dataGridView1.Enabled = true;
            btnAgregar.Enabled = true;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnLimpiar.Enabled = true;
            txtDni.Enabled = true;            
        }


        private async static Task<List<Cliente>> TraerClientes()
        {
            List<Cliente> clientes = await Task.Run(() =>
            {
                try
                {
                    return ClienteDAO.Leer();

                }
                catch (Exception)
                {
                    return null;
                }
            });

            return clientes;
        }
    }

}
