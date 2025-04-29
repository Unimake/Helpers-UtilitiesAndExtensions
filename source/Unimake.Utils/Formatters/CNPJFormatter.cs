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
            if(string.IsNullOrWhiteSpace(cnpj))
            {
                return string.Empty;
            }

            // Remove tudo que não seja letra ou número
            string cleaned = System.Text.RegularExpressions.Regex.Replace(cnpj, "[^a-zA-Z0-9]", "");

            // Garante que tenha tamanho 14
            cleaned = cleaned.PadLeft(14, '0');

            if(returnWithoutFormatting)
            {
                return cleaned;
            }

            // Formata
            // 00.000.000/0000-00
            return $"{cleaned.Substring(0, 2)}.{cleaned.Substring(2, 3)}.{cleaned.Substring(5, 3)}/{cleaned.Substring(8, 4)}-{cleaned.Substring(12, 2)}";
        }

        #endregion Public Methods
    }
}