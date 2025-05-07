using Unimake.Formatters;
using Unimake.Validators;
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

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Validate_EmptyCnpj_Allowed_ReturnsTrue(string cnpj) =>
            Assert.True(CNPJValidator.Validate(ref cnpj, allowNullOrEmpty: true));

        [Theory]
        [InlineData("12ABC34501DE35")]
        [InlineData("12.ABC.345/01DE-35")]
        [InlineData("11222333000181")]
        [InlineData("11.222.333/0001-81")]
        public void Validate_ValidCnpjs_ReturnsTrue(string cnpj) =>
            Assert.True(CNPJValidator.Validate(ref cnpj));

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Validate_EmptyCnpj_NotAllowed_ReturnsFalse(string cnpj) =>
            Assert.False(CNPJValidator.Validate(ref cnpj, allowNullOrEmpty: false));

        [Theory]
        [InlineData("1234567891012")]
        [InlineData("AA.AAA.AAA/AAAA-AA")]
        [InlineData("11.111.111/1111-11")]
        [InlineData("11111111111111")]
        [InlineData("12ABC34501DE00")]
        [InlineData("11222333000100")]
        [InlineData("12.ABC.345/01DE-00")]
        [InlineData("11.222.333/0001-00")]
        [InlineData("11.@#!.333/0001-00")]
        public void Validate_InvalidCnpjs_ReturnsFalse(string cnpj) =>
            Assert.False(CNPJValidator.Validate(ref cnpj));

        #endregion Public Methods
    }
}