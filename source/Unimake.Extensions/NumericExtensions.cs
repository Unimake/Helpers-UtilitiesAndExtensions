namespace System
{
    /// <summary>
    /// Fornece métodos de extensão para retornar um valor padrão caso o valor numérico seja nulo ou zero.
    /// </summary>
    public static class NumericExtensions
    {
        #region Public Methods

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for <c>null</c> ou zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor decimal opcional a ser avaliado.</param>
        /// <param name="defaultValue">Valor decimal a ser retornado se <paramref name="value"/> for nulo ou zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static decimal GetDefaultIfNullOrZero(this decimal? value, decimal? defaultValue)
            => GetDefaultIfNullOrZero(value.GetValueOrDefault(0), defaultValue);

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor decimal a ser avaliado.</param>
        /// <param name="defaultValue">Valor decimal a ser retornado se <paramref name="value"/> for zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static decimal GetDefaultIfNullOrZero(this decimal value, decimal? defaultValue)
            => value == 0 ? defaultValue.GetValueOrDefault() : value;

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for <c>null</c> ou zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor double opcional a ser avaliado.</param>
        /// <param name="defaultValue">Valor double a ser retornado se <paramref name="value"/> for nulo ou zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static double GetDefaultIfNullOrZero(this double? value, double? defaultValue)
            => GetDefaultIfNullOrZero(value.GetValueOrDefault(0), defaultValue);

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor double a ser avaliado.</param>
        /// <param name="defaultValue">Valor double a ser retornado se <paramref name="value"/> for zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static double GetDefaultIfNullOrZero(this double value, double? defaultValue)
            => value == 0 ? defaultValue.GetValueOrDefault() : value;

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for <c>null</c> ou zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor long opcional a ser avaliado.</param>
        /// <param name="defaultValue">Valor long a ser retornado se <paramref name="value"/> for nulo ou zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static long GetDefaultIfNullOrZero(this long? value, long? defaultValue)
            => GetDefaultIfNullOrZero(value.GetValueOrDefault(0), defaultValue);

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor long a ser avaliado.</param>
        /// <param name="defaultValue">Valor long a ser retornado se <paramref name="value"/> for zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static long GetDefaultIfNullOrZero(this long value, long? defaultValue)
            => value == 0 ? defaultValue.GetValueOrDefault() : value;

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for <c>null</c> ou zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor int opcional a ser avaliado.</param>
        /// <param name="defaultValue">Valor int a ser retornado se <paramref name="value"/> for nulo ou zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static int GetDefaultIfNullOrZero(this int? value, int? defaultValue)
            => GetDefaultIfNullOrZero(value.GetValueOrDefault(0), defaultValue);

        /// <summary>
        /// Retorna <paramref name="defaultValue"/> se <paramref name="value"/> for zero; caso contrário, retorna o valor.
        /// </summary>
        /// <param name="value">Valor int a ser avaliado.</param>
        /// <param name="defaultValue">Valor int a ser retornado se <paramref name="value"/> for zero.</param>
        /// <returns>O valor original ou o valor padrão.</returns>
        public static int GetDefaultIfNullOrZero(this int value, int? defaultValue)
            => value == 0 ? defaultValue.GetValueOrDefault() : value;

        #endregion Public Methods
    }
}