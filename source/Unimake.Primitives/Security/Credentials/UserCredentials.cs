namespace Unimake.Primitives.Security.Credentials
{
    /// <summary>
    /// Nome de usuário e senha para acesso ao aplicativo
    /// </summary>
    public struct UserCredentials
    {
        #region Private Fields

        private string _password;
        private string _username;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Senha cadastrada pelo usuário no momento da criação da conta no ERP.net
        /// </summary>
        public string Password { get => _password ?? (_password = string.Empty); set => _password = value; }

        /// <summary>
        /// Nome de usuário, e-mail, informado no momento da criação da conta no ERP.net
        /// </summary>
        public string Username { get => _username ?? (_username = string.Empty); set => _username = value; }

        #endregion Public Properties
    }
}