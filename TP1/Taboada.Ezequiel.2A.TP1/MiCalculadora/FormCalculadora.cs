using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using System.Text;

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
        /// <param name="numero1"> primer numero </param>
        /// <param name="numero2"> segundo numero </param>
        /// <param name="operando"> operador matematico </param>
        /// <returns> un double son el valor de la operacion </returns>
        private static double Operar(string numero1, string numero2, string operando)
        {
            if (string.IsNullOrEmpty(operando))
            {
                operando = " ";
            }
            return Math.Round(Calculadora.Operar(new Operando(numero1), new Operando(numero2), char.Parse(operando)), 2);
        }

        /// <summary>
        /// muestra por pantalla un mensaje a eleccion preguntando por si o no.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="titulo"></param>
        /// <returns> true en caso de si y false en caso de no </returns>
        private static bool Confirmar(string mensaje, string titulo)
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
        /// <param name="strNumero"> cadena a verificar </param>
        /// <returns> true si es numerica y del tipo DOUBLE o false en caso contrario </returns>
        private static bool EsDouble(string strNumero)
        {
            bool esDouble = true;
            bool flagPrimero = true;
            int menos = 0;
            int punto = 0;
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
                    punto++;
                }
                if (!char.IsDigit(item) && item != '.' || menos > 1 || punto > 1)
                {
                    esDouble = false;
                    break;
                }
                flagPrimero = false;
            }
            return esDouble;
        }
        #endregion

        #region Evento_Load

        /// <summary>
        /// llama al método Limpiar y carga el comboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            foreach (char item in operadores)
            {
                this.cmbOperador.Items.Add(item);
            }
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
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        /// <summary>
        ///  al intentar cerrar el formulario emite un mensaje para confirmar la accion. Si contesta SI se cerrará, si contesta NO continua en ejecución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// toma los datos de los textBoxs y el comboBox, se los pasa al metodo operar y refleja los resultados en el label y listBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperar_Click(object sender, EventArgs e)
        {
            string operador = this.cmbOperador.Text;
            string numero1 = this.txtNumero1.Text.Replace(',', '.');
            string numero2 = this.txtNumero2.Text.Replace(',', '.');
            double resultado = Operar(numero1, numero2, operador);

            if (string.IsNullOrEmpty(operador))
            {
                operador = "+";
            }

            if (string.IsNullOrEmpty(numero1) || !double.TryParse(numero1,out double num1))
            {
                numero1 = "0";
            }

            if (string.IsNullOrEmpty(numero2) || !double.TryParse(numero2, out double num2))
            {
                numero2 = "0";
            }

            if (resultado == double.MinValue)
            {
                this.lblResultado.Text = "No se puede dividir entre cero";
                this.lstOperaciones.Items.Add(this.lblResultado.Text);
            }
            else
            {
                this.lblResultado.Text = resultado.ToString();
                this.lstOperaciones.Items.Add($"{Convert.ToDouble(numero1)} {operador} {Convert.ToDouble(numero2)}  =  {resultado}");
            }
        }

        /// <summary>
        /// convertirá el resultado, de existir, a binario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConvertirABinario_Click(object sender, EventArgs e)
        {
            string binario = Operando.DecimalBinario(this.lblResultado.Text);
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
        private void BtnConvertirADecimal_Click(object sender, EventArgs e)
        {
            string numero = Operando.BinarioDecimal(this.lblResultado.Text);
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
        private void TxtNumero1_TextChanged(object sender, EventArgs e)
        {
            if (!EsDouble(this.txtNumero1.Text.Replace(',', '.')))
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
        private void TxtNumero2_TextChanged(object sender, EventArgs e)
        {
            if (!EsDouble(this.txtNumero2.Text.Replace(',', '.')))
            {
                MessageBox.Show("Por favor ingrese un dato numerico", "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNumero2.Text = string.Empty;
            }
        }

        #endregion
    }
}
