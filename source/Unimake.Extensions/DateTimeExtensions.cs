using System.Collections.Generic;
using System.Globalization;

namespace System
{
    /// <summary>
    /// Extensões para os tipos DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Calcula a data da Páscoa para um determinado ano usando o algoritmo de Gauss.
        /// </summary>
        /// <param name="year">Ano que é para calcular a data da Páscoa</param>
        public static DateTime CalculateEaster(this DateTime dateTime)
        {
            var year = dateTime.Year;
            var a = year % 19;
            var b = year / 100;
            var c = year % 100;
            var d = b / 4;
            var e = b % 4;
            var f = (b + 8) / 25;
            var g = (b - f + 1) / 3;
            var h = (19 * a + b - d - g + 15) % 30;
            var i = c / 4;
            var k = c % 4;
            var l = (32 + 2 * e + 2 * i - h - k) % 7;
            var m = (a + 11 * h + 22 * l) / 451;
            var mes = (h + l - 7 * m + 114) / 31;
            var dia = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(year, mes, dia);
        }

        /// <summary>
        /// Retorna a diferença entre as datas
        /// </summary>
        /// <param name="value">primeira data da comparação</param>
        /// <param name="date">segunda data da comparação</param>
        /// <returns>timespan contendo as diferenças entre as datas</returns>
        public static TimeSpan DateDiff(this DateTime value, DateTime date) => value.Subtract(date);

        /// <summary>
        /// Retorna a data e toma como base a última hora do dia
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        /// <summary>
        /// Calcula o primeiro dia do mês da data informada
        /// </summary>
        /// <param name="dateTime">Data</param>
        /// <returns>Data do primeiro dia do mês</returns>
        /// <remarks></remarks>
        public static DateTime FirstDayMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1);

        /// <summary>
        /// Retorna qual é o primeiro dia da semana de acordo com a cultura
        /// </summary>
        /// <param name="date">Não é utilizado para nada, somente para atender a extension</param>
        /// <returns>Primeiro dia da semana de acordo com a cultura</returns>
        public static DayOfWeek FirstDayOfWeek(this DateTime date) => CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        /// <summary>
        /// Retorna o dia do mês do primeiro dia da semana de uma data especificada
        /// </summary>
        ///<param name="date">Data base para pesquisar o primeiro dia</param>
        public static DateTime FirstDayOfWeekDate(this DateTime date)
        {
            var firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var firstDayInWeek = date.Date;

            while(firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        /// <summary>
        /// Retorna a lista de feriados fixos no Brasil
        /// </summary>
        public static HashSet<DateTime> FixedHolidays(this DateTime _)
        {
            var year = DateTime.Now.Year;

            return new HashSet<DateTime>
            {
                new DateTime(year, 1, 1),  // Confraternização Universal
                new DateTime(year, 4, 21), // Tiradentes
                new DateTime(year, 5, 1),  // Dia do Trabalho
                new DateTime(year, 9, 7),  // Independência do Brasil
                new DateTime(year, 10, 12),// Nossa Senhora Aparecida
                new DateTime(year, 11, 2), // Finados
                new DateTime(year, 11, 15),// Proclamação da República
                new DateTime(year, 12, 25) // Natal
            };
        }

        /// <summary>
        /// Retorna o fuso horário da aplicação
        /// </summary>
        /// <param name="date">Data para pesquisa do fuso</param>
        /// <param name="timezoneId">Se nada for informado, será recuperado o timezone "E. South America Standard Time"</param>
        /// <returns></returns>
        public static string GetTimezone(this DateTime date, TimeZoneId timezoneId = null)
        {
            if(timezoneId == null)
            {
                timezoneId = TimeZoneId.ESouthAmericaStandardTime;
            }

            var timespan = TimeZoneInfo.FindSystemTimeZoneById(timezoneId).BaseUtcOffset;
            var timezone = timespan.Hours;

            if(date.IsDaylightSavingTime())
            {
                timezone += 1;
            }

            return $"{timezone:D2}:{timespan.Seconds:D2}";
            ;
        }

        /// <summary>
        /// Verifica se uma data é um dia útil no Brasil (exclui finais de semana e feriados).
        /// </summary>
        /// <param name="dateTime">Data a ser verificada</param>
        public static bool IsBusinessDay(this DateTime dateTime)
        {
            if(IsWeekend(dateTime) || IsHoliday(dateTime))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compara se as datas são iguais desconsiderando as horas
        /// </summary>
        /// <param name="value">valor de comparação um</param>
        /// <param name="compareValue">Valor que será comparado se é meior que o valor 1</param>
        /// <returns></returns>
        public static bool IsEqual(this DateTime value, DateTime compareValue)
        {
            value = Convert.ToDateTime(value.ToShortDateString());
            compareValue = Convert.ToDateTime(compareValue.ToShortDateString());
            return value == compareValue;
        }

        /// <summary>
        /// Compara se a data é maior que a data de comparação desconsiderando as horas
        /// </summary>
        /// <param name="value">valor de comparação um</param>
        /// <param name="compareValue">Valor que será comparado se é maior que o valor 1</param>
        /// <returns></returns>
        public static bool IsGreaterThan(this DateTime value, DateTime compareValue)
        {
            value = Convert.ToDateTime(value.ToShortDateString());
            compareValue = Convert.ToDateTime(compareValue.ToShortDateString());
            return value > compareValue;
        }

        public static bool IsGreaterThanOrEqualTo(this DateTime value, DateTime compareValue)
        {
            value = Convert.ToDateTime(value.ToShortDateString());
            compareValue = Convert.ToDateTime(compareValue.ToShortDateString());
            return value >= compareValue;
        }

        /// <summary>
        /// Verifica se a data informada é um feriado fixo ou móvel.
        /// </summary>
        /// <param name="dateTime">Data a ser verificada</param>
        public static bool IsHoliday(this DateTime dateTime) => FixedHolidays(dateTime).Contains(dateTime.Date) || MovableHolidays(dateTime).Contains(dateTime.Date);

        /// <summary>
        /// retorna true se a data informada for um ano bissexto
        /// </summary>
        /// <param name="value">data válida</param>
        /// <returns></returns>
        public static bool IsLeapYear(this DateTime value) => DateTime.IsLeapYear(value.Year);

        /// <summary>
        /// Compara se a data é menor que a data de comparação desconsiderando as horas
        /// </summary>
        /// <param name="value">valor de comparação um</param>
        /// <param name="compareValue">Valor que será comparado se é menor que o valor 1</param>
        /// <param name="equal">Se true, compara usando menor igual a</param>
        /// <returns></returns>
        public static bool IsLessThan(this DateTime value, DateTime compareValue)
        {
            value = Convert.ToDateTime(value.ToShortDateString());
            compareValue = Convert.ToDateTime(compareValue.ToShortDateString());
            return value < compareValue;
        }

        /// <summary>
        /// Compara se a data é menor que a data de comparação desconsiderando as horas
        /// </summary>
        /// <param name="value">valor de comparação um</param>
        /// <param name="compareValue">Valor que será comparado se é menor que o valor 1</param>
        /// <returns></returns>
        public static bool IsLessThanOrEqualTo(this DateTime value, DateTime compareValue)
        {
            value = Convert.ToDateTime(value.ToShortDateString());
            compareValue = Convert.ToDateTime(compareValue.ToShortDateString());
            return value <= compareValue;
        }

        /// <summary>
        /// Retorna true se a data for válida
        /// </summary>
        /// <param name="value">data</param>
        /// <returns>true se a data for válida</returns>
        public static bool IsValid(this DateTime value)
        {
            if(value <= DateTime.MinValue)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// retorna true se a data for válida
        /// </summary>
        /// <param name="value">data</param>
        /// <returns>true se a data for válida</returns>
        public static bool IsValid(this DateTime? value) => IsValid(value.GetValueOrDefault(DateTime.MinValue));

        /// <summary>
        /// Retorna se a data informada é um final de semana (sábado ou domingo).
        /// </summary>
        /// <param name="dateTime">Data a ser verificada</param>
        public static bool IsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Calcula o último dia do mês da data informada
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Data do ultimo dia do mês</returns>
        /// <remarks></remarks>
        public static DateTime LastDayMonth(this DateTime date) => LastDayMonth(date, date.Month, date.Year);

        /// <summary>
        /// Calcula o último dia do mês do mês informada
        /// </summary>
        /// <param name="date">Data</param>
        /// <param name="month">mes para pesquisar</param>
        /// <param name="year">Ano, se não informado usa o ano atual</param>
        /// <returns>Data do ultimo dia do mês</returns>
        /// <remarks></remarks>
        public static DateTime LastDayMonth(this DateTime date, int month, int year = 0)
        {
            if(year == 0)
            {
                year = date.Year;
            }

            return new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Retorna a lista de feriados móveis baseados na Páscoa.
        /// </summary>
        /// <param name="dateTime">Informar um data para que verifiquemos os feriados móveis do ano da data informada</param>
        public static HashSet<DateTime> MovableHolidays(this DateTime dateTime)
        {
            var easter = CalculateEaster(dateTime);

            return new HashSet<DateTime>
            {
                easter.AddDays(-47), // Carnaval (segunda-feira)
                easter.AddDays(-46), // Carnaval (terça-feira)
                easter.AddDays(-2),  // Sexta-feira Santa
                easter,              // Páscoa
                easter.AddDays(60)   // Corpus Christi
            };
        }

        /// <summary>
        /// Retorna a data e toma como base a primeira hora do dia
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        /// <summary>
        /// Converte um valor Unix epoch time para <see cref="DateTime"/>
        /// </summary>
        /// <param name="unixTimeStamp">Unix <see cref="DateTime"/> em segundos</param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        /// <summary>
        /// Calcula a semana da data informada
        /// </summary>
        /// <param name="dataTime">Data que é para calcular a semana</param>
        /// <returns>O Número da semana do mês da data informada</returns>
        public static int WeekOfDate(this DateTime dataTime)
        {
            var firstDayOfMonth = FirstDayMonth(dataTime);
            var calendar = CultureInfo.CurrentCulture.Calendar;

            var lastWeek = calendar.GetWeekOfYear(dataTime, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var firstWeek = calendar.GetWeekOfYear(firstDayOfMonth, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            return lastWeek - firstWeek + 1;
        }

        /// <summary>
        /// Calcula a quantidade de semanas que tem no mês
        /// </summary>
        /// <param name="dateTime">Data</param>
        /// <returns>Quantidade de semanas</returns>
        /// <remarks></remarks>
        public static int WeekOfMonth(this DateTime dateTime)
        {
            var lastDayMonth = LastDayMonth(dateTime);
            return WeekOfDate(lastDayMonth);
        }

        #endregion Public Methods
    }
}