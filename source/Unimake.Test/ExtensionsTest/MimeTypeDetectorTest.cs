using System;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.ExtensionsTest
{
    public class MimeTypeDetectorTest
    {
        #region Private Fields

        private byte[] UnknownBytes = new byte[] { 0xFD, 0xD5, 0xF2 };

        #endregion Private Fields

        #region Public Constructors

        public MimeTypeDetectorTest()
        {
            //Apenas para testar a adição de um novo tipo de arquivo
            MimeTypeDetector.AddFileType(UnknownBytes, new MimeTypeDetector.FileType { MimeType = "unknown/unknown", Extension = "unk" });
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void ArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                MimeTypeDetector.AddFileType(UnknownBytes, new MimeTypeDetector.FileType { MimeType = "unknown/unknown", Extension = "unk" });
            });
        }

        [Theory]
        [InlineData(@"D:\Temp\escapamento.webp", "webp", "image/webp")]
        [InlineData(@"D:\Temp\bklcom.pdf", "pdf", "application/pdf")]
        [InlineData(@"D:\Temp\agencia.png", "png", "image/png")]
        public void GetMimeType(string fileName, string extension, string mimeType)
        {
            var fi = new System.IO.FileInfo(fileName);
            var bytes = System.IO.File.ReadAllBytes(fileName);
            var result = bytes.GetFileMimeTypeAndExtension();

            Assert.Equal(mimeType, result.MimeType);
            Assert.Equal(extension, result.Extension);
            Assert.Equal($".{extension}", fi.Extension);
        }

        [Theory]
        [InlineData("unk", "unknown/unknown")]
        public void GetUnknownMimeType(string extension, string mimeType)
        {
            var result = UnknownBytes.GetFileMimeTypeAndExtension();

            Assert.Equal(mimeType, result.MimeType);
            Assert.Equal(extension, result.Extension);
        }

        [Fact]
        public void OverriteTest()
        {
            MimeTypeDetector.AddFileType(UnknownBytes, new MimeTypeDetector.FileType { MimeType = "unknown/unknown", Extension = "unk" }, true);
        }

        #endregion Public Methods
    }
}