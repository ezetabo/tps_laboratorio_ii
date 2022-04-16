using System;
using System.Collections.Generic;

namespace Entidades
{    
    public static class Calculadora
    {
        #region ValidarOperador

        /// <summary>
        ///  valida que el operador recibido sea +, -, / o *. Caso contrario retornará +.
        /// </summary>
        /// <param name="operador"> char a validar </param> 
        /// <returns> operador valido </returns> 
        private static char ValidarOperador(char operador)
        {
            if (operador != '+' && operador != '-' && operador != '/' && operador != '*')
            {
                operador = '+';
            }
            return operador;
        }

        #endregion

        #region Operar

        /// <summary>
        ///  valida y realiza la operación pedida entre ambos números.
        /// </summary>
        /// <param name="numero1"> primer numero </param> 
        /// <param name="numero2"> segundo numero </param> 
        /// <param name="operador">  operacion matematica </param>
        /// <returns> el valor de la operacion deseada </returns> 
        public static double Operar(Operando numero1, Operando numero2, char operador)
        {
            double resultado = double.NaN;
            if (numero1 is not null && numero2 is not null)
            {
                switch (ValidarOperador(operador))
                {
                    case '/':
                        resultado = numero1 / numero2;
                        break;
                    case '*':
                        resultado = numero1 * numero2;
                        break;
                    case '-':
                        resultado = numero1 - numero2;
                        break;
                    default:
                        resultado = numero1 + numero2;
                        break;
                }
            }
            return resultado;
        }

        #endregion
    }
}
