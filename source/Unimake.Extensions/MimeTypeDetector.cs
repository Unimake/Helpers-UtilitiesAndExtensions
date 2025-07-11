using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System
{
    /// <summary>
    /// Classe responsável por detectar o tipo MIME e a extensão de um arquivo com base na assinatura binária.
    /// </summary>
    /// <remarks>
    /// Tem como base a documentação em <see href="https://en.wikipedia.org/wiki/List_of_file_signatures"/>
    /// <para>Está sendo implementado conforme a necessidade, você pode adicionar seus tipos aqui ou
    /// solicitar através do Github em <see href="https://github.com/Unimake/Helpers-UtilitiesAndExtensions/issues"/></para>
    /// </remarks>
    public static class MimeTypeDetector
    {
        #region Private Fields

        /// <summary>
        /// Dicionário contendo as assinaturas binárias dos arquivos e seus respectivos tipos MIME e extensões.
        /// <para>Você pode adicionar seus tipos aqui, se não estiver implementado ainda. </para>
        /// </summary>
        private static readonly List<(byte[] Signature, FileType FileType)> _fileTypes = new List<(byte[] Signature, FileType FileType)>
        {
            // 📷 Imagens
            ( new byte[] { 0xFF, 0xD8, 0xFF }, new FileType { MimeType = "image/jpeg", Extension = "jpg" } ),
            (new byte[] { 0x89, 0x50, 0x4E, 0x47 }, new FileType { MimeType = "image/png", Extension = "png" }),
            (new byte[] { 0x47, 0x49, 0x46, 0x38 }, new FileType { MimeType = "image/gif", Extension = "gif" }),
            (new byte[] { 0x42, 0x4D }, new FileType { MimeType = "image/bmp", Extension = "bmp" }),
            (new byte[] { 0x52, 0x49, 0x46, 0x46 }, new FileType { MimeType = "image/webp", Extension = "webp" }),

            // 🎥 Vídeos
            (new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70 }, new FileType { MimeType = "video/mp4", Extension = "mp4" }),
            (new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, new FileType { MimeType = "video/x-matroska", Extension = "mkv" })  ,
            (new byte[] { 0x52, 0x49, 0x46, 0x46 }, new FileType { MimeType = "video/x-msvideo", Extension = "avi" }),
            (new byte[] { 0x00, 0x00, 0x01, 0xBA }, new FileType { MimeType = "video/mpeg", Extension = "mpeg" }),
            (new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, new FileType { MimeType = "video/quicktime", Extension = "mov" }),

            // 📄 Documentos
            (new byte[] { 0x25, 0x50, 0x44, 0x46 }, new FileType { MimeType = "application/pdf", Extension = "pdf" }),
            (new byte[] { 0xD0, 0xCF, 0x11, 0xE0 }, new FileType { MimeType = "application/msword", Extension = "doc" }),
            (new byte[] { 0x50, 0x4B, 0x03, 0x04 }, new FileType { MimeType = "application/zip", Extension = "zip" }),
            (new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C }, new FileType { MimeType = "application/xml", Extension = "xml" }),
            (new byte[] { 0xEF, 0xBB, 0xBF }, new FileType { MimeType = "text/plain", Extension = "txt" }),
            (new byte[] { 0x3C, 0x68, 0x74, 0x6D, 0x6C }, new FileType { MimeType = "text/html", Extension = "html" }),

            // 🎵 Áudio
            (new byte[] { 0x49, 0x44, 0x33 }, new FileType { MimeType = "audio/mpeg", Extension = "mp3" }),
            (new byte[] { 0x4F, 0x67, 0x67, 0x53 }, new FileType { MimeType = "audio/ogg", Extension = "ogg" }),
            (new byte[] { 0x52, 0x49, 0x46, 0x46 }, new FileType { MimeType = "audio/wav", Extension = "wav" }),
            (new byte[] { 0xFF, 0xF1 }, new FileType { MimeType = "audio/aac", Extension = "aac" }),
        };

        #endregion Private Fields

        #region Private Methods

        private static FileType DetectZipBasedFile(byte[] fileBytes)
        {
            try
            {
                using(var ms = new MemoryStream(fileBytes))
                {
                    using(var archive = new System.IO.Compression.ZipArchive(ms, System.IO.Compression.ZipArchiveMode.Read, leaveOpen: true))
                    {
                        if(archive.Entries.Any(e => e.FullName.Equals("word/document.xml", StringComparison.OrdinalIgnoreCase)))
                            return new FileType { MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", Extension = "docx" };

                        if(archive.Entries.Any(e => e.FullName.Equals("xl/workbook.xml", StringComparison.OrdinalIgnoreCase)))
                            return new FileType { MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Extension = "xlsx" };

                        if(archive.Entries.Any(e => e.FullName.Equals("ppt/presentation.xml", StringComparison.OrdinalIgnoreCase)))
                            return new FileType { MimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation", Extension = "pptx" };

                        if(archive.Entries.Any(e => e.FullName.Equals("META-INF/MANIFEST.MF", StringComparison.OrdinalIgnoreCase)))
                            return new FileType { MimeType = "application/java-archive", Extension = "jar" };

                        if(archive.Entries.Any(e => e.FullName.Equals("AndroidManifest.xml", StringComparison.OrdinalIgnoreCase)))
                            return new FileType { MimeType = "application/vnd.android.package-archive", Extension = "apk" };

                        if(archive.Entries.Any(e => e.FullName.Equals("mimetype", StringComparison.OrdinalIgnoreCase) ||
                                                     e.FullName.Equals("META-INF/container.xml", StringComparison.OrdinalIgnoreCase)))
                            return new FileType { MimeType = "application/epub+zip", Extension = "epub" };

                        // Se não encontrou nenhuma assinatura conhecida → ZIP genérico
                        return new FileType { MimeType = "application/zip", Extension = "zip" };
                    }
                }
            }
            catch
            {
                // Se falhar, trata como ZIP genérico
                return new FileType { MimeType = "application/zip", Extension = "zip" };
            }
        }

        private static bool IsUtf8(byte[] bytes)
        {
            try
            {
                var text = System.Text.Encoding.UTF8.GetString(bytes);
                var encoded = System.Text.Encoding.UTF8.GetBytes(text);
                return encoded.SequenceEqual(bytes);
            }
            catch
            {
                return false;
            }
        }

        #endregion Private Methods

        #region Public Structs

        /// <summary>
        /// Estrutura que representa um tipo de arquivo, contendo seu MIME type e extensão.
        /// </summary>
        public struct FileType
        {
            #region Public Properties

            /// <summary>
            /// Extensão do arquivo sem ponto. (Por exemplo, "jpg", "png", "mp4").
            /// </summary>
            public string Extension { get; set; }

            /// <summary>
            /// Tipo MIME do arquivo (por exemplo, "image/jpeg", "video/mp4").
            /// </summary>
            public string MimeType { get; set; }

            #endregion Public Properties
        }

        #endregion Public Structs

        #region Public Methods

        /// <summary>
        /// Registra um novo tipo de arquivo baseado em sua assinatura binária.
        /// </summary>
        /// <param name="signature">Assinatura binária que identifica o tipo de arquivo.</param>
        /// <param name="fileType">Estrutura <see cref="FileType"/> contendo o tipo MIME e a extensão do arquivo.</param>
        /// <param name="overwrite">Se verdadeiro, sobrescreve um tipo existente com a mesma assinatura. O padrão é false.</param>
        /// <exception cref="ArgumentException">
        /// Lançada quando a assinatura já estiver registrada e <paramref name="overwrite"/> for <c>false</c>.
        /// </exception>
        public static void AddFileType(byte[] signature, FileType fileType, bool overwrite = false)
        {
            var index = _fileTypes.FindIndex(ft => ft.Signature.SequenceEqual(signature));

            if(!overwrite &&
               index > 0)
            {
                var existing = _fileTypes[index].FileType;  
                throw new ArgumentException($"A assinatura binária já está registrada para o tipo MIME '{existing.MimeType}' e extensão '{existing.Extension}'.");
            }

            if(index == -1)
            {
                _fileTypes.Add((signature, fileType));
                return;
            }

            // Se já existe, atualiza
            _fileTypes[index] = (signature, fileType);
        }

        /// <summary>
        /// Obtém o tipo MIME e a extensão de um arquivo com base na sua assinatura binária.
        /// </summary>
        /// <param name="fileBytes">Array de bytes representando o conteúdo do arquivo.</param>
        /// <returns>Uma estrutura <see cref="FileType"/> contendo o MIME type e a extensão do arquivo.</returns>
        /// <remarks>Retorna <code>new FileType { MimeType = "application/octet-stream", Extension = "bin" };</code> se nada for encontrado</remarks>
        public static FileType GetFileMimeTypeAndExtension(this byte[] fileBytes)
        {
            // É de tipos conhecidos?
            foreach(var (signature, type) in _fileTypes)
            {
                if(fileBytes.Length >= signature.Length && fileBytes.Take(signature.Length).SequenceEqual(signature))
                {
                    // Sim. É ZIP? .................................................Não, é outro tipo
                    return type.Extension == "zip" ? DetectZipBasedFile(fileBytes) : type;
                }
            }

            // Não. É UTF-8?
            //⚠️ Essa abordagem pode falhar em casos raros de arquivos binários que "passam" como UTF-8.

            if(IsUtf8(fileBytes))
            {
                return new FileType { MimeType = "text/plain", Extension = "txt" };
            }

            return new FileType { MimeType = "application/octet-stream", Extension = "bin" };
        }

        #endregion Public Methods
    }
}