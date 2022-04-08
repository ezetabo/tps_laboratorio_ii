using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Operando
    {
        #region Atributos y Propiedades
        /// <summary>
        /// atributo de la clase Operando.
        /// </summary>
        private double numero;

        /// <summary>
        /// propiedad de la clase Operando, asignará un valor al atributo número, previa validación.
        /// </summary>
        private string Numero
        {
            set
            {
                this.numero = ValidarOperando(value);
            }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// constructor por defecto, asigna valor 0 al atributo numero.
        /// </summary>
        public Operando() : this(0)
        {

        }

        /// <summary>
        /// constructor parametrizado, asigna valor al atributo numero.
        /// </summary>
        /// <param name="numero"></param>
        public Operando(double numero)
        {
            this.numero = numero;
        }

        /// <summary>
        /// constructor parametrizado, asigna valor al atributo numero.
        /// </summary>
        /// <param name="strNumero"></param>
        public Operando(string strNumero)
        {
            this.Numero = strNumero;
        }
        #endregion

        #region Validaciones
        /// <summary>
        /// comprueba que el valor recibido sea numérico, y lo retornará en formato double. Caso contrario, retornará 0.
        /// </summary>
        /// <param name="strNumero"></param> valor a validar que sea numerico.
        /// <returns></returns> un double con el valor de dato recido en caso de ser valido, sino el valor 0.
        private double ValidarOperando(string strNumero)
        {
            if (!double.TryParse(strNumero, out double numero))
            {
                numero = 0;
            }
            return numero;
        }

        /// <summary>
        /// valida que la cadena de caracteres esté compuesta SOLAMENTE por caracteres '0' o '1'.
        /// </summary>
        /// <param name="binario"></param> cadena a validar.
        /// <returns></returns> true en caso de ser solo 1 o 0. False en caso de no ser solo 1 o 0.
        private bool EsBinario(string binario)
        {
            bool retorno = true;
            foreach (char item in binario)
            {
                if (item != '0' && item != '1')
                {
                    retorno = false;
                    break;
                }
            }
            return retorno;
        }
        #endregion

        #region Sobrecarga Operadores
        /// <summary>
        ///  realiza la operacion sumar entre los dos números.
        /// </summary>
        /// <param name="n1"></param> numero 1.
        /// <param name="n2"></param> numero 2.
        /// <returns></returns> resultado de la operacion.
        public static double operator +(Operando n1, Operando n2)
        {
            return n1.numero + n2.numero;
        }

        /// <summary>
        ///  realiza la operacion restar entre los dos números.
        /// </summary>
        /// <param name="n1"></param> numero 1.
        /// <param name="n2"></param> numero 2.
        /// <returns></returns> resultado de la operacion.
        public static double operator -(Operando n1, Operando n2)
        {
            return n1.numero - n2.numero;
        }

        /// <summary>
        ///  realiza la operacion multiplicar entre los dos números.
        /// </summary>
        /// <param name="n1"></param> numero 1.
        /// <param name="n2"></param> numero 2.
        /// <returns></returns> resultado de la operacion.
        public static double operator *(Operando n1, Operando n2)
        {
            return n1.numero * n2.numero;
        }

        /// <summary>
        ///  realiza la operacion dividir entre los dos números.
        /// </summary>
        /// <param name="n1"></param> numero 1.
        /// <param name="n2"></param> numero 2.
        /// <returns></returns> resultado de la operacion, en caso de una división por 0, retornará double.MinValue.
        public static double operator /(Operando n1, Operando n2)
        {
            double resultado = double.MinValue;
            if (n2.numero != 0)
            {
                resultado = n1.numero / n2.numero;
            }
            return resultado;
        }
        #endregion

        #region Conversores
        /// <summary>
        /// convierte un número decimal a binario, de no ser posible retornará "Valor inválido".
        /// </summary>
        /// <param name="strNumero"></param>
        /// <returns></returns> de ser posible el numero convertido a binario, caso contrario "Valor inválido".
        public string DecimalBinario(string strNumero)
        {
            string resultado = "Valor Invalido";
            if (int.TryParse(strNumero, out int numeroEntero) && numeroEntero > -1)
            {
                resultado = "0";
                if (numeroEntero > 0)
                {
                    resultado = "";
                    int dato;
                    for (int i = 0; numeroEntero > 0; i++)
                    {
                        dato = numeroEntero % 2;
                        numeroEntero = numeroEntero / 2;
                        resultado = dato.ToString() + resultado;
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// convierte un número decimal a binario, de no ser posible retornará "Valor inválido".
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns> de ser posible el numero convertido a binario, caso contrario "Valor inválido".
        public string DecimalBinario(double numero)
        {
            return DecimalBinario(numero.ToString());
        }

        /// <summary>
        /// valida que se trate de un numero binario y lo convierte a decimal, en caso de no ser posible retornará "Valor inválido".
        /// </summary>
        /// <param name="binario"></param>
        /// <returns></returns> de ser posible el numero convertido a decimal, caso contrario "Valor inválido".
        public string BinarioDecimal(string binario)
        {
            string retorno = "Valor Invalido";
            if (EsBinario(binario))
            {
                retorno = "0";
                int resultado = 0;
                int posicion = binario.Length;
                foreach (char dato in binario)
                {
                    posicion--;
                    if (dato == '1')
                    {
                        resultado += (int)Math.Pow(2, posicion);
                    }
                }
                if (resultado > 0)
                {
                    retorno = resultado.ToString();
                }
            }
            return retorno;
        }
        #endregion
    }
}
