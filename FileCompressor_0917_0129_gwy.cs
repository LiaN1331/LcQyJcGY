// 代码生成时间: 2025-09-17 01:29:28
 * Usage:
 *   FileCompressor compressor = new FileCompressor();
 *   compressor.Decompress("compressedFilePath", "destinationFolder");
 */

using System;
using System.IO;
using System.IO.Compression;

namespace FileUtility
{
    public class FileCompressor
    {
        // Decompresses a zip file to a specified destination folder.
        public void Decompress(string compressedFilePath, string destinationFolder)
        {
            // Check if the file exists.
            if (!File.Exists(compressedFilePath))
            {
                throw new FileNotFoundException("The compressed file was not found.", compressedFilePath);
            }

            // Ensure the destination folder exists, create it if not.
            Directory.CreateDirectory(destinationFolder);

            try
            {
                // Decompress the file.
                ZipFile.ExtractToDirectory(compressedFilePath, destinationFolder);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during decompression.
                throw new InvalidOperationException("An error occurred while decompressing the file.", ex);
            }
        }
    }
}
