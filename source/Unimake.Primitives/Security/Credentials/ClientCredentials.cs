namespace Unimake.Primitives.Security.Credentials
{
    /// <summary>
    /// Representa as credenciais de um cliente para autenticação
    /// </summary>
    public struct ClientCredentials
    {
        #region Private Fields

        private string _clientId;
        private string _secret;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Se o <see cref="GrantType"/> for <see cref="GrantType.ClientCredentials"/>, a chave do cliente deve ser informada
        /// </summary>
        public string ClientId { get => _clientId ?? (_clientId = string.Empty); set => _clientId = value; }

        public string Secret { get => _secret ?? (_secret = string.Empty); set => _secret = value; }

        #endregion Public Properties
    }
}