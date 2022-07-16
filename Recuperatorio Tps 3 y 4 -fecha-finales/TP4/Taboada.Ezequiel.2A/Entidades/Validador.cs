using System.Text;

namespace Entidades
{
    public static class Validador
    {
        /// <summary>
        /// valida que la cadena recibida sea numerica y del tipo UINT.
        /// </summary>
        /// <param name="strNumero"></param> cadena a verificar.
        /// <returns></returns> true si es numerica y del tipo UINT o false en caso contrario.
        public static bool EsUInt(string strNumero)
        {
            bool UInt = true;

            foreach (char item in strNumero)
            {

                if (!char.IsDigit(item))
                {
                    UInt = false;
                    break;
                }

            }
            return UInt;
        }

        /// <summary>
        /// Recibe una cadena y devuelve una copia con formato upperCamelCase
        /// </summary>
        /// <param name="texto"> cadena a formatear </param>
        /// <returns> Una copia de la cadena con formato upperCamelCase </returns>
        public static string FormatoNombre(string texto)
        {
            StringBuilder formato = new StringBuilder(texto.ToLower());

            for (int i = 0; i < formato.Length; i++)
            {
                if (i == 0)
                {
                    formato[i] = char.ToUpper(formato[i]);
                }
                if (formato[i] == ' ')
                {
                    i++;
                    formato[i] = char.ToUpper(formato[i]);
                }

            }
            return formato.ToString();
        }

        public static bool Es_DNI_Telefono(string dato)
        {
            return dato.Length == 8 && Validador.EsUInt(dato);
        }
        public static bool EsSoloLetras(string strNumero)
        {
            bool letra = true;

            foreach (char item in strNumero)
            {                
                if (!char.IsLetter(item) && !char.IsWhiteSpace(item))
                {
                    letra = false;
                    break;
                }

            }
            return letra;
        }

        public static bool EsPatente(string patente)
        {
            bool es = true;
            if (patente.Length != 7 && patente.Length != 9)
            {
                es = false;
            }
            else
            {
                if (patente.Length == 7)
                {
                    for (int i = 0; i < patente.Length; i++)
                    {
                        if (i == 0 || i == 1 || i == 5 || i == 6)
                        {
                            if (!char.IsLetter(patente[i]))
                            {
                                es = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!char.IsDigit(patente[i]))
                            {
                                es = false;
                                break;
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < patente.Length; i++)
                    {
                        if (i == 2 || i == 6)
                        {
                            continue;
                        }
                        if (i == 0 || i == 1 || i == 7 || i == 8)
                        {
                            if (!char.IsLetter(patente[i]))
                            {
                                es = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!char.IsDigit(patente[i]))
                            {
                                es = false;
                                break;
                            }
                        }

                    }
                }
            }
            return es;
        }

        public static string FormatoPatente(string patente)
        {
            string formato = patente.Length == 7 ? patente.Substring(0, 2) + " " + patente.Substring(2, 3) + " " + patente[5..] : patente;
            return formato.ToUpper();
        }

        public static bool TryParseNombre(string entrada, out string salida)
        {
            salida = EsSoloLetras(entrada) ? entrada : null;
            return salida is not null;
        }

        public static bool TryParsePatente(string entrada, out string salida)
        {
            salida = EsPatente(entrada) ? FormatoPatente(entrada) : null;
            return salida is not null;
        }
    }
}

