// 代码生成时间: 2025-09-17 16:47:29
 * This class provides functionality to calculate hash values for strings.
 * It demonstrates good C# practices, error handling, and documentation.
 */
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashCalculationTool
{
    /// <summary>
    /// Service class to calculate hash values for strings.
    /// </summary>
    public class HashCalculatorService
    {
        /// <summary>
        /// Calculates the SHA256 hash of the provided input string.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The SHA256 hash of the input string as a hexadecimal string.</returns>
        public string CalculateSHA256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
