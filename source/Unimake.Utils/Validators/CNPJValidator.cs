using System.Linq;
using System.Text.RegularExpressions;

namespace Unimake.Validators
{
    public abstract class CNPJValidator
    {
        #region Private Constructors

        private CNPJValidator()
        {
        }

        #endregion Private Constructors

        #region Private Methods

        /// <summary>
        /// Faz a validação do CNPJ
        /// </summary>
        /// <param name="cnpj">valor para validação</param>
        /// <returns></returns>
        private static bool Validate(string cnpj)
        {
            var baseCnpj = cnpj.Substring(0, 12);
            var dvInformado = cnpj.Substring(12, 2);

            if (baseCnpj.Length != 12)
                return false;

            var dvCalculado = CalculateDigitsVerifiers(baseCnpj);
            return dvCalculado == dvInformado;
        }

        private static string CalculateDigitsVerifiers(string baseCnpj)
        {
            var valores = baseCnpj.Select(GetValueForCalculation).ToArray();

            var primeiroDV = CalculateDV(valores, new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });

            // Adiciona o primeiro dígito verificador ao final
            var valoresComDV1 = valores.Append(primeiroDV).ToArray();
            var segundoDV = CalculateDV(valoresComDV1, new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });

            return $"{primeiroDV}{segundoDV}";
        }

        private static int GetValueForCalculation(char caractere)
        {
            caractere = char.ToUpper(caractere);
            var ascii = (int)caractere;

            return ascii - 48;
        }

        private static int CalculateDV(int[] valores, int[] pesos)
        {
            var soma = 0;

            for (int i = 0; i < valores.Length; i++)
                soma += valores[i] * pesos[i];

            var resto = soma % 11;
            return (resto < 2) ? 0 : 11 - resto;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Analisa o CNPJ informado e verifica se é válido.
        /// <para>Se válido, atualiza o parâmetro <paramref name="cnpj"/> para o valor válido e sem formatação.</para>
        /// <para>Para formatar, pode-se usar o Unimake.Formatters.CNPJFormatter</para>
        /// <para>Este método remove qualquer caractere diferente de número para validar e devolve o CNPJ válido no parâmetro <paramref name="cnpj"/></para>
        /// </summary>
        /// <param name="cnpj">CNPJ para validação.</param>
        /// <param name="allowNullOrEmpty">Se true, permite nulo ou em branco</param>
        /// <param name="formatted">Se verdadeiro e o <paramref name="cnpj"/>  for válido, devolvo o <paramref name="cnpj"/> formatado.</param>
        /// <returns>Verdadeiro, se o CNPJ for válido ou se <paramref name="allowNullOrEmpty"/>
        /// for verdadeiro e o o valor informado em <paramref name="cpf"/> for nulo ou vazio</returns>
        public static bool Validate(ref string cnpj, bool allowNullOrEmpty = true, bool formatted = false)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                if (allowNullOrEmpty)
                {
                    cnpj = "";
                    return true;
                }

                return false;
            }

            var cleanCnpj = Regex.Replace(cnpj, "[^a-zA-Z0-9]", "");

            //Se todos os caracteres forem iguais, isso indica que o CNPJ é inválido
            //se tamanho for diferente de 14, é falso
            if (cleanCnpj.Length != 14 || cleanCnpj.All(c => c == cleanCnpj[0]))
                return false;

            if (!Validate(cleanCnpj))
                return false;

            cnpj = formatted ? Formatters.CNPJFormatter.Format(cleanCnpj) : cleanCnpj;

            return true;
        }

        #endregion Public Methods
    }
}