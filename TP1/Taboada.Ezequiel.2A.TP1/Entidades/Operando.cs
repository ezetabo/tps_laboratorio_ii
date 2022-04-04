using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Operando
    {
        private double numero;
        private string Numero
        {
            set
            {
                numero = ValidarOperando(value);
            }
        }

        #region Constructores
        public Operando() : this(0)
        {

        }
        public Operando(double numero)
        {
            this.numero = numero;
        }
        public Operando(string strNumero)
        {
            Numero = strNumero;
        }
        #endregion

        #region Validaciones
        private double ValidarOperando(string strNumero)
        {
            if (!double.TryParse(strNumero, out double numero))
            {
                numero = 0;
            }
            return numero;
        }
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
        public static double operator +(Operando n1, Operando n2)
        {
            return n1.numero + n2.numero;
        }
        public static double operator -(Operando n1, Operando n2)
        {
            return n1.numero - n2.numero;
        }
        public static double operator *(Operando n1, Operando n2)
        {
            return n1.numero * n2.numero;
        }
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
        public string DecimalBinario(string strNumero)
        {
            string resultado = "Valor Invalido";
            if (int.TryParse(strNumero, out int numeroEntero) && numeroEntero > -1)
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
            return resultado;
        }
        public string DecimalBinario(double numero)
        {
            return DecimalBinario(numero.ToString());
        }
        public string BinarioDecimal(string binario)
        {
            string retorno = "Valor Invalido";
            if (EsBinario(binario))
            {
                int resultado = 0;
                int posicion = binario.Length;
                foreach (char dato in binario)
                {
                    posicion--;
                    if (dato == '1')
                    {
                        resultado += (int)Math.Pow(2, posicion);
                        retorno = resultado.ToString();
                    }
                }
            }
            return retorno;
        }
        #endregion

    }
}
