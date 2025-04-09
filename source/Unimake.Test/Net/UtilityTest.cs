using System;
using System.Diagnostics;
using Unimake.Net;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.Net
{
    public class UtilityTest
    {
        #region Public Methods

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
                "http://www.unimake.com",
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