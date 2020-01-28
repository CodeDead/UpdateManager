using System;
using System.IO;
using System.Security.Cryptography;

namespace CodeDead.UpdateManager
{
    /// <summary>
    /// Class that contains logic to calculate hashes
    /// </summary>
    public sealed class HashCalculator
    {
        /// <summary>
        /// Calculate the MD5 hash of a file
        /// </summary>
        /// <param name="path">The path of the file for which the MD5 hash should be calculated</param>
        /// <returns>The MD5 hash string</returns>
        public static string CalculateMD5(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException(nameof(path));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(bs);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Calculate the SHA1 hash of a file
        /// </summary>
        /// <param name="path">The path of the file for which the SHA1 hash should be calculated</param>
        /// <returns>The SHA1 hash string</returns>
        public static string CalculateSHA1(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException(nameof(path));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    byte[] hash = sha1.ComputeHash(bs);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Calculate the SHA256 hash of a file
        /// </summary>
        /// <param name="path">The path of the file for which the SHA256 hash should be calculated</param>
        /// <returns>The SHA256 hash string</returns>
        public static string CalculateSHA256(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException(nameof(path));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA256 sha256 = SHA256Managed.Create())
                {
                    byte[] hash = sha256.ComputeHash(bs);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Calculate the SHA384 hash of a file
        /// </summary>
        /// <param name="path">The path of the file for which the SHA384 hash should be calculated</param>
        /// <returns>The SHA384 hash string</returns>
        public static string CalculateSHA384(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException(nameof(path));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA384 sha256 = SHA384Managed.Create())
                {
                    byte[] hash = sha256.ComputeHash(bs);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        /// <summary>
        /// Calculate the SHA512 hash of a file
        /// </summary>
        /// <param name="path">The path of the file for which the SHA512 hash should be calculated</param>
        /// <returns>The SHA512 hash string</returns>
        public static string CalculateSHA512(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException(nameof(path));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA512 sha256 = SHA512Managed.Create())
                {
                    byte[] hash = sha256.ComputeHash(bs);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
