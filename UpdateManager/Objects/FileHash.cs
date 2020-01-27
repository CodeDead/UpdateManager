namespace CodeDead.UpdateManager.Objects
{
    /// <summary>
    /// Class that contains the logic to display a file hash
    /// </summary>
    public sealed class FileHash
    {
        /// <summary>
        /// Initialize a new FileHash
        /// </summary>
        public FileHash()
        {

        }

        /// <summary>
        /// Initialize a new FileHash
        /// </summary>
        /// <param name="hashType">The hash type of the hash</param>
        /// <param name="hash">The hash of the file</param>
        public FileHash(string hashType, string hash)
        {
            HashType = hashType;
            Hash = hash;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the hash type of the hash
        /// </summary>
        public string HashType { get; set; }

        /// <summary>
        /// Gets or sets the hash of the file
        /// </summary>
        public string Hash { get; set; }

        #endregion
    }
}
