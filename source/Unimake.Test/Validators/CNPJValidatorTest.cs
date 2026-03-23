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
        [InlineData("GA.ZH.NN0/0001-06")]
        [InlineData("GA.ZBH.NN0/BGYC-25")]
        [InlineData("TS.L7X.85Z/0001-00")]
        [InlineData("ts.l3x.85z/aa0t-60")]
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
        // Abaixo, foram gerados pelo site https://servicos.receitafederal.gov.br/servico/cnpj-alfa/simular
        [InlineData("J3.S4H.0CE/0001-50")]
        [InlineData("J3.S4H.0CE/NATM-86")]
        [InlineData("J3.S4H.0CE/NMKH-88")]
        [InlineData("J3.S4H.0CE/M0MG-48")]
        [InlineData("V2.C1N.5EW/0001-70")]
        [InlineData("C2.EAA.41X/0001-40")]
        [InlineData("TX.G93.X6K/0001-03")]
        [InlineData("CP.RTK.VM7/0001-87")]
        [InlineData("3E.MNT.W39/0001-99")]
        [InlineData("TT.GPG.Y4A/0001-96")]
        [InlineData("N7.LBS.K5H/0001-24")]
        [InlineData("JJ.469.CCC/0001-81")]
        [InlineData("JJ.469.CCC/S1Y8-02")]
        [InlineData("JJ.469.CCC/NEW5-12")]
        [InlineData("JJ.469.CCC/93JW-64")]
        [InlineData("18.RVS.KH5/0001-08")]
        [InlineData("18.RVS.KH5/R634-06")]
        [InlineData("LV.E9R.2VZ/0001-69")]
        [InlineData("LV.E9R.2VZ/XK51-27")]
        [InlineData("GA.ZBH.NN0/0001-06")]
        [InlineData("GA.ZBH.NN0/BGXC-25")]
        [InlineData("TS.L3X.85Z/0001-00")]
        [InlineData("TS.L3X.85Z/AZ0T-60")]
        [InlineData("5S.1YC.GHM/0001-80")]
        [InlineData("KM.HL9.JK1/0001-46")]
        [InlineData("TW.H7M.GLH/0001-19")]
        [InlineData("M6.HZG.GNV/0001-00")]
        [InlineData("KN.AEK.REC/0001-71")]
        [InlineData("KN.AEK.REC/G2EZ-36")]
        [InlineData("KN.AEK.REC/V916-09")]
        [InlineData("KN.AEK.REC/MLPP-08")]
        [InlineData("KN.AEK.REC/M9Z3-40")]
        [InlineData("PC.B1Z.P5J/0001-33")]
        [InlineData("TR.HE8.1HT/0001-65")]
        [InlineData("TR.HE8.1HT/7YRH-05")]
        [InlineData("TR.HE8.1HT/YMGA-20")]
        [InlineData("TR.HE8.1HT/EERV-44")]
        [InlineData("TR.HE8.1HT/TXX1-88")]
        public void ValidCNPJ(string cnpj)
        {
            Assert.True(CNPJValidator.Validate(ref cnpj, false));
            Assert.True(CNPJValidator.Validate(cnpj, false));
        }

        #endregion Public Methods
    }
}