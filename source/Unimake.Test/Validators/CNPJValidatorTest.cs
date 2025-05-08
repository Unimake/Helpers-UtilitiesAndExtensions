using Unimake.Validators;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Validators
{
    public class CNPJValidatorTest
    {
        #region Public Methods

        [Theory]
        [InlineData("AA.AAA.AAA/AAAA-AA")]
        [InlineData("11.111.111/1111-11")]
        [InlineData("12ABC34501DE00")]
        [InlineData("12.ABC.345/01DE-00")]
        [InlineData("11.222.333/0001-00")]
        [InlineData("11.@#!.333/0001-00")]
        [InlineData("")]
        [InlineData("          ")]
        [InlineData("    124436524    569 1 6      ")]
        [InlineData("3632354975824659")]
        [InlineData("69826152446241")]
        [InlineData("363292")]
        [InlineData("11111111111111")]
        [InlineData("111111111")]
        [InlineData("11111RGT1111")]
        [InlineData("3632FS9QWDA#.,D659")]
        [InlineData("LGKSJFHDGEOPFE")]
        [InlineData(null)]
        public void InvalidCNPJ(string cnpj) =>
            Assert.False(CNPJValidator.Validate(ref cnpj, false));

        [Theory]
        [InlineData("41.653.207/0001-42")]
        [InlineData(" ")]
        [InlineData("28.4 65.0 42/ 0001- 17")]
        [InlineData("55.17 7.579/0001-54")]
        [InlineData("      ")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ValidAndEmptyCNPJ(string cnpj) =>
            Assert.True(CNPJValidator.Validate(ref cnpj, true));

        [Theory]
        [InlineData("41.653.207/0001-42")]
        [InlineData("91.242.375/0001-13")]
        [InlineData("59.357.854/0001-72")]
        [InlineData("28.465.042/0001-17")]
        [InlineData("55.177.579/0001-54")]
        [InlineData(" 5 5.1 77.5     79/0   00 1-54 ")]
        [InlineData("36.182.724/0001-40")]
        [InlineData("12ABC34501DE35")]
        [InlineData("12.ABC.345/01DE-35")]
        [InlineData("11222333000181")]
        [InlineData("11.222.333/0001-81")]
        public void ValidAndFormatted(string cnpj)
        {
            Assert.True(CNPJValidator.Validate(ref cnpj, true, true));
            Assert.Equal(Formatters.CNPJFormatter.Format(cnpj), cnpj);
        }

        [Theory]
        [InlineData("41.653.207/0001-42")]
        [InlineData("91.242.375/0001-13")]
        [InlineData("59.357.854/0001-72")]
        [InlineData("28.465.042/0001-17")]
        [InlineData("55.177.579/0001-54")]
        [InlineData(" 5 5.1 77.5     79/0   00 1-54 ")]
        [InlineData("36.182.724/0001-40")]
        [InlineData("12ABC34501DE35")]
        [InlineData("12.ABC.345/01DE-35")]
        [InlineData("11222333000181")]
        [InlineData("11.222.333/0001-81")]
        public void ValidCNPJ(string cnpj) =>
            Assert.True(CNPJValidator.Validate(ref cnpj, false));

        #endregion Public Methods
    }
}