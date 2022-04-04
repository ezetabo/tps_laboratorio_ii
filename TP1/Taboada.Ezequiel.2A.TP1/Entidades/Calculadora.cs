using System;

namespace Entidades
{
    public static class Calculadora
    {
        private static char ValidarOperador(char operador)
        {
            if (operador != '+' && operador != '-' && operador != '/' && operador != '*')
            {
                operador = '+';
            }
            return operador;
        }
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
