using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Unimake
{
    public static class RuntimeHelper
    {
        #region Private Methods

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        #endregion Private Methods

        #region Public Properties

        /// <summary>
        /// Indica se a aplicação tem uma janela de console disponível.
        /// </summary>
        public static bool HasConsole
        {
            get
            {
                if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return GetConsoleWindow() != IntPtr.Zero;
                }

                // No Linux/macOS não tem GetConsoleWindow, usa outra heurística
                return Environment.UserInteractive && !Console.IsOutputRedirected;
            }
        }

        /// <summary>
        /// Indica se a aplicação foi iniciada via "dotnet MinhaApp.dll" (host dependente).
        /// </summary>
        public static bool IsDotNetHost
        {
            get
            {
                var processName = Process.GetCurrentProcess().ProcessName;
                return processName.Equals("dotnet", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Indica se está rodando de forma interativa (com usuário, não como serviço).
        /// </summary>
        public static bool IsInteractive =>
            Environment.UserInteractive && HasConsole;

        #endregion Public Properties
    }
}