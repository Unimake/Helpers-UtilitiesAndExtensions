using System.Diagnostics;
using Unimake.Formatters;
using Xunit;

namespace Unimake.Test.Formatters
{
    public class CNPJTest
    {
        #region Public Methods

        [Fact]
        public void CNPJFormat()
        {
            Debug.WriteLine(CNPJFormatter.Format("27035298000121")); //Retorna CNPJ numérico formatado
            Debug.WriteLine(CNPJFormatter.Format("AABCD298000121")); //Retorna CNPJ Alfanumérico formatado
            Debug.WriteLine(CNPJFormatter.Format("27.035.298/0001-21", true)); //Retorna CNPJ sem formatação
        }

        #endregion Public Methods
    }
}