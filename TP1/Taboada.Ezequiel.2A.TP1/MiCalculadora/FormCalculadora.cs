using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace MiCalculadora
{
    public partial class FormCalculadora : Form
    {
        /// <summary>
        /// lista con los operadores matematicos.
        /// </summary>
        private List<char> operadores = new List<char>() { ' ', '+', '-', '*', '/' };

        /// <summary>
        /// inicializa los componentes con los valores asignados.
        /// </summary>
        public FormCalculadora()
        {
            InitializeComponent();

            foreach (char item in operadores)
            {
                this.cmbOperador.Items.Add(item);
            }
        }

        /// <summary>
        ///  borra los datos de los TextBox, ComboBox y Label de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            this.txtNumero1.Text = string.Empty;
            this.txtNumero2.Text = string.Empty;
            this.cmbOperador.Text = string.Empty;
            this.lblResultado.Text = "0";
        }

        /// <summary>
        /// llama al método Limpiar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        /// <summary>
        /// llama al método Limpiar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        /// <summary>
        /// muestra por pantalla un mensaje a eleccion preguntando por si o no.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="titulo"></param>
        /// <returns></returns> true en caso de si y false en caso de no.
        private bool Confirmar(string mensaje, string titulo)
        {
            bool retorno = false;
            DialogResult respuesta = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                retorno = true;
            }
            return retorno;
        }

        /// <summary>
        /// al intentar cerrar el formulario emite un mensaje para confirmar la accion. Si contesta SI se cerrará, si contesta NO continua en ejecución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Confirmar("¿Está seguro de querer salir?", "SALIR");
        }

        /// <summary>
        ///  al intentar cerrar el formulario emite un mensaje para confirmar la accion. Si contesta SI se cerrará, si contesta NO continua en ejecución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static double Operar(string numero1, string numero2, string operando)
        {
            return Calculadora.Operar(new Operando(numero1), new Operando(numero2), char.Parse(operando));
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            string operador = this.cmbOperador.Text;
            if (operador == string.Empty)
            {
                operador = "+";
            }
            this.lblResultado.Text = Convert.ToString(Operar(this.txtNumero1.Text, this.txtNumero2.Text, operador));
            this.lstOperaciones.Items.Add(this.txtNumero1.Text + " " + operador + " " + this.txtNumero2.Text + " = " + this.lblResultado.Text + "\n");
        }
               

        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            Operando operando = new Operando();
            string binario = operando.DecimalBinario(this.lblResultado.Text);
            if (binario != "Valor Invalido")
            {
                this.lstOperaciones.Items.Add(this.lblResultado.Text + " (D) = " + binario + " (B)");
            }
            else
            {
                this.lstOperaciones.Items.Add(binario);
            }
            this.lblResultado.Text = binario;
        }

        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            Operando operando = new Operando();
            string numero = operando.BinarioDecimal(this.lblResultado.Text);
            if (numero != "Valor Invalido")
            {
                this.lstOperaciones.Items.Add(this.lblResultado.Text + " (B) = " + numero + " (D)");
            }
            else
            {
                this.lstOperaciones.Items.Add(numero);
            }
            this.lblResultado.Text = numero;
        }
    }
}
