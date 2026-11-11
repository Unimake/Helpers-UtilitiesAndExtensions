using Unimake.Cryptography;

#pragma warning disable IDE0130 // Namespace de extensões devem corresponder ao tipo extendido

namespace System
{
    public static class StringExtensions
    {
        #region Public Methods

        public static string Decrypt(this string input, string password) => AES.Decrypt(input, password);

        public static string Encrypt(this string input, string password) => AES.Encrypt(input, password);

        #endregion Public Methods
    }
}
