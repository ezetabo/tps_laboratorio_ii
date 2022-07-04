using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Entidades;

namespace RentaDeAutos
{
    public partial class FrmVehiculoAlta : FrmAltaHerencia
    {
        List<Vehiculo> vehiculos;
        private Vehiculo vehiculo;
        string patente;

        public virtual Vehiculo Vehiculo { get => vehiculo; }

        public FrmVehiculoAlta()
        {

        }

        public FrmVehiculoAlta(List<Vehiculo> lista)
        {
            InitializeComponent();
            this.vehiculos = lista;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VerificarCarga())
            {
                Enum.TryParse<EClasificcacion>(this.cmbClasificacion.SelectedValue.ToString(), out EClasificcacion clasificcacion);
                Enum.TryParse<ConsoleColor>(this.cmbColor.SelectedValue.ToString(), out ConsoleColor color);
                this.vehiculo = new Vehiculo(this.patente, clasificcacion, color, false);
                this.DialogResult = DialogResult.OK;
            }
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

        private void FrmAltaVehiculo_Load(object sender, EventArgs e)
        {
            this.cmbClasificacion.DataSource = Enum.GetValues(typeof(EClasificcacion));
            this.cmbColor.DataSource = Enum.GetValues(typeof(ConsoleColor));
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (ExistePatente(this.patente))
            {
                MessageBox.Show("La patente ya existe");
                this.textBox1.Clear();
                this.textBox1.Focus();
            }

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!Validador.TryParsePatente(this.textBox1.Text,out this.patente))
            {
                MessageBox.Show("Formato de patente incorrecto\n ej:AA000AA - AA 000 AAA");
                this.textBox1.Clear();
                this.textBox1.Focus();
            }
        }

        private bool ExistePatente(string dato)
        {
            bool existe = false;
            foreach (Vehiculo item in this.vehiculos)
            {
                if (item.Patente == dato)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    }
}
