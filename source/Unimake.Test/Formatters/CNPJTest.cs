using Unimake.Formatters;
using Xunit;

namespace Unimake.Test.Formatters
{
    public class CNPJTest
    {
        #region Public Methods

        [Theory]
        [InlineData("27035298000121", "27.035.298/0001-21", false)]
        [InlineData("035298000121", "00.035.298/0001-21", false)]
        [InlineData("AABCD298000121", "AA.BCD.298/0001-21", false)]
        [InlineData("27.035.298/0001-21", "27035298000121", true)]
        [InlineData("00.035.298/0001-21", "00035298000121", true)]
        [InlineData("035.298/0001-21", "00035298000121", true)]
        [InlineData("AA.BCD.298/0001-21", "AABCD298000121", true)]
        public void CNPJFormat(string cnpj, string expected, bool returnWithoutFormatting)
        {
            cnpj = CNPJFormatter.Format(cnpj, returnWithoutFormatting);
            Assert.Equal(expected, cnpj);
        }

        #endregion Public Methods
    }
}