using System;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.ExtensionsTest;

public class DateTimeExtensionsTest
{
    #region Public Methods

    [Fact]
    public void ToMidnightUtc_ComFusoInformado_DeveUsarOFusoInformado()
    {
        // UTC+0 (GMT Standard Time, sem horário de verão em janeiro)
        var value = new DateTime(2026, 1, 10, 15, 0, 0, DateTimeKind.Utc);

        var result = value.ToMidnightUtc(TimeZoneId.GMTStandardTime);

        Assert.Equal(new DateTime(2026, 1, 10, 0, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_ComHorarioDeVeraoBrasileiro_DeveConsiderarOffsetMenos2()
    {
        // 2018-01-10 (horário de verão vigente no Brasil, UTC-2)
        var value = new DateTime(2018, 1, 10, 15, 0, 0, DateTimeKind.Utc);

        var result = value.ToMidnightUtc();

        Assert.Equal(new DateTime(2018, 1, 10, 2, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_ComInicioDeHorarioDeVeraoSemMeiaNoite_DeveRetornarPrimeiraHoraValida()
    {
        // 2018-11-04: o horário de verão começou à 00:00 (pulou para 01:00), então 00:00 não existe.
        var value = new DateTime(2018, 11, 4, 12, 0, 0, DateTimeKind.Utc);

        var result = value.ToMidnightUtc();

        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(new DateTime(2018, 11, 4, 3, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_ComKindLocal_DeveConverterParaOFusoAntesDeDefinirODia()
    {
        var utc = new DateTime(2026, 9, 16, 2, 30, 0, DateTimeKind.Utc);
        var local = utc.ToLocalTime();

        Assert.Equal(utc.ToMidnightUtc(), local.ToMidnightUtc());
    }

    [Fact]
    public void ToMidnightUtc_ComKindUnspecified_DeveConsiderarDataNoFusoInformado()
    {
        var value = new DateTime(2026, 9, 16, 22, 0, 0, DateTimeKind.Unspecified);

        var result = value.ToMidnightUtc();

        Assert.Equal(new DateTime(2026, 9, 16, 3, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_ComKindUtcAntesDaMeiaNoiteDeBrasilia_DeveRetornarDiaAnterior()
    {
        // 02:00Z = 23:00 do dia 15 em Brasília
        var value = new DateTime(2026, 9, 16, 2, 0, 0, DateTimeKind.Utc);

        var result = value.ToMidnightUtc();

        Assert.Equal(new DateTime(2026, 9, 15, 3, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_ComKindUtcAposAMeiaNoiteDeBrasilia_DeveRetornarMesmoDia()
    {
        // 03:00Z = 00:00 do dia 16 em Brasília
        var value = new DateTime(2026, 9, 16, 3, 0, 0, DateTimeKind.Utc);

        var result = value.ToMidnightUtc();

        Assert.Equal(new DateTime(2026, 9, 16, 3, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_ComViradaDeAno_DeveRetornarDiaCorreto()
    {
        // 01/01/2027 01:00Z = 31/12/2026 22:00 em Brasília
        var value = new DateTime(2027, 1, 1, 1, 0, 0, DateTimeKind.Utc);

        var result = value.ToMidnightUtc();

        Assert.Equal(new DateTime(2026, 12, 31, 3, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ToMidnightUtc_DeveSempreRetornarKindUtc()
    {
        foreach(var kind in new[] { DateTimeKind.Utc, DateTimeKind.Local, DateTimeKind.Unspecified })
        {
            var result = new DateTime(2026, 9, 16, 12, 0, 0, kind).ToMidnightUtc();

            Assert.Equal(DateTimeKind.Utc, result.Kind);
        }
    }

    #endregion Public Methods
}