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
        #region Atributo

        /// <summary>
        /// lista con los operadores matematicos.
        /// </summary>
        private List<char> operadores = new List<char>() { ' ', '+', '-', '*', '/' };

        #endregion

        #region Constructor

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

        #endregion

        #region Metodos y Validaciones

        /// <summary>
        ///  borra los datos de los TextBox, ComboBox y Label de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            this.txtNumero1.Text = string.Empty;
            this.txtNumero2.Text = string.Empty;
            this.cmbOperador.SelectedIndex = -1;
            this.lblResultado.Text = "0";
        }

        /// <summary>
        /// recibe dos números y un operador, realiza la operacion matematica y retorna el resultado.
        /// </summary>
        /// <param name="numero1"></param> primer numero.
        /// <param name="numero2"></param> segundo numero.
        /// <param name="operando"></param> operador matematico.
        /// <returns></returns> un double son el valor de la operacion. 
        private static double Operar(string numero1, string numero2, string operando)
        {
            return Calculadora.Operar(new Operando(numero1), new Operando(numero2), char.Parse(operando));
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
        /// valida que la cadena recibida sea numerica y del tipo DOUBLE.
        /// </summary>
        /// <param name="strNumero"></param> cadena a verificar.
        /// <returns></returns> true si es numerica y del tipo DOUBLE o false en caso contrario.
        private bool EsDouble(string strNumero)
        {
            bool esDouble = true;
            bool flagPrimero = true;
            int menos = 0;
            int coma = 0;
            foreach (char item in strNumero)
            {
                if (item == '-')
                {
                    menos++;
                    if (flagPrimero && item == '-')
                    {
                        flagPrimero = false;
                        continue;
                    }
                }
                if (item == '.')
                {
                    coma++;
                }
                if (!char.IsDigit(item) && item != '.' || menos > 1 || coma > 1)
                {
                    esDouble = false;
                    break;
                }
                flagPrimero = false;
            }
            return esDouble;
        }

        #endregion|

        #region Evento_Load

        /// <summary>
        /// llama al método Limpiar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        #endregion

        #region Evento_Closing

        /// <summary>
        /// al intentar cerrar el formulario emite un mensaje para confirmar la accion. Si contesta SI se cerrará, si contesta NO continua en ejecución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Confirmar("¿Está seguro de querer salir?", "SALIR");
        }

        #endregion

        #region Eventos_Clik

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
        ///  al intentar cerrar el formulario emite un mensaje para confirmar la accion. Si contesta SI se cerrará, si contesta NO continua en ejecución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// toma los datos de los textBoxs y el comboBox, se los pasa al metodo operar y refleja los resultados en el label y listBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// convertirá el resultado, de existir, a binario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// convertirá el resultado, de existir y ser binario, a decimal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region Eventos_TextChanged

        /// <summary>
        /// verifica que el dato ingresado sea numerico.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNumero1_TextChanged(object sender, EventArgs e)
        {
            if (!EsDouble(this.txtNumero1.Text))
            {
                MessageBox.Show("Por favor ingrese un dato numerico", "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNumero1.Text = string.Empty;
            }
        }

        /// <summary>
        /// verifica que el dato ingresado sea numerico.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNumero2_TextChanged(object sender, EventArgs e)
        {
            if (!EsDouble(this.txtNumero2.Text))
            {
                MessageBox.Show("Por favor ingrese un dato numerico", "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNumero2.Text = string.Empty;
            }
        }

        #endregion
    }
}
