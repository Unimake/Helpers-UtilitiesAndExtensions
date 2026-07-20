using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unimake.Net;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Net
{
    public class UtilityTest
    {
        #region Public Methods

        [Theory]
        [InlineData(WebExceptionStatus.NameResolutionFailure, HttpConnectionFailureType.Dns)]
        [InlineData(WebExceptionStatus.ConnectFailure, HttpConnectionFailureType.Connection)]
        [InlineData(WebExceptionStatus.ConnectionClosed, HttpConnectionFailureType.Connection)]
        [InlineData(WebExceptionStatus.KeepAliveFailure, HttpConnectionFailureType.Connection)]
        [InlineData(WebExceptionStatus.ReceiveFailure, HttpConnectionFailureType.Connection)]
        [InlineData(WebExceptionStatus.SendFailure, HttpConnectionFailureType.Connection)]
        [InlineData(WebExceptionStatus.Timeout, HttpConnectionFailureType.Timeout)]
        [InlineData(WebExceptionStatus.TrustFailure, HttpConnectionFailureType.Tls)]
        [InlineData(WebExceptionStatus.SecureChannelFailure, HttpConnectionFailureType.Tls)]
        [InlineData(WebExceptionStatus.ProxyNameResolutionFailure, HttpConnectionFailureType.Proxy)]
        [InlineData(WebExceptionStatus.ProtocolError, HttpConnectionFailureType.Http)]
        [InlineData(WebExceptionStatus.Success, HttpConnectionFailureType.None)]
        public void ClassifyWebExceptionStatusTest(WebExceptionStatus status, HttpConnectionFailureType expected) =>
            Assert.Equal(expected, Utility.ClassifyWebExceptionStatus(status));

        [Theory]
        [InlineData(200, true, HttpConnectionFailureType.None, WebExceptionStatus.Success)]
        [InlineData(404, false, HttpConnectionFailureType.Http, WebExceptionStatus.ProtocolError)]
        [InlineData(500, false, HttpConnectionFailureType.Http, WebExceptionStatus.ProtocolError)]
        public void TestHttpConnectionDetailedPreservaRespostaHttp(int statusCode, bool sucesso,
            HttpConnectionFailureType falha, WebExceptionStatus webExceptionStatus)
        {
            var resultado = ComServidorHttp(statusCode,
                url => Utility.TestHttpConnectionDetailed(url, timeoutInSeconds: 2));

            Assert.True(resultado.ResponseReceived);
            Assert.Equal(statusCode, resultado.StatusCode);
            Assert.Equal(sucesso, resultado.IsSuccessStatusCode);
            Assert.Equal(falha, resultado.FailureType);
            Assert.Equal(webExceptionStatus, resultado.WebExceptionStatus);
            Assert.True(resultado.DurationMilliseconds >= 0);
        }

        [Fact]
        public void TestHttpConnectionMantemCompatibilidadeParaErroHttp()
        {
            var respondeu = ComServidorHttp(500,
                url => Utility.TestHttpConnection(url, timeoutInSeconds: 2));

            Assert.True(respondeu);
        }

        [Fact]
        public void TestHttpConnectionDetailedClassificaTimeoutReal()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            var servidor = Task.Run(() =>
            {
                using(var cliente = listener.AcceptTcpClient())
                {
                    Thread.Sleep(1500);
                }
            });

            try
            {
                var resultado = Utility.TestHttpConnectionDetailed(
                    $"http://127.0.0.1:{port}/", timeoutInSeconds: 1);

                Assert.False(resultado.ResponseReceived);
                Assert.Equal(HttpConnectionFailureType.Timeout, resultado.FailureType);
                Assert.Equal(WebExceptionStatus.Timeout, resultado.WebExceptionStatus);
                Assert.True(resultado.DurationMilliseconds >= 900);
            }
            finally
            {
                listener.Stop();
                servidor.GetAwaiter().GetResult();
            }
        }

        [Fact]
        public void TestHttpConnectionDetailedClassificaConexaoRecusada()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();

            var resultado = Utility.TestHttpConnectionDetailed(
                $"http://127.0.0.1:{port}/", timeoutInSeconds: 1);

            Assert.False(resultado.ResponseReceived);
            Assert.Equal(HttpConnectionFailureType.Connection, resultado.FailureType);
            Assert.Equal(WebExceptionStatus.ConnectFailure, resultado.WebExceptionStatus);
        }

        private static T ComServidorHttp<T>(int statusCode, Func<string, T> executar)
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            var servidor = Task.Run(() =>
            {
                using(var cliente = listener.AcceptTcpClient())
                using(var stream = cliente.GetStream())
                {
                    var buffer = new byte[4096];
                    stream.Read(buffer, 0, buffer.Length);
                    var resposta = Encoding.ASCII.GetBytes(
                        $"HTTP/1.1 {statusCode} Teste\r\nContent-Length: 0\r\nConnection: close\r\n\r\n");
                    stream.Write(resposta, 0, resposta.Length);
                }
            });

            try
            {
                var resultado = executar($"http://127.0.0.1:{port}/");
                servidor.GetAwaiter().GetResult();
                return resultado;
            }
            finally
            {
                listener.Stop();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [Trait("BugFix", "#172205")]
        public void Fix_172205(int timeoutInSeconds)
        {
            var watch = Stopwatch.StartNew();
            watch.Start();
            _ = Unimake.Net.Utility.HasInternetConnection(timeoutInSeconds);
            watch.Stop();

            var elapsed = watch.Elapsed;

            Assert.True((int)elapsed.TotalSeconds <= timeoutInSeconds, $"O tempo de execução '{(int)elapsed.TotalSeconds:N0}s' foi maior que {timeoutInSeconds:N0} segundos.");
        }

        [Fact]
        public void GetLocalIPV4Address() => Console.WriteLine(Utility.GetLocalIPAddress());

        [Fact]
        public void GetLocalIPV6Address() => Console.WriteLine(Utility.GetLocalIPAddress(true));

        [Theory]
        [InlineData(01)]
        [InlineData(02)]
        [InlineData(03)]
        [InlineData(04)]
        [InlineData(05)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(30)]
        public void HasInternetConnection(int timeoutInSeconds) => Assert.True(Unimake.Net.Utility.HasInternetConnection(timeoutInSeconds));

        [Fact]
        [Trait("Utility", "Net")]
        public void HasInternetConnectionTest()
        {
            string[] testUrls = new string[] {
                    "8.8.8.8", //Servidor Primário de DNS do Google
                    "8.8.4.4", //Servidor Secundário de DNS do Google
                    "1.1.1.1", //Servidor Primário de DNS do Cloudfare
                    "1.0.0.1",  //Servidor Secundário de DNS do Cloudfare
                    "9.9.9.9", //Servidor Primário de DNS do Quad 9
                    "149.112.112.112", //Servidor Secundário de DNS do Quad 9
                    "http://clients3.google.com/generate_204",
                    "http://www.microsoft.com",
                    "http://www.cloudflare.com",
                    "http://www.amazon.com",
                    "http://www.unimake.com.br",
                    "http://67.205.183.164"
            };

            string[] urls;

            foreach(string url in testUrls)
            {
                urls = new string[] { url };

                Assert.True(Unimake.Net.Utility.HasInternetConnection(null, 3, urls));
            }

            testUrls = new string[] {
                "http://www.unimakexx.com",
                "3.3.3.3",
            };

            foreach(string url in testUrls)
            {
                urls = new string[] { url };

                Assert.False(Unimake.Net.Utility.HasInternetConnection(null, 3, urls));
            }

            Assert.True(Unimake.Net.Utility.HasInternetConnection(3));
        }

        #endregion Public Methods
    }
}
