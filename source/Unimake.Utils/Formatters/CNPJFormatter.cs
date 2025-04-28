namespace Unimake.Formatters
{
    public abstract class CNPJFormatter
    {
        #region Private Constructors

        private CNPJFormatter()
        {
        }

        #endregion Private Constructors

        #region Public Methods

        /// <summary>
        /// Formata o CNPJ, podendo tratar também caracteres alfanuméricos.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser formatado</param>
        /// <param name="returnWithoutFormatting">Se verdadeiro, retorna apenas os caracteres limpos, sem formatação</param>
        /// <returns>CNPJ formatado ou limpo conforme opção</returns>
        public static string Format(string cnpj, bool returnWithoutFormatting = false)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return string.Empty;

            // Remove tudo que não seja letra ou número
            string cleaned = System.Text.RegularExpressions.Regex.Replace(cnpj, "[^a-zA-Z0-9]", "");

            if (returnWithoutFormatting)
            {
                return cleaned;
            }

            // Detecta se é alfanumérico (contém letras)
            bool isAlphanumeric = System.Text.RegularExpressions.Regex.IsMatch(cleaned, "[a-zA-Z]");

            if (isAlphanumeric)
            {
                // Trata como CNPJ alfanumérico
                var letrasNumeros = cleaned.Length >= 12 ? cleaned.Substring(0, 12) : cleaned.PadRight(12, '0');
                var numeros = cleaned.Length > 12 ? cleaned.Substring(12) : "";
                numeros = System.Text.RegularExpressions.Regex.Replace(numeros, "[^0-9]", ""); // Só números
                numeros = numeros.PadRight(2, '0'); // Garante 2 dígitos

                return $"{letrasNumeros.Substring(0, 2)}.{letrasNumeros.Substring(2, 3)}.{letrasNumeros.Substring(5, 3)}/{letrasNumeros.Substring(8, 4)}-{numeros.Substring(0, 2)}";
            }
            else
            {
                // Trata como CNPJ numérico comum
                string numbersOnly = cleaned.PadLeft(14, '0');
                return StringFormatter.Format(numbersOnly, @"00\.000\.000/0000-00");
            }
        }

        #endregion Public Methods
    }
}