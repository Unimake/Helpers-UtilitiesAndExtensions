using System;
using System.IO;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.ExtensionsTest
{
    public class MimeTypeDetectorTest
    {
        #region Private Fields

        private static bool registered = false;
        private byte[] UnknownBytes = new byte[] { 0xFD, 0xD5, 0xF2 };

        #endregion Private Fields

        #region Public Constructors

        public MimeTypeDetectorTest()
        {
            if(registered)
                return;

            registered = true;
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
        [InlineData(@"danfeview.webp", "webp", "image/webp")]
        [InlineData(@"PDFFile.pdf", "pdf", "application/pdf")]
        [InlineData(@"TXTFile.txt", "txt", "text/plain")]
        [InlineData(@"unimake.png", "png", "image/png")]
        [InlineData(@"ZIP.zip", "zip", "application/zip")]
        [InlineData(@"word.docx", "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        public void GetMimeType(string fileName, string extension, string mimeType)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Assets", "Files", fileName);
            var fi = new System.IO.FileInfo(path);
            var bytes = System.IO.File.ReadAllBytes(fi.FullName);
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
            //Apenas para testar a adição de um novo tipo de arquivo
            MimeTypeDetector.AddFileType(UnknownBytes, new MimeTypeDetector.FileType { MimeType = "unknown/unknown", Extension = "unk" }, true);
        }

        #endregion Public Methods
    }
}