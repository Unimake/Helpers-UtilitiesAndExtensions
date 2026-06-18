namespace Unimake.Primitives.Security.Credentials
{
    /// <summary>
    /// Representa as credenciais de um token de atualização para autenticação
    /// </summary>
    public struct RefreshTokenCredentials
    {
        #region Private Fields

        private string _refreshToken;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Token de atualização para autenticação
        /// </summary>
        public string RefreshToken { get => _refreshToken ?? (_refreshToken = string.Empty); set => _refreshToken = value; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Converte implicitamente uma string em <see cref="RefreshTokenCredentials"/>
        /// </summary>
        /// <param name="refreshToken">Token de atualização</param>
        public static implicit operator RefreshTokenCredentials(string refreshToken) => new RefreshTokenCredentials { RefreshToken = refreshToken ?? string.Empty };

        /// <summary>
        /// Converte implicitamente um <see cref="RefreshTokenCredentials"/> em string
        /// </summary>
        /// <param name="credentials">Credenciais do token de atualização</param>
        public static implicit operator string(RefreshTokenCredentials credentials) => credentials.RefreshToken ?? string.Empty;

        #endregion Public Methods
    }
}