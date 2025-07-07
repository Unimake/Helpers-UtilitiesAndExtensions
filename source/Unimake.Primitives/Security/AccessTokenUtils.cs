using System.IdentityModel.Tokens.Jwt;

namespace System
{
    /// <summary>
    /// Utilitários para tokens de acesso padrão JWT
    /// </summary>
    public sealed class AccessTokenUtils
    {
        #region Private Constructors

        private AccessTokenUtils()
        {
        }

        #endregion Private Constructors

        #region Public Methods

        /// <summary>
        /// Verifica se o token JWT armazenado em <paramref name="accessToken"/> está expirado,
        /// considerando uma margem de segurança opcional em segundos.
        /// </summary>
        /// <param name="accessToken">String contendo o token JWT.</param>
        /// <param name="clockSkewInSeconds">Quantidade de segundos a subtrair da data de expiração para fins de segurança. Valor padrão: dois segundos</param>
        /// <param name="trueIfEmpty">Se verdadeiro, retorna <c>true</c> se o token, <paramref name="accessToken"/>, estiver vazio ou nulo. Caso contrário, lança uma exceção.</param>
        /// <returns><c>true</c> se o token estiver expirado (considerando a margem de segurança); caso contrário, <c>false</c>.</returns>
        /// <exception cref="ArgumentException">Lançado se o token não puder ser lido ou não contiver a informação de expiração.</exception>
        public static bool IsExpired(string accessToken, int clockSkewInSeconds = 2, bool trueIfEmpty = true)
        {
            if(string.IsNullOrWhiteSpace(accessToken))
            {
                return trueIfEmpty ? true : throw new ArgumentNullException(nameof(accessToken), "AccessToken não pode ser nulo ou vazio.");
            }

            var handler = new JwtSecurityTokenHandler();

            if(!handler.CanReadToken(accessToken))
            {
                throw new ArgumentException("O token fornecido não é um JWT válido.", nameof(accessToken));
            }

            var jwtToken = handler.ReadJwtToken(accessToken);

            if(!jwtToken.Payload.Expiration.HasValue)
            {
                throw new ArgumentException("O token não possui informação de expiração (exp).", nameof(accessToken));
            }

            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(jwtToken.Payload.Expiration.Value);

            // Aplica o Clock Skew (margem de segurança) subtraindo segundos da expiração
            var adjustedExpiry = expiryDate.AddSeconds(-clockSkewInSeconds);

            return adjustedExpiry < DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Verifica se o token é válido.
        /// <para>Retorna verdadeiro se <paramref name="accessToken"/> não for vazio e não estiver expirado</para>
        /// </summary>
        /// <param name="accessToken">Token de acesso</param>
        /// <returns></returns>
        public static bool IsValid(string accessToken) => !string.IsNullOrWhiteSpace(accessToken) && !IsExpired(accessToken);

        #endregion Public Methods
    }
}