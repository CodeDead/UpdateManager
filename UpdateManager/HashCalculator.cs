using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CodeDead.UpdateManager
{
    /// <summary>
    /// Class that contains logic to calculate hashes
    /// </summary>
    public sealed class HashCalculator
    {
        #region Variables

        private string _filePath;
        private byte[] _fileData;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path of the file for which hashes should be calculated
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value.Length == 0) throw new ArgumentException(nameof(value));
                _filePath = value;
            }
        }

        #endregion

        /// <summary>
        /// Initialize a new HashCalculator
        /// </summary>
        public HashCalculator()
        {

        }

        /// <summary>
        /// Initialize a new HashCalculator
        /// </summary>
        /// <param name="filePath">The path of the file for which hashes should be calculated</param>
        public HashCalculator(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Read the bytes of the chosen file
        /// </summary>
        public void ReadData()
        {
            if (FilePath == null) throw new ArgumentNullException(nameof(FilePath));

            _fileData = File.ReadAllBytes(FilePath);
        }

        /// <summary>
        /// Asynchronously read the bytes of the chosen file
        /// </summary>
        /// <returns></returns>
        public async Task ReadDataAsync()
        {
            byte[] result;

            using (FileStream sourceStream = File.Open(FilePath, FileMode.Open))
            {
                result = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(result, 0, (int)sourceStream.Length);
            }

            _fileData = result;
        }

        /// <summary>
        /// Calculate the MD5 hash of a file
        /// </summary>
        /// <returns>The MD5 hash string</returns>
        public string CalculateMd5()
        {
            if (_fileData == null) throw new ArgumentNullException(nameof(_fileData));
            if (_fileData.Length == 0) throw new ArgumentException(nameof(_fileData));

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(_fileData);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Asynchronously calculate the MD5 hash of a file
        /// </summary>
        /// <returns>The MD5 hash string</returns>
        public async Task<string> CalculateMd5Async()
        {
            string result = null;
            await Task.Run(() => { result = CalculateMd5(); });
            return result;
        }

        /// <summary>
        /// Calculate the SHA1 hash of a file
        /// </summary>
        /// <returns>The SHA1 hash string</returns>
        public string CalculateSha1()
        {
            if (_fileData == null) throw new ArgumentNullException(nameof(_fileData));
            if (_fileData.Length == 0) throw new ArgumentException(nameof(_fileData));

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(_fileData);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Asynchronously calculate the SHA1 hash of a file
        /// </summary>
        /// <returns>The SHA1 hash string</returns>
        public async Task<string> CalculateSha1Async()
        {
            string result = null;
            await Task.Run(() => { result = CalculateSha1(); });
            return result;
        }

        /// <summary>
        /// Calculate the SHA256 hash of a file
        /// </summary>
        /// <returns>The SHA256 hash string</returns>
        public string CalculateSha256()
        {
            if (_fileData == null) throw new ArgumentNullException(nameof(_fileData));
            if (_fileData.Length == 0) throw new ArgumentException(nameof(_fileData));

            // ReSharper disable once AccessToStaticMemberViaDerivedType
            using (SHA256 sha256 = SHA256Managed.Create())
            {
                byte[] hash = sha256.ComputeHash(_fileData);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Asynchronously calculate the SHA256 hash of a file
        /// </summary>
        /// <returns>The SHA256 hash string</returns>
        public async Task<string> CalculateSha256Async()
        {
            string result = null;
            await Task.Run(() => { result = CalculateSha256(); });
            return result;
        }

        /// <summary>
        /// Calculate the SHA384 hash of a file
        /// </summary>
        /// <returns>The SHA384 hash string</returns>
        public string CalculateSha384()
        {
            if (_fileData == null) throw new ArgumentNullException(nameof(_fileData));
            if (_fileData.Length == 0) throw new ArgumentException(nameof(_fileData));

            // ReSharper disable once AccessToStaticMemberViaDerivedType
            using (SHA384 sha256 = SHA384Managed.Create())
            {
                byte[] hash = sha256.ComputeHash(_fileData);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Asynchronously calculate the SHA384 hash of a file
        /// </summary>
        /// <returns>The SHA384 hash string</returns>
        public async Task<string> CalculateSha384Async()
        {
            string result = null;
            await Task.Run(() => { result = CalculateSha384(); });
            return result;
        }

        /// <summary>
        /// Calculate the SHA512 hash of a file
        /// </summary>
        /// <returns>The SHA512 hash string</returns>
        public string CalculateSha512()
        {
            if (_fileData == null) throw new ArgumentNullException(nameof(_fileData));
            if (_fileData.Length == 0) throw new ArgumentException(nameof(_fileData));

            // ReSharper disable once AccessToStaticMemberViaDerivedType
            using (SHA512 sha256 = SHA512Managed.Create())
            {
                byte[] hash = sha256.ComputeHash(_fileData);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Asynchronously calculate the SHA512 hash of a file
        /// </summary>
        /// <returns>The SHA512 hash string</returns>
        public async Task<string> CalculateSha512Async()
        {
            string result = null;
            await Task.Run(() => { result = CalculateSha512(); });
            return result;
        }
    }
}
