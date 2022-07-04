using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Entidades;

namespace RentaDeAutos
{
    public partial class FrmClienteAlta : FrmAltaHerencia
    {
        private List<Cliente> clientes;
        private Cliente cliente;
        private string dni;
        private string nombre;
        private string apeliido;
        private string telefono;

        public List<Cliente> Clientes { get => clientes; set => clientes = value; }
        public virtual Cliente Cliente { get => cliente; set => cliente = value; }

        public FrmClienteAlta()
        {

        }

        public FrmClienteAlta(List<Cliente> clientes)
        {
            InitializeComponent();
            this.clientes = clientes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool VerificarCarga()
        {
            bool ok = true;
            foreach (Control item in groupBox1.Controls)
            {
                if (string.IsNullOrEmpty(item.Text))
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VerificarCarga())
            {
                this.cliente = new Cliente(dni, nombre, apeliido, telefono);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("por favor debe completar todos los campos");
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (!Validador.TryParseNombre(this.txtNombre.Text, out this.nombre))
            {
                MessageBox.Show("Nombre no Valido");
                this.txtNombre.Clear();
                this.txtNombre.Focus();
            }
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            if (!Validador.TryParseNombre(this.txtApellido.Text, out apeliido))
            {
                MessageBox.Show("Apellido no valido");
                this.txtApellido.Clear();
                this.txtApellido.Focus();
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (EixsteDni(this.textBox1.Text))
            {
                MessageBox.Show("El DNI ya existe");
                this.textBox1.Clear();
                this.textBox1.Focus();
            }
            else
            {
                dni = this.textBox1.Text;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!Validador.Es_DNI_Telefono(this.textBox1.Text))
            {
                MessageBox.Show("El formato no es valido");
                this.textBox1.Clear();
                this.textBox1.Focus();

            }
        }

        private void txtTelefono_Validated(object sender, EventArgs e)
        {
            if (!Validador.Es_DNI_Telefono(this.txtTelefono.Text))
            {
                MessageBox.Show("Formato de Telefono incorrecto");
                this.txtTelefono.Clear();
                this.txtTelefono.Focus();
            }
            else
            {
                telefono = this.txtTelefono.Text;
            }
        }

        private bool EixsteDni(string dni)
        {
            bool existe = false;
            foreach (Cliente item in this.clientes)
            {
                if (item.Dni == dni)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    }

}
