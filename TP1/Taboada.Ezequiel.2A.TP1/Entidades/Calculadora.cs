using System;

namespace Entidades
{
    public static class Calculadora
    {
        /// <summary>
        ///  valida que el operador recibido sea +, -, / o *. Caso contrario retornará +.
        /// </summary>
        /// <param name="operador"></param> char a validar
        /// <returns></returns> operador valido
        private static char ValidarOperador(char operador)
        {
            if (operador != '+' && operador != '-' && operador != '/' && operador != '*')
            {
                operador = '+';
            }
            return operador;
        }

        /// <summary>
        ///  valida y realiza la operación pedida entre ambos números.
        /// </summary>
        /// <param name="numero1"></param> primer numero.
        /// <param name="numero2"></param> segundo numero.
        /// <param name="operador"></param> operacion matematica.
        /// <returns></returns> el valor de la operacion deseada.
        public static double Operar(Operando numero1, Operando numero2, char operador)
        {
            double resultado = 0;
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
    }
}
