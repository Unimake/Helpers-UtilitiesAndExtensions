using System.Net;

namespace Unimake.Net
{
    /// <summary>
    /// Categoria de falha encontrada durante uma conexão HTTP.
    /// </summary>
    public enum HttpConnectionFailureType
    {
        /// <summary>Nenhuma falha.</summary>
        None = 0,
        /// <summary>Falha de resolução DNS.</summary>
        Dns = 1,
        /// <summary>Falha de conexão TCP.</summary>
        Connection = 2,
        /// <summary>Tempo limite excedido.</summary>
        Timeout = 3,
        /// <summary>Falha no canal TLS.</summary>
        Tls = 4,
        /// <summary>Falha relacionada ao proxy.</summary>
        Proxy = 5,
        /// <summary>Resposta HTTP de erro.</summary>
        Http = 6,
        /// <summary>Falha não classificada.</summary>
        Unknown = 7
    }

    /// <summary>
    /// Resultado detalhado de uma tentativa de conexão HTTP.
    /// </summary>
    public sealed class HttpConnectionResult
    {
        /// <summary>Indica que o servidor devolveu uma resposta HTTP.</summary>
        public bool ResponseReceived { get; set; }

        /// <summary>Indica código HTTP entre 200 e 299.</summary>
        public bool IsSuccessStatusCode { get; set; }

        /// <summary>Código HTTP, ou zero quando não houve resposta.</summary>
        public int StatusCode { get; set; }

        /// <summary>Status técnico informado por <see cref="WebException"/>.</summary>
        public WebExceptionStatus WebExceptionStatus { get; set; }

        /// <summary>Categoria normalizada da falha.</summary>
        public HttpConnectionFailureType FailureType { get; set; }

        /// <summary>Duração da tentativa, em milissegundos.</summary>
        public long DurationMilliseconds { get; set; }

        /// <summary>Mensagem resumida do erro.</summary>
        public string ErrorMessage { get; set; }
    }
}
