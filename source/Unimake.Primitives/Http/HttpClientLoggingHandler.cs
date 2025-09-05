using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Unimake.Primitives.Http
{
    /// <summary>
    /// Handler para logs do HttpClient
    /// </summary>
    public sealed class HttpClientLoggingHandler : DelegatingHandler
    {
        #region Private Methods

        private static void LogHeaders(HttpHeaders headers)
        {
            WriteLine("Headers:");

            foreach(var header in headers)
            {
                foreach(var value in header.Value)
                {
                    WriteLine($"{header.Key}: {value}");
                }
            }
        }

        private static void WriteLine(string message) => Trace.WriteLine(message);

        private static async Task WriteRequestAsync(HttpRequestMessage request)
        {
            WriteLine("Request:");
            WriteLine($"Method: {request.Method}");
            WriteLine($"RequestUri: {request.RequestUri}");
            LogHeaders(request.Headers);

            if(request.Content != null)
            {
                WriteLine("Payload/Body:");
                WriteLine(await request.Content.ReadAsStringAsync());
            }

            WriteLine("");
        }

        private void WriteCertificate()
        {
            WriteLine("Certificate(s):");

            if(InnerHandler is HttpClientHandler clientHandler &&
                           clientHandler.ClientCertificates?.Count > 0)
            {
                foreach(var cert in clientHandler.ClientCertificates)
                {
                    var certs = cert.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach(var item in certs)
                    {
                        WriteLine(item);
                    }
                }

                return;
            }

            WriteLine("Request has no certificate(s).");
            WriteLine("");
        }

        private async Task<HttpResponseMessage> WriteResponseAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopWatch = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);
            stopWatch.Stop();

            WriteLine($"Response: (elapsed {stopWatch.ElapsedMilliseconds} (ms))");
            WriteLine($"StatusCode: {(int)response.StatusCode}");
            WriteLine($"ReasonPhrase: {response.ReasonPhrase}");
            WriteLine($"Headers:\r\n{response.Headers}");
            WriteLine("");

            if(response.Content != null)
            {
                WriteLine("Response Content:");
                WriteLine(await response.Content.ReadAsStringAsync());
                WriteLine("");
            }

            return response;
        }

        #endregion Private Methods

        #region Protected Methods

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            WriteLine($"{Environment.NewLine}===================== <HttpClientLogging> =====================");
            WriteCertificate();
            await WriteRequestAsync(request);
            var response = await WriteResponseAsync(request, cancellationToken);
            WriteLine($"===================== </HttpClientLogging> ====================={Environment.NewLine}");
            return response;
        }

        #endregion Protected Methods

        #region Public Classes

        public static class Builder
        {
            #region Public Methods

            public static HttpClient Create(bool disposeHandler = true)
            {
                return new HttpClient(new HttpClientLoggingHandler(), disposeHandler);
            }

            public static HttpClient Create(HttpMessageHandler innerHandler, bool disposeHandler = true)
            {
                return new HttpClient(new HttpClientLoggingHandler(innerHandler), disposeHandler);
            }

            #endregion Public Methods
        }

        #endregion Public Classes

        #region Public Constructors

        /// <summary>
        /// <inheritdoc cref="DelegatingHandler(HttpMessageHandler)"/>
        /// </summary>
        /// <param name="innerHandler"><inheritdoc cref="DelegatingHandler(HttpMessageHandler)"/></param>
        public HttpClientLoggingHandler(HttpMessageHandler innerHandler)
                    : base(innerHandler)
        {
        }

        /// <summary>
        /// <inheritdoc cref="DelegatingHandler()"/>
        /// </summary>
        public HttpClientLoggingHandler()
            : this(new HttpClientHandler())
        {
        }

        #endregion Public Constructors
    }
}