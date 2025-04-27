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
        /// Formata o CNPJ
        /// </summary>
        /// <param name="cnpj">cnpj a ser formatado</param>
        /// <param name="returnOnlyNumbers">Se verdadeiro, retorna apenas os números sem formatação</param>
        /// <returns>cnpj formatado</returns>
        public static string Format(string cnpj, bool returnOnlyNumbers = false)
        {
            cnpj = UConvert.OnlyNumbers(cnpj, "-.,/").ToString().PadLeft(14, '0');

            if (returnOnlyNumbers)
            {
                return cnpj;
            }

            return StringFormatter.Format(cnpj, @"00\.000\.000/0000-00");
        }

        /// <summary>
        /// Formata o CNPJ que contem caracteres alfanuméricos
        /// </summary>
        /// <param name="cnpj">cnpj a ser formatado</param>
        /// <param name="returnOnlyCharacters ">Se verdadeiro, retorna apenas os caracteres sem formatação</param>
        /// <returns>cnpj formatado</returns>
        public static string FormatAlphanumeric(string cnpj, bool returnOnlyCharacters = false)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return string.Empty;

            // Remove tudo que não seja letra ou número
            cnpj = System.Text.RegularExpressions.Regex.Replace(cnpj, "[^a-zA-Z0-9]", "");

            if (returnOnlyCharacters)
                return cnpj; // Se quiser só o texto limpo, retorna aqui

            // Separa primeiros 12 caracteres (alfanuméricos) e últimos 2 (só números)
            var letrasNumeros = cnpj.Length >= 12 ? cnpj.Substring(0, 12) : cnpj.PadRight(12, '0');
            var numeros = cnpj.Length > 12 ? cnpj.Substring(12) : "";
            numeros = System.Text.RegularExpressions.Regex.Replace(numeros, "[^0-9]", ""); // Só números
            numeros = numeros.PadRight(2, '0'); // Garante que tenha 2 dígitos

            // Aplica a máscara manualmente
            return $"{letrasNumeros.Substring(0, 2)}.{letrasNumeros.Substring(2, 3)}.{letrasNumeros.Substring(5, 3)}/{letrasNumeros.Substring(8, 4)}-{numeros.Substring(0, 2)}";
        }

        #endregion Public Methods
    }
}